import { Component, OnInit } from '@angular/core';
import { AlertMessageEvent, AlertType } from '../../models/common/bot/alert-message-event';
import { AlertService } from '../../services/notification/alert.service';
import { Subscription } from 'rxjs';
import { INotification } from 'projects/coda/src/app/interfaces/notification';

@Component({
  selector: 'app-alert',
  templateUrl: './alert.component.html',
  styleUrls: ['./alert.component.scss']
})
export class AlertComponent implements OnInit {

  private subscription: Subscription;
  message: AlertMessageEvent;
  notifications: INotification[];

  constructor(private alertService: AlertService) { }

  ngOnInit() {
    this.subscription = this.alertService.message$.subscribe(message => {
      this.message = message;
      this.handleNotificationMessage(this.message);
    });
  }

  handleNotificationMessage(message: AlertMessageEvent) {
    if (!message) {
      return;
    }
    if (message.type === AlertType.ERROR) {
      this.notifications = this.constructErrorMessage(message);
    } else if (message.type === AlertType.SUCCESS) {
      this.notifications = this.constructSuccessMessage(message);
    }
  }

  constructErrorMessage(message: AlertMessageEvent): INotification[] {
    return [
      {
        content: message.text,
        position: 'topRight',
        title: 'Error',
        animation: 'fromTop',
        hideTimeout: 5000,
        autohide: true,
        progress: false,
        view: 'error',
        icon: 'icofont-worried'
      }
    ]
  }

  constructSuccessMessage(message: AlertMessageEvent): INotification[] {
    return [
      {
        content: message.text,
        position: 'topRight',
        title: 'Success',
        animation: 'fromTop',
        hideTimeout: 5000,
        autohide: true,
        progress: false,
        view: 'success',
        icon: 'icofont-wink-smile'
      }
    ]
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}
