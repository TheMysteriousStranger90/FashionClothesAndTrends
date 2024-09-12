import {Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {Category, ClothingParams, Gender, Size} from 'src/app/shared/models/clothing-params';
import {ShopService} from '../shop.service';
import {Brand} from 'src/app/shared/models/brand';
import {ClothingItem} from 'src/app/shared/models/clothing-item';
import {Guid} from 'guid-typescript';
import { MatSelectChange } from '@angular/material/select';
import { PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.sass']
})
export class ShopComponent implements OnInit {

  @ViewChild('search') searchTerm?: ElementRef;
  products: ClothingItem[] = [];
  brands: Brand[] = [];
  clothingParams = new ClothingParams();
  sortOptions = [
    {name: 'Alphabetical', value: 'name'},
    {name: 'Price: Low to high', value: 'priceAsc'},
    {name: 'Price: High to low', value: 'priceDesc'},
  ];
  genderOptions = [null, ...Object.values(Gender)];
  categoryOptions = [null, ...Object.values(Category)];
  sizeOptions = [null, ...Object.values(Size)];
  totalCount = 0;

  constructor(private shopService: ShopService) {
    this.clothingParams = shopService.getShopParams();
  }

  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
  }

  getProducts() {
    this.shopService.getClothingItems().subscribe({
      next: response => {
        this.products = response.data;
        this.totalCount = response.count;
      },
      error: error => console.log(error)
    })
  }



  getBrands() {
    this.shopService.getBrands().subscribe({
      next: response => {
        this.brands = [{id: '', name: 'All', description: ''}, ...response]
      },
      error: error => console.log(error)
    })
  }

  onBrandSelected(brandId: string) {
    const params = this.shopService.getShopParams();
    params.clothingBrandId = brandId;
    params.pageIndex = 1;
    this.shopService.setShopParams(params);
    this.clothingParams = params;
    this.getProducts();
  }

  onSortSelected(event: MatSelectChange) {
    const params = this.shopService.getShopParams();
    params.sort = event.value;

    params.pageIndex = 1;

    this.shopService.setShopParams(params);
    this.clothingParams = params;
    this.getProducts();
  }

  onGenderSelected(event: MatSelectChange) {
    const params = this.shopService.getShopParams();
    params.gender = event.value;
    params.pageIndex = 1; // Reset to first page on filter change
    this.shopService.setShopParams(params);
    this.clothingParams = params;
    this.getProducts();
  }

  onCategorySelected(event: MatSelectChange) {
    const params = this.shopService.getShopParams();
    params.category = event.value;
    params.pageIndex = 1; // Reset to first page on filter change
    this.shopService.setShopParams(params);
    this.clothingParams = params;
    this.getProducts();
  }

  onSizeSelected(event: MatSelectChange) {
    const params = this.shopService.getShopParams();
    params.size = event.value;
    params.pageIndex = 1; // Reset to first page on filter change
    this.shopService.setShopParams(params);
    this.clothingParams = params;
    this.getProducts();
  }

  onPageChanged(event: PageEvent) {
    const params = this.shopService.getShopParams();
    if (params.pageIndex !== event.pageIndex + 1 || params.pageSize !== event.pageSize) {

      params.pageIndex = event.pageIndex + 1;

      params.pageSize = event.pageSize;
      this.shopService.setShopParams(params);
      this.clothingParams = params;
      this.getProducts();
    }
  }

  onSearch() {
    const params = this.shopService.getShopParams();
    params.search = this.searchTerm?.nativeElement.value;
    params.pageIndex = 1;
    this.shopService.setShopParams(params);
    this.clothingParams = params;
    this.getProducts();
  }

  onReset() {
    if (this.searchTerm) this.searchTerm.nativeElement.value = '';
    this.clothingParams = new ClothingParams();
    this.shopService.setShopParams(this.clothingParams);
    this.getProducts();
  }
}
