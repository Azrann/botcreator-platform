import { NgModule } from '@angular/core';
import { UserProfileComponent } from './user-profile.component';
import { Routes, RouterModule } from '@angular/router';
import { UserProfileResolver } from './resolvers/user-profile-resolver.service';
import { SharedModule } from 'src/app/core/shared/shared.module';

const routes: Routes = [
  { path: '', component: UserProfileComponent,  resolve: { resolvedData: UserProfileResolver } }
]


@NgModule({
  declarations: [UserProfileComponent],
  imports: [
    SharedModule,
    RouterModule.forChild(routes)
  ]
})
export class UserProfileModule { }
