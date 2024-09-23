import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CheckoutDeliveryComponent } from './checkout-delivery.component';

describe('CheckoutDeliveryComponent', () => {
  let component: CheckoutDeliveryComponent;
  let fixture: ComponentFixture<CheckoutDeliveryComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CheckoutDeliveryComponent]
    });
    fixture = TestBed.createComponent(CheckoutDeliveryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
