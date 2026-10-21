import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService } from 'src/app/core/services/data/user.service';
import { first, tap, switchMap } from 'rxjs/operators';
import { AlertService } from 'src/app/core/services/notification/alert.service';
import { AuthenticationService } from 'src/app/core/services/data/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'bot-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.scss']
})
export class SignUpComponent implements OnInit {
  signUpForm: FormGroup;
  loadingsignUpForm: boolean;

  constructor(private router: Router,
    private formBuilder: FormBuilder,
    private userService: UserService,
    private alertService: AlertService,
    private authenticationService: AuthenticationService) {
    this.loadingsignUpForm = false;
  }

  ngOnInit() {
    this.initsignUpForm();
  }

  initsignUpForm() {
    this.signUpForm = this.formBuilder.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required]
    });
  }

  signUp(valid: boolean) {
    if (!valid) {
      return;
    }
    this.loadingsignUpForm = true;

    this.userService.register(this.signUpForm.value)
      .pipe(
        tap((res) => this.alertService.success('Registration successful', true)),
        switchMap((res) => this.authenticationService.login(this.signUpForm.controls['email'].value,
          this.signUpForm.controls['password'].value, false))
      )
      .subscribe(
        data => {
          this.router.navigate(['/']);
        },
        error => {
          this.alertService.error(error);
          this.loadingsignUpForm = false;
        });
  }
}

