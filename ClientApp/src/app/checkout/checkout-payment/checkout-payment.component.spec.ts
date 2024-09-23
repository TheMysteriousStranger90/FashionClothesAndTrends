import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CheckoutPaymentComponent } from './checkout-payment.component';

describe('CheckoutPaymentComponent', () => {
  let component: CheckoutPaymentComponent;
  let fixture: ComponentFixture<CheckoutPaymentComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CheckoutPaymentComponent]
    });
    fixture = TestBed.createComponent(CheckoutPaymentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
