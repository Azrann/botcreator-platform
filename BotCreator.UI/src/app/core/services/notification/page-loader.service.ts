import { Injectable } from '@angular/core';
import { Router, NavigationStart, NavigationEnd, NavigationCancel, NavigationError } from '@angular/router';
import { Observable, Subject, BehaviorSubject } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class PageLoaderService {
    private loadingStatusSource$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
    public loadingStatus$: Observable<boolean> = this.loadingStatusSource$.asObservable();

    private pagesToLoad: string[] = ['/templates', '/user-bots']
    private pagesToLoadPrefixes: string[] = ['/bot-setting', '/templates/customer-service-bot']

    constructor(private router: Router) {
        router.events.subscribe(routerEvent => {
            /*  if (!event["url"])
                  return;
  
              if (!this.pagesToLoad.find(s => s === event["url"]) && !this.pagesToLoadPrefixes.find(s => event["url"].startsWith(s))) {
                  return;
              }
  
              if (event instanceof NavigationStart) {
                  this.startLoading();
              }
              //if (event instanceof NavigationEnd) {
              //    this.endLoading();
              //}
              if (event instanceof NavigationCancel) {
                  this.stopLoading();
              }
              if (event instanceof NavigationError) {
                  this.stopLoading();
              }*/



            if (routerEvent instanceof NavigationStart) {
                this.startLoading();
            }

            if (routerEvent instanceof NavigationEnd ||
                routerEvent instanceof NavigationCancel ||
                routerEvent instanceof NavigationError) {
                this.stopLoading();
            }
        });
    }

    startLoading() {       
        this.loadingStatusSource$.next(true);
    }

    stopLoading() {
        this.loadingStatusSource$.next(false);
    }
}