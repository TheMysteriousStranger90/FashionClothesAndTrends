import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrderTotalsComponent } from './order-totals.component';

describe('OrderTotalsComponent', () => {
  let component: OrderTotalsComponent;
  let fixture: ComponentFixture<OrderTotalsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [OrderTotalsComponent]
    });
    fixture = TestBed.createComponent(OrderTotalsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
