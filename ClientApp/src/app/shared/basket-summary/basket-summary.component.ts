import {Component, EventEmitter, Input, Output} from '@angular/core';
import {BasketItem} from '../models/basket';
import {BasketService} from 'src/app/basket/basket.service';

@Component({
  selector: 'app-basket-summary',
  templateUrl: './basket-summary.component.html',
  styleUrls: ['./basket-summary.component.sass']
})
export class BasketSummaryComponent {
  @Output() addItem = new EventEmitter<BasketItem>();
  @Output() removeItem = new EventEmitter<{ id: string, quantity: number }>();
  @Input() isBasket = true;

  displayedColumns: string[] = ['product', 'price', 'quantity', 'total', 'remove'];

  constructor(public basketService: BasketService) {
  }

  addBasketItem(item: BasketItem) {
    this.addItem.emit(item);
  }

  removeBasketItem(id: string, quantity = 1) {
    this.removeItem.emit({id, quantity});
  }
}
