import { Guid } from "guid-typescript";

export interface Coupon {
  id: string;
  code: string;
  discountPercentage: number;
  expiryDate: Date;
  isActive: boolean;
}
