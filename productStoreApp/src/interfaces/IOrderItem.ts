export interface IOrderDetail {
  unitPrice: number;
  quantity: number;
  orderId: number;
  productId: number;
  productName: number;
  productDescription: string;
  productCategoryId: number;
  productCategoryName: string;
}

export interface IOrderDetailResponse {
  unitPrice: number;
  quantity: number;
  orderId: number;
  productId: number;
  productName: number;
  productDescription: string;
  productCategoryId: number;
  productCategoryName: string;
}
