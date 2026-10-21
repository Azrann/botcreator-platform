import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { UserService } from 'src/app/core/services/data/user.service';
import { AlertService } from 'src/app/core/services/notification/alert.service';
import { AuthenticationService } from 'src/app/core/services/data/auth.service';
import { PasswordValidation } from 'src/app/core/validators/password-validator';
import { first } from 'rxjs/operators';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.scss']
})
export class ResetPasswordComponent implements OnInit {

  resetPasswordForm: FormGroup;
  resetPasswordFormLoading: boolean;
  resetToken: string;
  submitted: boolean = false;

  constructor(private router: Router,
    private formBuilder: FormBuilder,
    private userService: UserService,
    private alertService: AlertService,
    private activatedRoute: ActivatedRoute) {
    this.resetPasswordFormLoading = false;
  }

  ngOnInit() {
    this.activatedRoute.queryParams.subscribe(params => {
      this.resetToken = params['reset-token'];
    });

    this.resetPasswordForm = this.formBuilder.group({
      password: ['', [Validators.required]],
      confirmPassword: ['', [Validators.required]]
    }, { validators: PasswordValidation.passwordMatchValidator() });
  }

  resetPassword(valid: boolean) {
    this.submitted = true;

    // stop here if form is invalid
    if (this.resetPasswordForm.invalid) {
      return;
    }
    if (!this.resetToken) {
      return;
    }

    this.resetPasswordFormLoading = true;
    this.userService.setNewPassword(this.resetPasswordForm.value.password, this.resetToken)
      .pipe(first())
      .subscribe(
        data => {
          this.alertService.success('You have set new password', true);
          this.router.navigate(['/']);
        },
        error => {
          this.alertService.error(error);
          this.resetPasswordFormLoading = false;
        });
  }
}
