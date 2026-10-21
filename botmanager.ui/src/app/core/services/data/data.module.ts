import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthenticationService } from './auth.service';
import { BotService } from './bot.service';
import { UserService } from './user.service';
import { JwtInterceptor } from '../../middleware/jwt-intercepter';
import { ErrorInterceptor } from '../../middleware/error-intercepter';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    HttpClientModule
  ],
  providers: [
    AuthenticationService,
    BotService,
    UserService,
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
  ]
})
export class DataModule { }
