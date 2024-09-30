import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CouponService } from 'src/app/core/services/coupon.service';
import { Coupon } from 'src/app/shared/models/coupon';
import { CreateCoupon } from 'src/app/shared/models/create-coupon';

@Component({
  selector: 'app-create-coupon',
  templateUrl: './create-coupon.component.html',
  styleUrls: ['./create-coupon.component.sass']
})
export class CreateCouponComponent implements OnInit {
  couponForm: FormGroup;
  coupons: Coupon[] = [];

  constructor(private fb: FormBuilder, private couponService: CouponService) {
    this.couponForm = this.fb.group({
      discountPercentage: ['', [Validators.required, Validators.min(1), Validators.max(100)]],
      expiryDate: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.loadCoupons();
  }

  loadCoupons(): void {
    this.couponService.getAllCoupons().subscribe({
      next: (coupons) => this.coupons = coupons,
      error: (err) => console.error(err)
    });
  }

  createCoupon(): void {
    if (this.couponForm.valid) {
      const newCoupon: CreateCoupon = this.couponForm.value;
      this.couponService.createCoupon(newCoupon).subscribe({
        next: () => {
          this.loadCoupons();
          this.couponForm.reset();
        },
        error: (err) => console.error(err)
      });
    }
  }
}
