import {Component, ChangeDetectionStrategy} from '@angular/core';
import {BasketService} from 'src/app/basket/basket.service';

@Component({
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: false,
  selector: 'app-order-totals',
  templateUrl: './order-totals.component.html',
  styleUrls: ['./order-totals.component.sass']
})
export class OrderTotalsComponent {

  constructor(public basketService: BasketService) {
  }

}
