import { IOrderDetail, IOrderDetailResponse } from './IOrderItem';

export interface IOrder {
  id: number;
  userComment: string;
  status: string;
  userUsername: string;
  employeeId: number;
  details: IOrderDetail[];
}

export interface IOrderResponse {
  id: number;
  userComment: string;
  status: string;
  userId: number;
  userUsername: string;
  employeeId: number;
  details: IOrderDetailResponse[];
}
