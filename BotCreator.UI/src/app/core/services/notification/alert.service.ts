import { Injectable } from '@angular/core';
import { Router, NavigationStart } from '@angular/router';
import { Observable, Subject } from 'rxjs';
import { AlertMessageEvent, AlertType } from '../../models/common/bot/alert-message-event';

@Injectable({ providedIn: 'root' })
export class AlertService {
    private messageSource$: Subject<AlertMessageEvent> = new Subject<AlertMessageEvent>();
    public message$: Observable<AlertMessageEvent> = this.messageSource$.asObservable();
    private keepAfterNavigationChange = false;

    constructor(private router: Router) {
        // clear alert message on route change
        router.events.subscribe(event => {
            if (event instanceof NavigationStart) {
                if (this.keepAfterNavigationChange) {
                    // only keep for a single location change
                    this.keepAfterNavigationChange = false;
                } else {
                    // clear alert
                    this.messageSource$.next();
                }
            }
        });
    }

    success(message: string, keepAfterNavigationChange = false) {        
        this.keepAfterNavigationChange = keepAfterNavigationChange;
        this.messageSource$.next({ type: AlertType.SUCCESS, text: message });
    }

    error(message: string, keepAfterNavigationChange = false) {
        this.keepAfterNavigationChange = keepAfterNavigationChange;
        this.messageSource$.next({ type: AlertType.ERROR, text: message });
    }
}