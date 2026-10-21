import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { UIModule } from '../ui/ui.module';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { MalihuScrollbarModule } from 'ngx-malihu-scrollbar';

import { BaseLayoutComponent } from './base-layout';
import { NavBarComponent } from './components/nav-bar';
import { SideBarComponent } from './components/side-bar';
import { VerticalLayoutComponent } from './vertical';
import { ExtraLayoutComponent } from './extra';
import { HorizontalLayoutComponent } from './horizontal';
import { MenuComponent } from './components/menu';
import { SettingsPanelComponent } from './components/settings-panel';
import { FooterComponent } from './components/footer';
import { LogoComponent } from './components/logo/logo.component';

@NgModule({
	imports: [
		CommonModule,
		RouterModule,
		UIModule,
		FormsModule,
		ReactiveFormsModule,
		MalihuScrollbarModule.forRoot(),
	],
	declarations: [
		NavBarComponent,
		SideBarComponent,
		MenuComponent,
		VerticalLayoutComponent,
		ExtraLayoutComponent,
		HorizontalLayoutComponent,
		MenuComponent,
		SettingsPanelComponent,
		FooterComponent,
		BaseLayoutComponent,
		LogoComponent,
	]
})
export class LayoutModule { }
