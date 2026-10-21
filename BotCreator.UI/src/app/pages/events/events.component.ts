import { Component, OnInit } from '@angular/core';
import { ReportService } from 'src/app/core/services/data/report.service';
import { BotService } from 'src/app/core/services/data/bot.service';
import { switchMap, catchError } from 'rxjs/operators';
import { of } from 'rxjs';
import { UserBotView } from 'src/app/core/models/view/user-bot-view';
import { ActivatedRoute } from '@angular/router';
import { EventView } from 'src/app/core/models/view/report/event-view';
import { getLocaleDateTimeFormat } from '@angular/common';

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.scss']
})
export class EventsComponent implements OnInit {
  rows: Array<any> = [];

  constructor(private reportService: ReportService,
    private botService: BotService,
    private route: ActivatedRoute) { }

  ngOnInit() {
    setTimeout(() => {
      this.rows = this.route.snapshot.data['resolvedData'];
    });
  }
}
