import { Component, OnInit, EventEmitter } from '@angular/core';
import { BotService } from 'src/app/core/services/data/bot.service';
import { UserBotView } from 'src/app/core/models/view/user-bot-view';
import { delay } from 'rxjs/operators';
import { ActivatedRoute, Router } from '@angular/router';
import { debug } from 'util';

@Component({
  selector: 'app-bot-list',
  templateUrl: './bot-list.component.html',
  styleUrls: ['./bot-list.component.scss']
})
export class BotListComponent implements OnInit {
  userBots: UserBotView[];
  isActive: boolean;
  loadingActivateButton: boolean = false;
  config = {
    sorting: false,
    filtering: false
  };
  closeActionDropdown: EventEmitter<boolean> = new EventEmitter<boolean>();

  constructor(private botService: BotService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit() {
    const resolvedData: UserBotView[] = this.route.snapshot.data['resolvedData'];
    this.onBotListRetrieved(resolvedData);
  }
  onBotListRetrieved(resolvedData: UserBotView[]) {
    this.userBots = resolvedData;
  }

  onEditAction(row: UserBotView) {
    this.closeActionDropdown.emit(true);
    setTimeout(() => {
      this.router.navigate([`/vertical/bot/service-bot/${row.id}/edit`]);
    });
  }

  onRemoveAction(row: UserBotView) {
    this.closeActionDropdown.emit(true);
    setTimeout(() => {
      this.router.navigate([`/vertical/bot/service-bot/${row.id}/remove`]);
    });
  }

  onTestAction(row: UserBotView) {
    this.closeActionDropdown.emit(true);
    setTimeout(() => {
      this.router.navigate([`/vertical/bot/bot-test/${row.id}`], { queryParams: { name: row.name } });
    });
  }


  onActionCloseDropdown() {
    this.closeActionDropdown.emit(true);
  }

  onBotNameClick(botName: string) {
    let bot = this.userBots.find(bot => bot.name === botName);

    this.router.navigate([`/vertical/bot/service-bot/${bot.id}/edit`]);
  }

  toggleActivation(row: UserBotView) {
    row.isActivating = true;

    this.botService.setBotActive(row.id.toString(), !row.isActive).subscribe(() => {
      row.isActivating = false;
      row.isActive = !row.isActive;
    });
  }
}
