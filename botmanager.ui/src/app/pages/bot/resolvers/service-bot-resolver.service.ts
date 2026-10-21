import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { UserBotView } from 'src/app/core/models/view/user-bot-view';
import { BotService } from 'src/app/core/services/data/bot.service';
import { Observable, of } from 'rxjs';
import { delay, catchError } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
})
export class ServiceBotResolver implements Resolve<UserBotView[]> {

    constructor(private botService: BotService) { }

    resolve(route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot): Observable<UserBotView[]> {
        const id = route.paramMap.get('id');
        if (+id === 0) {
            return of(null);
        }

        return this.botService.getCompleteServiceBot(id).pipe(
            catchError(error => {
                const message = `Retrieval error: ${error}`;
                console.error(message);
                return of(null);
            })
        )
    }

}
