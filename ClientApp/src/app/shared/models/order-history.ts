import { Guid } from "guid-typescript";

export interface OrderHistory {
  id: Guid;
  orderDate: Date;
  totalAmount: number;
  status: string;
  shippingAddress: string;
  orderItemsHisory: OrderItemHistory[];
}

export interface OrderItemHistory {
  id: Guid;
  clothingItemId: string;
  clothingItemName: string;
  quantity: number;
  priceAtPurchase: number;
}
