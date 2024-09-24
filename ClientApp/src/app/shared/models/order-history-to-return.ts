import { OrderItemHistory } from "./order-history";

export interface OrderHistoryToReturn {
  id: string;
  orderDate: string;
  totalAmount: number;
  status: string;
  shippingAddress: string;
  orderItems: OrderItemHistory[];
}
