import { NgModule } from '@angular/core';
import { ResetPasswordComponent } from './reset-password.component';
import { SharedModule } from 'src/app/core/shared/shared.module';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  { path: '', component: ResetPasswordComponent }
]

@NgModule({
  declarations: [ResetPasswordComponent],
  imports: [
    SharedModule,
    RouterModule.forChild(routes)
  ],
  exports: [ResetPasswordComponent]
})
export class ResetPasswordModule { }
