import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { ApplyCoupon } from 'src/app/shared/models/apply-coupon';
import { Coupon } from 'src/app/shared/models/coupon';
import { CreateCoupon } from 'src/app/shared/models/create-coupon';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CouponService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getAllCoupons(): Observable<Coupon[]> {
    return this.http.get<Coupon[]>(`${this.baseUrl}coupon/all`);
  }

  createCoupon(coupon: CreateCoupon): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}coupon`, coupon);
  }

  applyCoupon(applyCoupon: ApplyCoupon, callback: () => void): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}coupon/apply`, applyCoupon).pipe(
      map(() => {
        callback();
      })
    );
  }
}
