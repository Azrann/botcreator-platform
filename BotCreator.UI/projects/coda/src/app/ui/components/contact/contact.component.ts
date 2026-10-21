import { Component, OnInit, HostBinding, Input, OnChanges } from '@angular/core';
import { IUser } from '../../interfaces/user';
import { IMenuItem } from '../../interfaces/menuItem';

@Component({
  selector: 'tc-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.scss']
})
export class TCContactComponent implements OnInit, OnChanges {
	@HostBinding('class.tc-contact') true;

	@Input() user: IUser;

	contact: IUser;
	
  constructor() { 
		this.contact = {
			tag: '',
			birthDate: '',
			email: '',
			img: '',
			labels: [],
			location:'',
			role:'',
			name:'',
			phone:''
		};
	}
  ngOnInit() {
		this.contact = this.user;
	}
	ngOnChanges() {
		this.ngOnInit();
	}
}
