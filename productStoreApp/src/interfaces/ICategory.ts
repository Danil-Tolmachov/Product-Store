import { type IProductResponse, type IProduct } from './IProduct';

export interface ICategory {
  id: number;
  name: string;
  items: never[] | IProduct[];
}

export interface ICategoryResponse {
  id: number;
  name: string;
  products: never[] | IProductResponse[];
}
