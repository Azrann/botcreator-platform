import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule, HTTP_INTERCEPTORS, HttpClient } from '@angular/common/http';
import { JwtInterceptor } from '../../middleware/jwt-intercepter';
import { ErrorInterceptor } from '../../middleware/error-intercepter';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { SharedModule } from '../../shared/shared.module';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';

export function createTranslateLoader(http: HttpClient) {
    return new TranslateHttpLoader(http, './assets/i18n/', '.json');
}

@NgModule({
    declarations: [],
    imports: [],
    providers: [],
    exports: [TranslateModule]
})
export class BotTranslateModule {
    static forRoot() {
        return TranslateModule.forRoot({
            loader: {
                provide: TranslateLoader,
                useFactory: (createTranslateLoader),
                deps: [HttpClient]
            }
        });
    }
}
