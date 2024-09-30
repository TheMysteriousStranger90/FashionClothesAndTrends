import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateCouponComponent } from './create-coupon.component';

describe('CreateCouponComponent', () => {
  let component: CreateCouponComponent;
  let fixture: ComponentFixture<CreateCouponComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CreateCouponComponent]
    });
    fixture = TestBed.createComponent(CreateCouponComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
