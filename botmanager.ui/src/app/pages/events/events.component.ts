import { Component, OnInit, ViewChild } from '@angular/core';
import { ReportService } from 'src/app/core/services/data/report.service';
import { BotService } from 'src/app/core/services/data/bot.service';
import { ActivatedRoute } from '@angular/router';
import { TCColumnComponent } from 'projects/coda/src/app/ui/components/table/column/column.component';

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.scss']
})
export class EventsComponent implements OnInit {

  @ViewChild('createdOnColumn', { static: true })
  createdOnColumn: TCColumnComponent;

  rows: Array<any> = [];

  constructor(private reportService: ReportService,
    private botService: BotService,
    private route: ActivatedRoute) { }

  ngOnInit() {
    this.createdOnColumn.config.sort = 'desc';
    setTimeout(() => {
      this.rows = this.route.snapshot.data['resolvedData'];
    });
  }
}
