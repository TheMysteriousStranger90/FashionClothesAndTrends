import { Component, ChangeDetectionStrategy, ChangeDetectorRef, OnInit } from '@angular/core';
import { AccountService } from 'src/app/account/account.service';
import { BasketService } from 'src/app/basket/basket.service';
import { NotificationsService } from 'src/app/notifications/notifications.service';
import { BasketItem } from 'src/app/shared/models/basket';
import { Notification } from '../../shared/models/notification';

@Component({
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: false,
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.sass']
})
export class NavBarComponent implements OnInit {

  unreadNotificationsCount: number = 0;

  constructor(
    public basketService: BasketService,
    public accountService: AccountService,
    private notificationsService: NotificationsService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.accountService.currentUser$.subscribe(user => {
      if (user) this.loadUnreadNotificationsCount();
      else {
        this.unreadNotificationsCount = 0;
        this.cdr.markForCheck();
      }
    });
  }

  getCount(items: BasketItem[]) {
    return items.reduce((sum, item) => sum + item.quantity, 0);
  }

  loadUnreadNotificationsCount(): void {
    this.notificationsService.getUnreadNotificationsByUserId().subscribe({
      next: (notifications: Notification[]) => {
        this.unreadNotificationsCount = notifications.length;
        this.cdr.markForCheck();
      },
      error: error => console.error('Error fetching unread notifications', error)
    });
  }
}
