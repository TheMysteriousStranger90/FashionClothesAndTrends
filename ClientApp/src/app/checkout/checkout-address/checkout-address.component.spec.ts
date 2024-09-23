import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CheckoutAddressComponent } from './checkout-address.component';

describe('CheckoutAddressComponent', () => {
  let component: CheckoutAddressComponent;
  let fixture: ComponentFixture<CheckoutAddressComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CheckoutAddressComponent]
    });
    fixture = TestBed.createComponent(CheckoutAddressComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
