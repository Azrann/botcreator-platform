import { NgModule } from '@angular/core';
import { LoginComponent } from './login.component';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from 'src/app/core/shared/shared.module';

const routes: Routes = [
  { path: '', component: LoginComponent }
]

@NgModule({
  declarations: [LoginComponent],
  imports: [
    SharedModule,
    RouterModule.forChild(routes)
  ],
  exports: [LoginComponent]
})
export class LoginModule { 
}
