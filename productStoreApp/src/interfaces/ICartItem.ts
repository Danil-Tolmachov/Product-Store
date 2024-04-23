import { IProductResponse, type IProduct } from './IProduct';

export interface ICartItem {
  product: IProduct;
  quantity: number;
}

export interface ICartItemResponse {
  product: IProductResponse;
  quantity: number;
}
