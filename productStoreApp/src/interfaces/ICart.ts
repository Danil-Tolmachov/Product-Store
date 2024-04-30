import { ICartItem, ICartItemResponse } from './ICartItem';

export interface ICart {
  items: ICartItem[];
}

export interface ICartResponse {
  items: ICartItemResponse[];
}
