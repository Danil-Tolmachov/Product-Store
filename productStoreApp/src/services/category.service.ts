import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { type Observable, map } from 'rxjs';
import {
  type ICategory,
  type ICategoryResponse,
} from '../interfaces/ICategory';
import { type IProduct, type IProductResponse } from '../interfaces/IProduct';
import environment from '../environments/environment';

const url = environment.apiUrl;
const urlImg = `${url}/image/product`;

@Injectable({
  providedIn: 'root',
})
export default class CategoryService {
  constructor(private readonly http: HttpClient) {}

  getCategories(): Observable<ICategory[]> {
    const link = `${url}/category`;

    return this.http
      .get<ICategoryResponse[]>(link)
      .pipe(
        map((response) =>
          response.map((category) => CategoryService.adaptCategory(category))
        )
      );
  }

  getCategory(id: number): Observable<ICategory> {
    const link = `${url}/category/${id}`;

    return this.http
      .get<ICategoryResponse>(link)
      .pipe(map((category) => CategoryService.adaptCategory(category)));
  }

  static adaptCategory(apiCategory: ICategoryResponse): ICategory {
    return {
      id: apiCategory.id,
      name: apiCategory.name,
      items: (apiCategory.products ?? []).map((product) =>
        CategoryService.adaptProduct(product)
      ),
    };
  }

  static adaptProduct(apiProduct: IProductResponse): IProduct {
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
