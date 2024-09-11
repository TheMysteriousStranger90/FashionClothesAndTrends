import {Guid} from "guid-typescript";

export interface ClothingParams {
  pageIndex: number;
  pageSize: number;
  clothingBrandId?: string;
  gender?: Gender;
  size?: Size;
  category?: Category;
  sort: string;
  search: string;
}

export enum Gender {
  Male = 'Male',
  Female = 'Female',
  Kids = 'Kids'
}

export enum Category {
  Top = 'Top',
  Bottom = 'Bottom',
  Outerwear = 'Outerwear',
  Accessories = 'Accessories',
  Shoes = 'Shoes',
  Bags = 'Bags',
  Jewelry = 'Jewelry'
}

export enum Size {
  XS = 'XS',
  S = 'S',
  M = 'M',
  L = 'L',
  XL = 'XL',
  XXL = 'XXL'
}

export class ClothingParams implements ClothingParams {
  pageIndex = 1;
  pageSize = 3;
  maxPageSize = 50;
  clothingBrandId?: string;
  gender?: Gender;
  size?: Size;
  category?: Category;
  sort = 'name';
  search = '';
}
