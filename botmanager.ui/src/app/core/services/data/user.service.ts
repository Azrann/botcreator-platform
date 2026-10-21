import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { RequestService } from '../common/request.service';
import { User } from 'src/app/core/models/common/user/user';
import { UserProfileDto } from '../../models/dto/user/user-profile.dto';
import { map } from 'rxjs/operators';
import { UserProfileView } from '../../models/view/user/user-profile.view';
import { of, Observable } from 'rxjs';
import { plainToClass } from 'class-transformer';

@Injectable({ providedIn: 'root' })
export class UserService {
    constructor(private http: HttpClient, private request: RequestService) { }

    getAll() {
        return this.request.get<User[]>('users');
    }

    getUserProfile(): Observable<UserProfileView> {
        return this.request.get<UserProfileDto>('users/loggedin/userinfo')
            .pipe(map(profileDto => {
                let mapped: UserProfileView = {
                    email: profileDto.email,
                    name: profileDto.firstName + ' ' + profileDto.lastName,
                    subscription: 'free subscription'
                };
                return mapped;
            }));
    }

    register(user: User) {
        return this.request.post<any>('users/register', user);
    }

    sendResetPasswordLink(email: string) {
        return this.request.post(`users/reset-password`, { email: email });
    }

    setNewPassword(password: string, resetToken: string) {
        return this.request.post(`users/set-new-password`, { password: password, token: resetToken });
    }
}