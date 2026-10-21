import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PageLoaderComponent } from './page-loader/page-loader.component';
import { AlertComponent } from './alert/alert.component';
import { UIModule } from 'projects/coda/public-api';
import { LanguageSelectorComponent } from './language-selector/language-selector.component';
import { BotHtmlcontentComponent } from './bot-htmlcontent/bot-htmlcontent.component';
import { BotTranslateModule } from '../services/translate/bot-translate.module';
import { PrismModule } from '@ngx-prism/core';

@NgModule({
  declarations: [PageLoaderComponent, AlertComponent, AlertComponent, LanguageSelectorComponent, 
    BotHtmlcontentComponent],
  imports: [
    CommonModule,
    UIModule,
    BotTranslateModule,
    PrismModule   
  ],
  exports: [PageLoaderComponent, AlertComponent, LanguageSelectorComponent, BotHtmlcontentComponent]
})
export class SharedComponentsModule { }
