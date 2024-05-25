import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { type Observable, map, BehaviorSubject, catchError, EMPTY } from 'rxjs';
import {
  type ICategory,
  type ICategoryResponse,
} from '../interfaces/ICategory';
import { type IProduct, type IProductResponse } from '../interfaces/IProduct';
import environment from '../environments/environment';
import { IImage, IImageResponse } from '../interfaces/IImage';

const url = environment.apiUrl;
const urlImg = `${url}/image/product`;

@Injectable({
  providedIn: 'root',
})
export default class CategoryService {
  private categoriesSubject = new BehaviorSubject<ICategory[]>([]);

  public categories: Observable<ICategory[]> =
    this.categoriesSubject.asObservable();

  constructor(private readonly http: HttpClient) {}

  /**
   * Retrieves all categories from the server.
   * @returns {Observable<ICategory[]>} An observable emitting an array of categories.
   */
  getCategories(): Observable<ICategory[]> {
    const link = `${url}/category`;

    return this.http.get<ICategoryResponse[]>(link).pipe(
      map((response) =>
        response.map((category) => CategoryService.adaptCategory(category))
      ),
      catchError((error, caught) => {
        if (error.status === 0) {
          return EMPTY;
        }

        return caught;
      })
    );
  }

  /**
   * Retrieves a category by its ID from the server.
   * @param {number} id - The ID of the category to retrieve.
   * @returns {Observable<ICategory>} An observable emitting the category with the specified ID.
   */
  getCategory(id: number): Observable<ICategory> {
    const link = `${url}/category/${id}`;

    return this.http.get<ICategoryResponse>(link).pipe(
      map((category) => CategoryService.adaptCategory(category)),
      catchError((error, caught) => {
        if (error.status === 0) {
          return EMPTY;
        }

        return caught;
      })
    );
  }

  /**
   * Adapts a category received from the server to the client-side model.
   * @param {ICategoryResponse} apiCategory - The category received from the server.
   * @returns {ICategory} The adapted client-side category.
   */
  static adaptCategory(apiCategory: ICategoryResponse): ICategory {
    return {
      id: apiCategory.id,
      name: apiCategory.name,
      items: (apiCategory.products ?? []).map((product) =>
        CategoryService.adaptProduct(product)
      ),
    };
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
