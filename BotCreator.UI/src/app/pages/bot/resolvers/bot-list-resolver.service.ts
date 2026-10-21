import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { UserBotView } from 'src/app/core/models/view/user-bot-view';
import { BotService } from 'src/app/core/services/data/bot.service';
import { Observable, of } from 'rxjs';
import { delay, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class BotListResolver implements Resolve<UserBotView[]> {

  constructor(private botService: BotService) { }

  resolve(route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<UserBotView[]> {

    return this.botService.getUserBots().pipe(
      catchError(error => {
        const message = `Retrieval error: ${error}`;
        console.error(message);
        return of([]);
      })
    )
  }

}
