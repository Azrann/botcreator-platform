import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CompleteServiceBot } from 'src/app/core/models/dto/bot/complete-service-bot';
import { BotService } from 'src/app/core/services/data/bot.service';
import { PageLoaderService } from 'src/app/core/services/notification/page-loader.service';
import { AlertService } from 'src/app/core/services/notification/alert.service';

@Component({
  selector: 'app-remove-bot',
  templateUrl: './remove-bot.component.html',
  styleUrls: ['./remove-bot.component.scss']
})
export class RemoveBotComponent implements OnInit {

  constructor(private route: ActivatedRoute,
    private botService: BotService,
    private pageLoaderService: PageLoaderService,
    private router: Router,
    private alertService: AlertService) { }

  botName: string;
  botId: number;

  ngOnInit() {
    this.route.data.subscribe(data => {
      const resolvedData: CompleteServiceBot = data['resolvedData'];
      this.onServiceBotRetrieved(resolvedData);
    });
  }

  onServiceBotRetrieved(serviceBot: CompleteServiceBot) {
    if (!serviceBot.bot) {
      return;
    }

    this.botId = serviceBot.bot.id;
    this.botName = serviceBot.bot.name;
  }

  removeBot() {
    this.pageLoaderService.startLoading();
    this.botService.removeBot(this.botId).subscribe(result => {
      this.pageLoaderService.stopLoading();
      this.alertService.success('Bot removed successful', true);
      this.router.navigate(['/vertical/bot/list']);
    }, error => {
      var errMsg = error.error.error.errorMessage;
      this.alertService.error(errMsg);
      this.pageLoaderService.stopLoading();
    });
  }
}
