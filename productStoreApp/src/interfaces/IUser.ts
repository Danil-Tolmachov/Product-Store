import { ICartItem } from './ICartItem';
import { IContact } from './IContact';
import { IOrder, IOrderResponse } from './IOrder';

export interface IUser {
  id: number;
  username: string;
  firstName: string;
  lastName: string;
  discount: number;
  address: string | null;
  cartItems: ICartItem[];
  contacts: IContact[];
  orders: IOrder[];
}

export interface IUserResponse {
  id: number;
  username: string;
  password: never;
  firstName: string;
  lastName: string;
  discount: number;
  address: string | null;
  cartItems: ICartItem[];
  contacts: IContact[];
  orders: IOrderResponse[];
}
