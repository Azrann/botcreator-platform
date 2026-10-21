import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { UIModule } from 'projects/coda/public-api';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MalihuScrollbarModule } from 'ngx-malihu-scrollbar';
import { NavBarComponent } from './components/nav-bar';
import { MainLayoutComponent } from './main-layout/main-layout.component';
import { MenuComponent } from './components/menu';
import { SideBarComponent } from './components/side-bar';
import { LogoComponent } from './components/logo';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ExtraLayoutComponent } from './extra-layout/extra-layout.component';
import { SharedComponentsModule } from '../core/components/shared-components.module';

@NgModule({
	imports: [
		CommonModule,
		RouterModule,
		UIModule,
		FormsModule,
		ReactiveFormsModule,
		MalihuScrollbarModule.forRoot(),
		BrowserAnimationsModule,
		SharedComponentsModule
	],
	declarations: [
		NavBarComponent,
		MainLayoutComponent,
		SideBarComponent,
		MenuComponent,
		LogoComponent,
		ExtraLayoutComponent		
	]
})
export class BotLayoutModule { }
