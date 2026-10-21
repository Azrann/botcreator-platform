import { NgModule } from '@angular/core';
import { SignUpComponent } from './sign-up.component';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from 'src/app/core/shared/shared.module';

const routes: Routes = [
  { path: '', component: SignUpComponent }
]

@NgModule({
  imports: [
    SharedModule,
    RouterModule.forChild(routes)
  ],
  declarations: [
    SignUpComponent
  ],
  exports: [SignUpComponent]

})
export class SignUpModule {
}
