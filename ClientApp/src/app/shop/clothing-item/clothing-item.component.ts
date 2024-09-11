import { Component, Input } from '@angular/core';
import { BasketService } from 'src/app/basket/basket.service';
import { ClothingItem } from 'src/app/shared/models/clothing-item';

@Component({
  selector: 'app-clothing-item',
  templateUrl: './clothing-item.component.html',
  styleUrls: ['./clothing-item.component.sass']
})
export class ClothingItemComponent {

  @Input() product?: ClothingItem;

  constructor(private basketService: BasketService) { }

  addItemToBasket() {
    this.product && this.basketService.addItemToBasket(this.product);
  }
}
