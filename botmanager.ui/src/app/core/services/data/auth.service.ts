import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { RequestService } from '../common/request.service';
import { User } from 'src/app/core/models/common/user/user';
import { LoginDto } from 'src/app/core/models/dto/login.dto';


@Injectable({ providedIn: 'root' })
export class AuthenticationService {
    private currentUserSource$: BehaviorSubject<User> = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser')));
    public currentUser$: Observable<User> = this.currentUserSource$.asObservable();

    constructor(private http: HttpClient, private request: RequestService) {
    }

    public get currentUserValue(): User {
        return this.currentUserSource$.value;
    }

    reAuthenticate(): Observable<LoginDto> {
        return this.request.post<LoginDto>(`users/refresh-access-token`,
            { accessToken: this.currentUserValue.token, refreshToken: this.currentUserValue.refreshToken })
            .pipe(map(user => {
                this.handleAuthenticateRequest(user);
                return user;
            }));
    }

    login(email: string, password: string, keepSigned: boolean): Observable<LoginDto> {
        return this.request.post<LoginDto>(`users/authenticate`, { email, password, keepSigned })
            .pipe(map(user => {
                this.handleAuthenticateRequest(user);
                return user;
            }));
    }

    logout() {
        // remove user from local storage to log user out
        localStorage.removeItem('currentUser');
        this.currentUserSource$.next(null);
    }

    private handleAuthenticateRequest(loginUser: LoginDto) {
        localStorage.setItem('currentUser', JSON.stringify(loginUser));

        let userToSave: any = {
            email: loginUser.email,
            token: loginUser.token,
            firstName: loginUser.firstName,
            lastName: loginUser.lastName,
            refreshToken: loginUser.refreshToken
        }

        this.currentUserSource$.next(userToSave);

    }
}