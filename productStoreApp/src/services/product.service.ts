import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { type Observable, map, BehaviorSubject, tap } from 'rxjs';
import { type IProduct, type IProductResponse } from '../interfaces/IProduct';
import { type ICategory } from '../interfaces/ICategory';
import environment from '../environments/environment.development';
import { IImageResponse } from '../interfaces/IImage';
import path from 'path';

const url = environment.apiUrl;
const urlImg = `${url}/image/product`;

@Injectable({
  providedIn: 'root',
})
export default class ProductService {
  private productsSubject = new BehaviorSubject<IProduct[]>([]);

  public products: Observable<IProduct[]> = this.productsSubject.asObservable();

  constructor(private readonly http: HttpClient) {}

  /**
   * Retrieves all products from the server.
   * @returns An observable emitting an array of products.
   */
  getProducts(): Observable<IProduct[]> {
    const link = `${url}/product`;

    return this.http.get<IProductResponse[]>(link).pipe(
      map((response) =>
        response.map((product) => ProductService.adaptProduct(product))
      ),
      tap((response) => {
        this.productsSubject.next(response);
      })
    );
  }

  /**
   * Retrieves a product by its ID from the server.
   * @param id The ID of the product to retrieve.
   * @returns An observable emitting the product with the specified ID.
   */
  getProduct(id: number): Observable<IProduct> {
    const link = `${url}/product/${id}`;

    return this.http
      .get<IProductResponse>(link)
      .pipe(map((product) => ProductService.adaptProduct(product)));
  }

  /**
   * Adapts a product received from the server to the client-side model.
   * @param apiProduct The product received from the server.
   * @returns The adapted client-side product.
   */
  static adaptProduct(apiProduct: IProductResponse): IProduct {
    const category: ICategory = {
      id: apiProduct.category.id,
      name: apiProduct.category.name,
      items: [],
    };

    return {
      id: apiProduct.id,
      name: apiProduct.name,
      price: apiProduct.price,
      discount: apiProduct.discount,
      unitMeasure: apiProduct.unitMeasure,
      category,
      description: apiProduct.description,
      specifications: apiProduct.specifications,
      imagePaths: (apiProduct.images ?? []).map((image) =>
        this.adaptImageResponse(image)
      ),
    };
  }

  static adaptImageResponse(apiImage: IImageResponse) {
    const convertedPath = `${urlImg}/${apiImage.path}`;

    return {
      path: convertedPath,
      alt: apiImage.alt,
    };
  }
}
