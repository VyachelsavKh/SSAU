import { Component, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements AfterViewInit {
  @ViewChild('loginInput') loginInput!: ElementRef<HTMLInputElement>;
  @ViewChild('passwordInput') passwordInput!: ElementRef<HTMLInputElement>;

  form: FormGroup;
  serverErrors: { [key: string]: string } = {};

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
  ) {
    this.form = this.fb.group({
      login: ['', Validators.required],
      password: ['', Validators.required],
    });
  }

  ngAfterViewInit() {
    this.loginInput.nativeElement.focus();
  }

  private handleSuccess() {
    this.router.navigate(['/main/my-payments']);
  }

  private handleError(err: any) {
    if (err?.error?.data) {
      this.serverErrors = err.error.data;
    } else {
      this.serverErrors = { general: err?.error?.description || 'Неизвестная ошибка' };
    }
  }

  focusPassword(event: Event) {
    event.preventDefault();
    this.passwordInput.nativeElement.focus();
  }

  onLogin() {
    this.serverErrors = {};
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
    this.authService.login(this.form.value).subscribe({
      next: () => this.handleSuccess(),
      error: (err) => this.handleError(err),
    });
  }

  onRegister() {
    this.serverErrors = {};
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
    this.authService.register(this.form.value).subscribe({
      next: () => this.handleSuccess(),
      error: (err) => this.handleError(err),
    });
  }
}