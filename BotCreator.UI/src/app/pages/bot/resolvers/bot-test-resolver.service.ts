import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { BotService } from 'src/app/core/services/data/bot.service';
import { Observable, of } from 'rxjs';
import { delay, catchError, map } from 'rxjs/operators';
import { BotWidgetHtml } from 'src/app/core/models/dto/bot/bot-widget-html';
import { BotTestData } from '../bot-test/bot-test-data';

@Injectable({
    providedIn: 'root'
})
export class BotTestResolver implements Resolve<BotTestData> {

    constructor(private botService: BotService) { }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<BotTestData> {
        const botId = route.paramMap.get('id');
        const botName = route.queryParamMap.get('name');
        if (+botId === 0) {
            return of(null);
        }

        return this.botService.getWidgetHtml(botId).pipe(
            map((result: BotWidgetHtml) => {
                let r: BotTestData = {
                    botName: botName,
                    widgetHtml: result.htmlContent
                };

                return r;
            }),
            catchError(error => {
                const message = `Retrieval error: ${error}`;
                console.error(message);
                return of(null);
            })
        );
    }
}
