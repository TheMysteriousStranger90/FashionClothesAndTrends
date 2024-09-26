import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {HttpTransportType, HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';
import { Notification } from '../shared/models/notification';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NotificationsService {
  baseUrl = environment.apiUrl;
  hubUrl = environment.hubUrl;
  private hubConnection?: HubConnection;

  constructor(private http: HttpClient, private toastr: ToastrService) { }

  startConnection(userId: string) {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(this.hubUrl, {
        skipNegotiation: true,
        transport: HttpTransportType.WebSockets
      })
      .build();

    this.hubConnection.start()
      .then(() => {
        console.log('Connection started');
        this.subscribeToUserNotifications(userId);
      })
      .catch(err => console.log('Error while starting connection: ' + err));

    this.hubConnection.on('SendMessage', (notification: Notification) => {
      console.log('Notification received:', notification);
      this.toastr.info(notification.text, 'New Notification');
    });

    this.hubConnection.onclose(error => {
      console.error('SignalR connection closed:', error);
      setTimeout(() => this.startConnection(userId), 5000);
    });
  }

  private subscribeToUserNotifications(userId: string) {
    this.hubConnection?.invoke('SubscribeToUser', userId)
      .catch(err => console.error('Error while subscribing to user notifications: ' + err));
  }



  getNotificationsByUserId(): Observable<Notification[]> {
    return this.http.get<Notification[]>(`${this.baseUrl}notifications/user/notifications`);
  }

  getUnreadNotificationsByUserId(): Observable<Notification[]> {
    return this.http.get<Notification[]>(`${this.baseUrl}notifications/user/notifications/unread`);
  }
}
