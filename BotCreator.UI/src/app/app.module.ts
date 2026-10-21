import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SignUpModule } from './pages/sign-up/sign-up.module';
import { DashboardModule } from './pages/dashboard/dashboard.module';
import { DataModule } from './core/services/data/data.module';
import { LoginModule } from './pages/login/login.module';
import { SharedComponentsModule } from './core/components/shared-components.module';
import { BotLayoutModule } from './layouts/bot-layout.module';
import { BotModule } from './pages/bot/bot.module';
import { BotTranslateModule } from './core/services/translate/bot-translate.module';
import { SharedModule } from './core/shared/shared.module';
import { ForgotPasswordModule } from './pages/forgot-password/forgot-password.module';
import { NotificationModule } from './core/services/notification/notification.module';
import { ResetPasswordModule } from './pages/reset-password/reset-password.module';
import { EventsModule } from './pages/events/events.module';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    DataModule,    
    NotificationModule,
    BotTranslateModule.forRoot(),
    SharedModule,
    BotLayoutModule,

    LoginModule,
    SignUpModule,
    ForgotPasswordModule,
    ResetPasswordModule,
    DashboardModule,
    BotModule,
    EventsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
