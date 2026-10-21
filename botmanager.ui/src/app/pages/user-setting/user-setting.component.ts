import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { IOption } from 'projects/coda/src/app/ui/interfaces/option';

@Component({
  selector: 'app-user-setting',
  templateUrl: './user-setting.component.html',
  styleUrls: ['./user-setting.component.scss']
})
export class UserSettingComponent implements OnInit {
  userSettingForm: FormGroup
  loadingMessagesForm: boolean;
  allLanguages: IOption[] = [
    { label: "English", value: "en" },
    { label: "Spanish", value: "es" },
  ]

  constructor(private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.userSettingForm = this.formBuilder.group({
      language: ['en']
    });
  }

}
