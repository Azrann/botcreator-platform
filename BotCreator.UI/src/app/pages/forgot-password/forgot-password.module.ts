import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ForgotPasswordComponent } from './forgot-password.component';
import { SharedModule } from 'src/app/core/shared/shared.module';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  { path: '', component: ForgotPasswordComponent }
]

@NgModule({
  declarations: [ForgotPasswordComponent],
  imports: [
    SharedModule,
    RouterModule.forChild(routes)
  ],
  exports: [ForgotPasswordComponent]
})
export class ForgotPasswordModule { }
