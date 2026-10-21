import { NgModule } from '@angular/core';
import { NotFoundComponent } from './not-found.component';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from 'src/app/core/shared/shared.module';

const routes: Routes = [
  { path: '', component: NotFoundComponent }
]

@NgModule({
  declarations: [NotFoundComponent],
  imports: [
    SharedModule,
    RouterModule.forChild(routes)
  ]
})
export class NotFoundModule { }
