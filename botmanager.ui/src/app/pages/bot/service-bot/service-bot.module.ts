import { NgModule } from '@angular/core';
import { ServiceBotComponent } from './service-bot.component';
import { CanDeactivateServiceBot } from './service-bot.guard';
import { HttpClient } from '@angular/common/http';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { SharedModule } from 'src/app/core/shared/shared.module';
import { PrismModule } from '@ngx-prism/core';
import { ClipboardModule } from 'ngx-clipboard';

export function createTranslateLoader(http: HttpClient) {
  return new TranslateHttpLoader(http, './assets/i18n/', '.json');
}

@NgModule({
  declarations: [ServiceBotComponent],
  imports: [
    SharedModule,
    PrismModule,
    ClipboardModule
  ],
  providers: [CanDeactivateServiceBot],
  exports: [ServiceBotComponent]
})
export class ServiceBotModule {
}
