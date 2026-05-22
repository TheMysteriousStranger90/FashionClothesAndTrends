import { TestBed } from '@angular/core/testing';

import { OrdersHistoryService } from './orders-history.service';

describe('OrdersHistoryService', () => {
  let service: OrdersHistoryService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OrdersHistoryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
