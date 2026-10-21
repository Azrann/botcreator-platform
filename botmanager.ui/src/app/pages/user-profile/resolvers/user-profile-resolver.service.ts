import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { UserBotView } from 'src/app/core/models/view/user-bot-view';
import { BotService } from 'src/app/core/services/data/bot.service';
import { Observable, of } from 'rxjs';
import { delay, catchError } from 'rxjs/operators';
import { UserProfileView } from 'src/app/core/models/view/user/user-profile.view';
import { UserService } from 'src/app/core/services/data/user.service';

@Injectable({
  providedIn: 'root'
})
export class UserProfileResolver implements Resolve<UserProfileView> {

  constructor(private userService: UserService) { }

  resolve(route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<UserProfileView> {

    return this.userService.getUserProfile().pipe(
      catchError(error => {
        const message = `Retrieval error: ${error}`;
        console.error(message);
        return of(null);
      })
    )
  }
}
