import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { UserBotView } from 'src/app/core/models/view/user-bot-view';
import { BotService } from 'src/app/core/services/data/bot.service';
import { Observable, of } from 'rxjs';
import { switchMap, catchError } from 'rxjs/operators';
import { EventView } from 'src/app/core/models/view/report/event-view';
import { ReportService } from 'src/app/core/services/data/report.service';

@Injectable({
    providedIn: 'root'
})
export class EventListResolver implements Resolve<EventView[]> {

    constructor(private botService: BotService,
        private reportService: ReportService) { }

    resolve(): Observable<EventView[]> {
        return this.botService.getUserBots().pipe(
            switchMap((userBots: UserBotView[]) => {
                if (userBots.length == 0) {
                    return of([]);
                }

                return this.reportService.getEvents(userBots[0].id);
            }),
            catchError(error => {
                const message = `Event List Retrieval error: ${error}`;
                console.error(message);
                return of([]);
            })
        );
    }
}
