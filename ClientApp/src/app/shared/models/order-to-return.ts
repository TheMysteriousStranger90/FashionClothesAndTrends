import { Address } from "./address";
import { OrderItem } from "./order";

export interface OrderToReturn {
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
