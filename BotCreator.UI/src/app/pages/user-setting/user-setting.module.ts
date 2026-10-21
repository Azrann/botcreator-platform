import { NgModule } from '@angular/core';
import { UserSettingComponent } from './user-setting.component';
import { Routes, RouterModule } from '@angular/router';
import { SharedModule } from 'src/app/core/shared/shared.module';

const routes: Routes = [
  { path: '', component: UserSettingComponent }
]

@NgModule({
  declarations: [UserSettingComponent],
  imports: [
    SharedModule,
    RouterModule.forChild(routes)
  ]
})
export class UserSettingModule { }
