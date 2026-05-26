export interface OrderHistory {
  id: string;
  orderDate: Date;
  totalAmount: number;
  status: string;
  shippingAddress: string;
  orderItems: OrderItemHistory[];
}

export interface OrderItemHistory {
  id: string;
  clothingItemId: string;
  clothingItemName: string;
  quantity: number;
  priceAtPurchase: number;
}
