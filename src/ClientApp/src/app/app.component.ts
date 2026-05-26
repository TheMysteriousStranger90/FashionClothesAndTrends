import {Component, OnInit, ChangeDetectionStrategy} from '@angular/core';
import {AccountService} from './account/account.service';
import {BasketService} from './basket/basket.service';

@Component({
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: false,
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.sass']
})
export class AppComponent implements OnInit {
  title = 'Fashion Clothes And Trends';

  constructor(private accountService: AccountService, private basketService: BasketService) {
  }

  ngOnInit(): void {
    this.accountService.loadCurrentUser();
    this.basketService.loadFromStorage();
  }
}