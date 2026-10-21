import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AlertService } from 'src/app/core/services/notification/alert.service';
import { UserService } from 'src/app/core/services/data/user.service';
import { AuthenticationService } from 'src/app/core/services/data/auth.service';
import { tap } from 'rxjs/operators';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.scss']
})
export class ForgotPasswordComponent implements OnInit {

  forgotForm: FormGroup;
  loadingForgotForm: boolean;

  constructor(private router: Router,
    private formBuilder: FormBuilder,
    private userService: UserService,
    private alertService: AlertService,
    private authenticationService: AuthenticationService) {
    this.loadingForgotForm = false;
  }

  ngOnInit() {
    this.forgotForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]]
    });
  }

  forgotPassword(valid: boolean) {
    if (!valid) {
      return;
    }
    this.loadingForgotForm = true;

    this.userService.sendResetPasswordLink(this.forgotForm.value["email"])
      .pipe(
        tap((res) => this.alertService.success('Reset password link was sent to your email', true))
      )
      .subscribe(
        data => {
          this.router.navigate(['/']);
        },
        error => {
          this.alertService.error(error);
          this.loadingForgotForm = false;
        });
  }

}



