import {Component, OnInit} from '@angular/core';
import {AccountService} from './account/account.service';
import {User} from './shared/models/user';
import {BasketService} from './basket/basket.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.sass']
})
export class AppComponent implements OnInit {
  title = 'Fashion Clothes And Trends';

  constructor(private accountService: AccountService, private basketService: BasketService,) {
  }

  ngOnInit(): void {
    this.setCurrentUser();
    this.loadBasket();
  }

  setCurrentUser() {
    const userString = localStorage.getItem('user');
    if (!userString) return;
    const user: User = JSON.parse(userString);
    this.accountService.setCurrentUser(user);
  }

  loadBasket() {
    const basketId = localStorage.getItem('basket_id');
    if (basketId) this.basketService.getBasket(basketId);
  }
}
