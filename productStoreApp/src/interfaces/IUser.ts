import { ICartProduct } from './ICartProduct';
import { IContact } from './IContact';
import { IOrder, IOrderResponse } from './IOrder';

export interface IUser {
  id: number;
  isActive: boolean;
  username: string;
  firstName: string;
  lastName: string;
  discount: number;
  address: string | null;
  cartItems: ICartProduct[];
  contacts: IContact[];
  orders: IOrder[];
}

export interface IUserResponse {
  id: number;
  isActive: boolean;
  username: string;
  password: never;
  firstName: string;
  lastName: string;
  discount: number;
  address: string | null;
  cartItems: ICartProduct[];
  contacts: IContact[];
  orders: IOrderResponse[];
}
