import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EventsComponent } from './events.component';
import { Routes, RouterModule } from '@angular/router';
import { SharedModule } from 'src/app/core/shared/shared.module';
import { EventListResolver } from './resolvers/event-list-resolver';

const routes: Routes = [
  {
    path: '', component: EventsComponent, resolve: { resolvedData: EventListResolver }
  }
]

@NgModule({
  declarations: [EventsComponent],
  imports: [
    SharedModule,
    RouterModule.forChild(routes)
  ],
  exports: [EventsComponent]
})
export class EventsModule { }
