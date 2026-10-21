import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { IOption } from 'projects/coda/src/app/ui/interfaces/option';
import { UserProfileView } from 'src/app/core/models/view/user/user-profile.view';
import { Route } from '@angular/compiler/src/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.scss']
})
export class UserProfileComponent implements OnInit {
  contact: UserProfileView

  constructor(private formBuilder: FormBuilder, private route: ActivatedRoute) {

  }

  ngOnInit() {
    this.route.data.subscribe(data => {
      const resolvedData: UserProfileView = data['resolvedData'];
      //this.errorMessage = resolvedData.error;
      this.onUserProfileRetrieved(resolvedData);
    });


  }

  onUserProfileRetrieved(userProfile: UserProfileView) {
    this.contact = userProfile;
  }

  onSubmit() {

  }

}
