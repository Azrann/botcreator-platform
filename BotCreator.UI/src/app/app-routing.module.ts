import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './core/guards/auth.guard';
import { MainLayoutComponent } from './layouts/main-layout/main-layout.component';
import { ExtraLayoutComponent } from './layouts/extra-layout/extra-layout.component';

const defaultRoutes: Routes = [
  {
    path: 'dashboard',
    loadChildren: () => import('./pages/dashboard/dashboard.module').then(m => m.DashboardModule)
  },
  {
    path: 'bot',
    loadChildren: () => import('./pages/bot/bot.module').then(m => m.BotModule)
  },
  {
    path: 'user-profile',
    loadChildren: () => import('./pages/user-profile/user-profile.module').then(m => m.UserProfileModule)
  },
  {
    path: 'user-setting',
    loadChildren: () => import('./pages/user-setting/user-setting.module').then(m => m.UserSettingModule)
  },
  {
    path: 'events',
    loadChildren: () => import('./pages/events/events.module').then(m => m.EventsModule)
  }
]

const extraRoutes: Routes = [
  {
    path: 'sign-up',
    loadChildren: () => import('./pages/sign-up/sign-up.module').then(m => m.SignUpModule)
  },
  {
    path: 'login',
    loadChildren: () => import('./pages/login/login.module').then(m => m.LoginModule)
  },
  {
    path: 'forgot-password',
    loadChildren: () => import('./pages/forgot-password/forgot-password.module').then(m => m.ForgotPasswordModule)
  },
  {
    path: 'reset-password',
    loadChildren: () => import('./pages/reset-password/reset-password.module').then(m => m.ResetPasswordModule)
  }
]

const routes: Routes = [
  {
    path: '',
    redirectTo: '/vertical/dashboard',
    pathMatch: 'full'
  },
  {
    path: 'vertical',
    canActivate: [AuthGuard],
    component: MainLayoutComponent,
    children: defaultRoutes
  },
  {
    path: 'extra',
    component: ExtraLayoutComponent,
    children: extraRoutes
  },
  {
    path: 'not-found',
    loadChildren: () => import('./pages/not-found/not-found.module').then(m => m.NotFoundModule)
  },
  {
    path: "**",
    redirectTo: "/not-found"
  }
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
