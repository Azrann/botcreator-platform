import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AlertService } from './alert.service';
import { PageLoaderService } from './page-loader.service';

@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ],
  providers: [PageLoaderService, AlertService]
})
export class NotificationModule { }
