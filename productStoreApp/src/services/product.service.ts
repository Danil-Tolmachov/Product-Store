import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { type Observable, map } from 'rxjs';
import { type IProduct, type IProductResponse } from '../interfaces/IProduct';
import { type ICategory } from '../interfaces/ICategory';
import environment from '../environments/environment.development';

const url = environment.apiUrl;
const urlImg = `${url}/image/product`;

@Injectable({
  providedIn: 'root',
})
export default class ProductService {
  constructor(private readonly http: HttpClient) {}

  getProducts(): Observable<IProduct[]> {
    const link = `${url}/product`;

    return this.http
      .get<IProductResponse[]>(link)
      .pipe(
        map((response) =>
          response.map((product) => ProductService.adaptProduct(product))
        )
      );
  }

  getProduct(id: number): Observable<IProduct> {
    const link = `${url}/product/${id}`;

    return this.http
      .get<IProductResponse>(link)
      .pipe(map((product) => ProductService.adaptProduct(product)));
  }

  private static adaptProduct(apiProduct: IProductResponse): IProduct {
    const category: ICategory = {
      id: apiProduct.categoryId,
      name: apiProduct.categoryName,
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
      imagePaths: (apiProduct.imagePaths ?? []).map(
        (path) => `${urlImg}/${path}`
      ),
    };
  }
}
