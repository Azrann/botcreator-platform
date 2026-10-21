import { Component, OnInit, HostBinding, ElementRef, HostListener } from '@angular/core';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { IOption } from '../../../ui/interfaces/option';
import * as SettingsActions from '../../../store/actions/settings.actions';
import { ISettings } from '../../../interfaces/settings';

@Component({
  selector: 'settings-panel',
  templateUrl: './settings-panel.component.html',
  styleUrls: ['./settings-panel.component.scss']
})
export class SettingsPanelComponent implements OnInit {
	@HostBinding('class.settings-panel') true;
	@HostBinding('style.right') get getOffset() { return this.offset + 'px'; }

	offset: number;
	layouts: IOption[];
	layoutOptions: IOption[];
	menuOptions: IOption[];
	verticalNavOptions: IOption[];
	horizontalNavOptions: IOption[];
	periodArray: string[];
	selectedWidth: boolean;
	selectedLayout: string;
	selectedNavStyle: string;
	selectedVerticalNavStyle: string;

	@HostListener('document:click', ['$event'])
	public onClick(event) {
		if (this.offset >= 0) {
			if (!this.element.nativeElement.contains(event.target)) {
				this.offset = -(this.element.nativeElement.offsetWidth);
			}
		}
	}

	constructor(
		private element: ElementRef,
		private store: Store<ISettings>,
		private router: Router
	) {
		this.store.select('settings').subscribe((settings: ISettings) => {
			this.selectedWidth = settings.boxed;
			this.selectedNavStyle = settings.horizontalNavColor;
			this.selectedVerticalNavStyle = settings.verticalNavColor;
		});
		this.selectedLayout = this.router.url.split('/')[1];
		this.layouts = [
			{
				label: 'Vertical layout',
				value: 'vertical'
			},
			{
				label: 'Horizontal layout',
				value: 'horizontal'
			}
		];
		this.layoutOptions = [
			{
				color: '#fff',
				label: 'Boxed',
				value: 'boxed'
			},
			{

				label: 'Full width',
				value: 'full'
			}
		];
		this.menuOptions = [
			{
				color: '#fff',
				label: 'Light style',
				value: 'light'
			},
			{
				selected: true,
				color: '#061e38',
				label: 'Dark style',
				value: 'dark'
			}
		];
		this.verticalNavOptions = [
			{
				selected: this.selectedVerticalNavStyle !== 'dark',
				color: '#fff',
				label: 'Light style',
				value: 'light'
			},
			{
				selected: this.selectedVerticalNavStyle === 'dark',
				color: '#061e38',
				label: 'Dark style',
				value: 'dark'
			}
		];
		this.horizontalNavOptions = [
			{
				selected: this.selectedNavStyle !== 'dark',
				color: '#fff',
				label: 'Light style',
				value: 'light'
			},
			{
				selected: this.selectedNavStyle === 'dark',
				color: '#061e38',
				label: 'Dark style',
				value: 'dark'
			}
		];
	}

	setLayout(value: string) {
		this.periodArray = this.router.url.split('/');
		this.periodArray[1] = value;

		let route = this.periodArray.join('/');

		this.router.navigate([route]);
	}

	setRightOffset(): void {
		if (this.offset === 0) {
			this.offset = -(this.element.nativeElement.offsetWidth);
		} else {
			this.offset = 0;
		}
	}

	setSearchState(state: boolean) {
		this.store.dispatch(new SettingsActions.ChangeSeatchState(state));
	}

	setMenuStyle(option: IOption) {
		this.menuOptions.forEach(menuOption => {
			menuOption !== option ? menuOption.selected = false
														: menuOption.selected = true;
		});
		this.store.dispatch(new SettingsActions.ChangeStyle(option.value));
	}

	setVerticalNavStyle(option: IOption) {
		this.verticalNavOptions.forEach(menuOption => {
			menuOption !== option ? menuOption.selected = false
														: menuOption.selected = true;
		});
		this.store.dispatch(new SettingsActions.ChangeVerticalNavColor(option.value));
	}

	setHorizontalNavStyle(option: IOption) {
		this.horizontalNavOptions.forEach(menuOption => {
			menuOption !== option ? menuOption.selected = false
														: menuOption.selected = true;
		});
		this.store.dispatch(new SettingsActions.ChangeHorizontalNavColor(option.value));
	}

	getSelectedLayout() {
		let selected: IOption;

		this.layouts.filter((option: IOption) => {
			if (option.value === this.selectedLayout) {
				selected = option;
				return;
			}
		});

		return selected.value;
	}

	getSelectedWidth() {
		let selected: IOption;
		let value: string = this.selectedWidth ? 'boxed' : 'full';

		this.layoutOptions.filter((item: IOption) => {
			if (item.value === value) {
				selected = item;
				return;
			}
		});

    return selected.value;
	}

	setLayoutType(type: string) {
		this.store.dispatch(new SettingsActions.ChangeLayoutWidth(type === 'boxed' ? true : false));
	}

  ngOnInit() { }
}
