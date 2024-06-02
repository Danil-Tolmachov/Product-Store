import { type IProductResponse, type IProduct } from './IProduct';

export interface ICategory {
  id: number;
  name: string;
  items: IProduct[] | null;
}

export interface ICategoryResponse {
  id: number;
  name: string;
  products: IProductResponse[] | null;
}
