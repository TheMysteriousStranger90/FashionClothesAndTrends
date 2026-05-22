import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CheckoutReviewComponent } from './checkout-review.component';

describe('CheckoutReviewComponent', () => {
  let component: CheckoutReviewComponent;
  let fixture: ComponentFixture<CheckoutReviewComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CheckoutReviewComponent]
    });
    fixture = TestBed.createComponent(CheckoutReviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
