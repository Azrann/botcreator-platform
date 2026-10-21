import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of, from, forkJoin, pipe } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';
import { RequestService } from '../common/request.service';
import { BotTemplate } from 'src/app/core/models/dto/bot/bot-template';
import { BotTemplateView } from 'src/app/core/models/view/bot-template-view';
import { EventView } from '../../models/view/report/event-view';
import { EventDto } from '../../models/dto/report/event-dto';
import { plainToClass } from 'class-transformer';

@Injectable({ providedIn: 'root' })
export class ReportService {

    constructor(
        private http: HttpClient,
        private request: RequestService) { }

    getEvents(botId: number): Observable<EventView[]> {
        return this.request.get<EventView[]>(`api/report/${botId}`)
            .pipe(map((data: EventDto[]): EventView[] => {
                if (!data)
                    return [];
                return data.map<EventView>((event: EventDto): EventView => {
                    return plainToClass(EventView, {
                        botId: event.botId,
                        clientId: event.clientId,
                        createdOn: event.createdOn,
                        dataJson: event.dataJson,
                        name: event.name,
                        username: event.username,
                        dataObj: event.dataJson ? JSON.parse(event.dataJson) : null,
                    })
                })
            }))
    }
}