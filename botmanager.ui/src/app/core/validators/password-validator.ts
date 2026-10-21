import { ValidationErrors, FormGroup, ValidatorFn, AbstractControl, Validators } from '@angular/forms';

export class PasswordValidation {
    static passwordMatchValidator(): ValidatorFn {
        return (control: FormGroup): ValidationErrors | null => {
            const passwordCtrl = control.root.get("password");
            const repeatPasswordCtrl = control.root.get("confirmPassword");

            const password = passwordCtrl && passwordCtrl.value;
            const repeatPassword = repeatPasswordCtrl && repeatPasswordCtrl.value;

            return password !== repeatPassword ? { 'passwordDoesNotMatch': true } : null;
        };
    }

    static passwordValidator(): ValidatorFn {
        return Validators.compose([Validators.minLength(6)])
    }
}