import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {
  type Observable,
  map,
  BehaviorSubject,
  tap,
  catchError,
  EMPTY,
} from 'rxjs';
import {
  IProductPage,
  IProductPageResponse,
  type IProduct,
  type IProductResponse,
} from '../interfaces/IProduct';
import { type ICategory } from '../interfaces/ICategory';
import environment from '../environments/environment.development';
import { IImage, IImageResponse } from '../interfaces/IImage';

const url = environment.apiUrl;
const urlImg = `${url}/image/product`;

@Injectable({
  providedIn: 'root',
})
export default class ProductService {
  private productsSubject = new BehaviorSubject<IProduct[]>([]);

  private productsPageSubject = new BehaviorSubject<IProductPage | null>(null);

  public products: Observable<IProduct[]> = this.productsSubject.asObservable();

  public productsPage: Observable<IProductPage | null> =
    this.productsPageSubject.asObservable();

  constructor(private readonly http: HttpClient) {}

  /**
   * Retrieves all products from the server.
   * @returns {Observable<IProduct[]>} An observable emitting an array of products.
   */
  getProducts(): Observable<IProduct[]> {
    const link = `${url}/product`;

    return this.http.get<IProductPageResponse>(link).pipe(
      map((response) => ProductService.adaptProductPage(response).products),
      tap((response) => {
        this.productsSubject.next(response);
      }),
      catchError((error, caught) => {
        if (error.status === 0) {
          return EMPTY;
        }

        return caught;
      })
    );
  }

  /**
   * Retrieves products page from the server.
   * @param {number} page - The page number to retrieve.
   * @param {number} [count=10] - The count of products per page. Defaults to 10.
   * @returns {Observable<IProductPage>} An observable emitting a products page.
   */
  getProductsPage(page: number, count: number = 10): Observable<IProductPage> {
    const link = `${url}/product?page=${page}&count=${count}`;

    return this.http.get<IProductPageResponse>(link).pipe(
      map((response) => ProductService.adaptProductPage(response)),
      tap((response) => {
        this.productsPageSubject.next(response);
      }),
      catchError((error, caught) => {
        if (error.status === 0) {
          return EMPTY;
        }

        return caught;
      })
    );
  }

  /**
   * Retrieves a page of products by category from the server.
   * @param {number} categoryId - The ID of the category.
   * @param {number} page - The page number to retrieve.
   * @param {number} [count=10] - The count of products per page. Defaults to 10.
   * @returns {Observable<IProductPage>} An observable emitting a products page by category.
   */
  getProductsPageByCategory(
    categoryId: number,
    page: number,
    count: number = 10
  ): Observable<IProductPage> {
    const link = `${url}/product/category/${categoryId}?page=${page}&count=${count}`;

    return this.http.get<IProductPageResponse>(link).pipe(
      map((response) => ProductService.adaptProductPage(response)),
      tap((response) => {
        this.productsPageSubject.next(response);
      }),
      catchError((error, caught) => {
        if (error.status === 0) {
          return EMPTY;
        }

        return caught;
      })
    );
  }

  /**
   * Retrieves a product by its ID from the server.
   * @param {number} id - The ID of the product to retrieve.
   * @returns {Observable<IProduct>} An observable emitting the product with the specified ID.
   */
  getProduct(id: number): Observable<IProduct> {
    const link = `${url}/product/${id}`;

    return this.http.get<IProductResponse>(link).pipe(
      map((product) => ProductService.adaptProduct(product)),
      catchError((error, caught) => {
        if (error.status === 0) {
          return EMPTY;
        }

        return caught;
      })
    );
  }

  /**
   * Adapts a product received from the server to the client-side model.
   * @param {IProductResponse} apiProduct - The product received from the server.
   * @returns {IProduct} The adapted client-side product.
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

  /**
   * Adapts a product page received from the server to the client-side model.
   * @param {IProductPageResponse} apiProduct - The product page received from the server.
   * @returns {IProductPage} The adapted client-side product page.
   */
  static adaptProductPage(apiProduct: IProductPageResponse): IProductPage {
    return {
      products: apiProduct.products.map((product) =>
        ProductService.adaptProduct(product)
      ),

      currentPage: apiProduct.currentPage,
      totalCount: apiProduct.totalCount,
      pagesCount: apiProduct.pagesCount,
      pageSize: apiProduct.pageSize,
    };
  }

  /**
   * Adapts an image response from the server to the client-side model.
   * @param {IImageResponse} apiImage - The image response from the server.
   * @returns {IImage} The adapted client-side image.
   */
  static adaptImageResponse(apiImage: IImageResponse): IImage {
    const convertedPath = `${urlImg}/${apiImage.path}`;

    return {
      path: convertedPath,
      alt: apiImage.alt,
    };
  }
}
