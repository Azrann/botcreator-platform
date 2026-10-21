import { NgModule } from '@angular/core';
import { DashboardComponent } from './dashboard.component';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from 'src/app/core/shared/shared.module';

const routes: Routes = [
  { path: '', component: DashboardComponent }
]

@NgModule({
  declarations: [DashboardComponent],
  imports: [
    SharedModule,
    RouterModule.forChild(routes)
  ],
  exports: [DashboardComponent]
})
export class DashboardModule {
  
}
