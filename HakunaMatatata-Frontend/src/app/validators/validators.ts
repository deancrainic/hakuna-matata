import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export class CustomValidators {
    static forbiddenPassword(): ValidatorFn {
        return (control: AbstractControl): ValidationErrors | null => {
          const value = control.value;
      
          if (!value) {
            return null;
          }
      
          const hasLetter = /[a-z]+/.test(value);
          const hasDigit = /[0-9]+/.test(value);
      
          const passwordValid = hasLetter && hasDigit;
      
          return !passwordValid ? { invalidPassword: true }: null;
        }
      }
      
      static passwordMatch(password: string, confirmPassword: string): ValidatorFn {
        return (formGroup: AbstractControl): { [key: string]: any } | null => {
          const passwordControl = formGroup.get(password);
          const confirmPasswordControl = formGroup.get(confirmPassword);
          
          if (!passwordControl || !confirmPasswordControl) {
            return null;
          }
      
          if (
            confirmPasswordControl.errors &&
            !confirmPasswordControl.errors['passwordMismatch']
          ) {
            return null;
          }
      
          if (passwordControl.value !== confirmPasswordControl.value) {
            confirmPasswordControl.setErrors({ passwordMismatch: true });
            return { passwordMismatch: true }
          } else {
            confirmPasswordControl.setErrors(null);
            return null;
          }
        };
      }

      static reservationLength(startDate: string, endDate: string): ValidatorFn {
        return (formGroup: AbstractControl): { [key: string]: any } | null => {
          const startDateControl = formGroup.get(startDate);
          const endDateControl = formGroup.get(endDate);

          if (startDateControl?.value === '' || endDateControl?.value === '') {
            return null;
          }

          if (!startDateControl || !endDateControl) {
            return null;
          }

          if ((endDateControl?.value?.getTime() - startDateControl?.value?.getTime()) / (1000 * 3600 * 24) < 2) {
            endDateControl.setErrors({ invalidLength: true });
            return { invalidLength: true };
          } else {
            endDateControl.setErrors(null);
            return null;
          }
        }
      }

      static guestsNumber(guestsNumber: number): ValidatorFn {
        return (control: AbstractControl): ValidationErrors | null => {
          const value = control.value;

          if (!value) {
            return null;
          }

          return (value <= guestsNumber && value >= 1) ? null : { invalidGuestsNumber: true };
        }
      }
}