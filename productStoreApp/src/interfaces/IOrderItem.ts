import { IProduct, IProductResponse } from './IProduct';

export interface IOrderDetail {
  unitPrice: number;
  quantity: number;
  product: IProduct;
}

export interface IOrderDetailResponse {
  unitPrice: number;
  quantity: number;
  product: IProductResponse;
}
