import { Component, OnInit } from '@angular/core';
import { NotificationsService } from './notifications.service';
import { Notification } from '../shared/models/notification';

@Component({
  selector: 'app-notifications',
  templateUrl: './notifications.component.html',
  styleUrls: ['./notifications.component.sass']
})
export class NotificationsComponent implements OnInit {
  unreadNotifications: Notification[] = [];

  constructor(private notificationsService: NotificationsService) {}

  ngOnInit(): void {
    this.loadUnreadNotifications();
  }

  loadUnreadNotifications(): void {
    this.notificationsService.getUnreadNotificationsByUserId().subscribe({
      next: notifications => this.unreadNotifications = notifications,
      error: error => console.error('Error fetching unread notifications', error)
    });
  }

  markAsRead(notificationId: string): void {
    this.notificationsService.markNotificationAsRead(notificationId).subscribe({
      next: () => {
        this.unreadNotifications = this.unreadNotifications.filter(n => n.id !== notificationId);
      },
      error: error => console.error('Error marking notification as read', error)
    });
  }
}
