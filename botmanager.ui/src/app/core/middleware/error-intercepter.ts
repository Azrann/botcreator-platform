import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable, throwError, BehaviorSubject, pipe } from 'rxjs';
import { catchError, filter, take, switchMap } from 'rxjs/operators';
import { AuthenticationService } from '../services/data/auth.service';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    private refreshTokenInProgress = false;

    private refreshTokenSubject$: BehaviorSubject<any> = new BehaviorSubject<any>(null);

    private refreshToken$: Observable<any> = this.refreshTokenSubject$.asObservable();

    constructor(private authenticationService: AuthenticationService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request).pipe(catchError(error => {
            if (request.url.includes("authenticate") ||
                request.url.includes('refresh-access-token')) {
                this.logout();
                return throwError(error);
            }

            if (error.status !== 401) {
                return throwError(error);
            }

            if (this.refreshTokenInProgress) {
                return this.refreshToken$
                    .pipe(
                        filter(result => result !== null),
                        take(1),
                        switchMap(() => next.handle(this.addAuthenticationToken(request)))
                    );
            }

            this.refreshTokenInProgress = true;
            this.refreshTokenSubject$.next(null);

            return this.authenticationService.reAuthenticate().pipe(
                switchMap((token: any) => {
                    this.refreshTokenInProgress = false;
                    this.refreshTokenSubject$.next(token);

                    return next.handle(this.addAuthenticationToken(request));
                }),
                catchError((err: any) => {
                    this.refreshTokenInProgress = false;
                    this.logout();
                    return throwError(error);
                })
            );
        }))
    }

    private logout() {
        this.authenticationService.logout();
        location.reload(true);
    }

    private addAuthenticationToken(request: HttpRequest<any>) {
        const accessToken = this.authenticationService.currentUserValue.token;

        if (!accessToken) {
            return request;
        }

        return request.clone({
            setHeaders: {
                Authorization: `Bearer ${accessToken}`
            }
        });
    }
}