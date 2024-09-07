import { Address } from "./address";

export interface OrderToCreate {
  basketId: string;
  deliveryMethodId: string;
  shipToAddress: Address;
}

export interface OrderItem {
  clothingItemId: string;
  clothingItemName: string;
  pictureUrl: string;
  price: number;
  quantity: number;
}

export interface Order {
  id: string;
  buyerEmail: string;
  orderDate: Date;
  shipToAddress: Address;
  deliveryMethod: string;
  shippingPrice: number;
  orderItems: OrderItem[];
  subtotal: number;
  total: number;
  status: string;
}

export interface OrderUpdateDto {
  orderId: string;
  deliveryMethodId?: string;
  shipToAddress: Address;
  orderItems: OrderItem[];
  status: string;
  subtotal?: number;
}
