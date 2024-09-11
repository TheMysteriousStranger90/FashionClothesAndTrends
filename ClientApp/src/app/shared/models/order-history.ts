
export interface OrderHistory {
  id: string;
  orderDate: Date;
  totalAmount: number;
  status: string;
  shippingAddress: string;
  orderItemsHisory: OrderItemHistory[];
}

export interface OrderItemHistory {
  id: string;
  clothingItemId: string;
  clothingItemName: string;
  quantity: number;
  priceAtPurchase: number;
}
