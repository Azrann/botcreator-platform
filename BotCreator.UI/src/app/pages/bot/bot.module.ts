import { NgModule } from '@angular/core';
import { BotListComponent } from './bot-list/bot-list.component';
import { Routes, RouterModule } from '@angular/router';
import { ServiceBotComponent } from './service-bot/service-bot.component';
import { BotListResolver } from './resolvers/bot-list-resolver.service';
import { ServiceBotResolver } from './resolvers/service-bot-resolver.service';
import { CanDeactivateServiceBot } from './service-bot/service-bot.guard';
import { ServiceBotModule } from './service-bot/service-bot.module';
import { SharedModule } from 'src/app/core/shared/shared.module';
import { RemoveBotComponent } from './remove-bot/remove-bot.component';
import { BotTestComponent } from './bot-test/bot-test.component';
import { BotTestResolver } from './resolvers/bot-test-resolver.service';

const routes: Routes = [
  {
    path: 'list',
    component: BotListComponent,
    resolve: { resolvedData: BotListResolver }
  },
  {
    path: 'bot-test/:id',
    component: BotTestComponent,
    resolve: { resolvedData: BotTestResolver }
  },
  {
    path: 'service-bot/:id/edit',
    component: ServiceBotComponent,
    resolve: { resolvedData: ServiceBotResolver },
    canDeactivate: [CanDeactivateServiceBot]
  },
  {
    path: 'service-bot/new',
    component: ServiceBotComponent,
    canDeactivate: [CanDeactivateServiceBot]
  },
  {
    path: 'service-bot/:id/remove',
    component: RemoveBotComponent,
    resolve: { resolvedData: ServiceBotResolver }
  },
]

@NgModule({
  declarations: [BotListComponent, RemoveBotComponent, BotTestComponent],
  imports: [
    SharedModule,
    ServiceBotModule,
    RouterModule.forChild(routes)
  ]
})
export class BotModule {
}
