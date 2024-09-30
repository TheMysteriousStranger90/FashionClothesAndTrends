import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { CouponService } from 'src/app/core/services/coupon.service';
import { ApplyCoupon } from 'src/app/shared/models/apply-coupon';
import { ClothingItem } from 'src/app/shared/models/clothing-item';
import { Coupon } from 'src/app/shared/models/coupon';
import { ShopService } from 'src/app/shop/shop.service';

@Component({
  selector: 'app-apply-coupon',
  templateUrl: './apply-coupon.component.html',
  styleUrls: ['./apply-coupon.component.sass']
})
export class ApplyCouponComponent implements OnInit {
  clothingItems: ClothingItem[] = [];
  coupons: Coupon[] = [];
  selectedClothingItem: ClothingItem | null = null;
  selectedCoupon: Coupon | null = null;

  constructor(private couponService: CouponService, private shopService: ShopService) {}

  ngOnInit(): void {
    this.loadClothingItems();
  }

  loadClothingItems(): void {
    this.shopService.getAllClothingItems().subscribe({
      next: (items) => this.clothingItems = items,
      error: (error) => console.error('Error loading clothing items', error)
    });
  }

  onClothingItemSelect(item: ClothingItem): void {
    this.selectedClothingItem = item;
    this.loadCoupons();
  }

  loadCoupons(): void {
    this.couponService.getAllCoupons().subscribe({
      next: (coupons) => this.coupons = coupons,
      error: (error) => console.error('Error loading coupons', error)
    });
  }

  onCouponSelect(coupon: Coupon): void {
    this.selectedCoupon = coupon;
  }

  applyCoupon(): void {
    if (this.selectedClothingItem && this.selectedCoupon) {
      const applyCoupon: ApplyCoupon = {
        clothingItemId: this.selectedClothingItem.id,
        couponCodeId: this.selectedCoupon.id
      };

      this.couponService.applyCoupon(applyCoupon).subscribe({
        next: () => {
          console.log('Coupon applied successfully');
          this.loadClothingItems();
        },
        error: (error) => console.error('Error applying coupon', error)
      });
    }
  }
}
