import { type IProduct } from './IProduct';

export interface ICartItem {
  product: IProduct;
  quantity: number;
}

export interface ICartItemResponse {
  productId: number;
  productName: string;
  productDescription: string;
  productCategoryId: number;
  productCategoryName: string;
  productPrice: number;
  productDiscount: number;

  quantity: number;

  cartId: number;
  cartUserId: number;
}
