import { IOrderDetail, IOrderDetailResponse } from './IOrderItem';

export interface IOrder {
  id: number;
  userComment: string;
  status: string;
  employeeId: number;
  total: number;
  isCompleted: boolean;
  isCanceled: boolean;
  details: IOrderDetail[];
}

export interface IOrderResponse {
  id: number;
  userComment: string;
  status: string;
  employeeId: number;
  total: number;
  isCompleted: boolean;
  isCanceled: boolean;
  details: IOrderDetailResponse[];
}
