import { ICartItem, ICartItemResponse } from './ICartItem';

export interface ICart {
  items: ICartItem[];
  total: number;
}

export interface ICartResponse {
  items: ICartItemResponse[];
  total: number;
}
