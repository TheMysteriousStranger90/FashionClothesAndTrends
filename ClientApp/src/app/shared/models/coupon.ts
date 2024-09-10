import { Guid } from "guid-typescript";

export interface Coupon {
  id: Guid;
  code: string;
  discountPercentage: number;
  expiryDate: Date;
  isActive: boolean;
}
