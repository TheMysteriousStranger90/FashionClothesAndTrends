import {Component} from '@angular/core';
import {AccountService} from 'src/app/account/account.service';
import {BasketService} from 'src/app/basket/basket.service';
import { NotificationsService } from 'src/app/notifications/notifications.service';
import {BasketItem} from 'src/app/shared/models/basket';
import { Notification } from '../../shared/models/notification';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.sass']
})
export class NavBarComponent {

  unreadNotificationsCount: number = 0;
  constructor(public basketService: BasketService, public accountService: AccountService, private notificationsService: NotificationsService) {
  }

  ngOnInit(): void {
    this.loadUnreadNotificationsCount();
  }

  getCount(items: BasketItem[]) {
    return items.reduce((sum, item) => sum + item.quantity, 0);
  }

  loadUnreadNotificationsCount(): void {
    this.notificationsService.getUnreadNotificationsByUserId().subscribe({
      next: (notifications: Notification[]) => {
        this.unreadNotificationsCount = notifications.length;
      },
      error: error => console.error('Error fetching unread notifications', error)
    });
  }
}
