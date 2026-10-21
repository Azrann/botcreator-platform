import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UIModule } from 'projects/coda/public-api';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { ChartsModule } from 'ng2-charts';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { BotTranslateModule } from '../services/translate/bot-translate.module';
import { SharedComponentsModule } from '../components/shared-components.module';
import { PrismModule } from '@ngx-prism/core';

@NgModule({
    declarations: [],
    imports: [],
    exports: [
        CommonModule,
        UIModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule,
        BotTranslateModule,
        ChartsModule,
        NgxChartsModule,
        SharedComponentsModule,
        PrismModule
    ]
})
export class SharedModule {
}
