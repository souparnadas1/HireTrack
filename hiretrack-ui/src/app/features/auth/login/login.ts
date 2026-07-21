import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../core/services/auth';
import { loginRequest } from '../../../core/models/auth.model';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './login.html',
  styleUrl: './login.scss',
})
export class LoginComponent {
  loginRequest: loginRequest = {
    email: '',
    password: '',
  };

  errorMessage: string = '';
  isLoading: boolean = false;

  constructor(
    private authService: AuthService,
    private router: Router,
  ) {}

  onSubmit(): void {
    this.isLoading = true;
    this.errorMessage = '';

    this.authService.login(this.loginRequest).subscribe({
      next: (response) => {
        this.authService.saveToken(response);
        this.isLoading = false;

        // Redirect based on role
        const role = response.role;
        if (role === 'Admin') this.router.navigate(['/admin']);
        else if (role === 'Employer') this.router.navigate(['/employer']);
        else this.router.navigate(['/jobs']);
      },
      error: (err) => {
        this.errorMessage = err.error?.message || 'Login failed. Please try again.';
        this.isLoading = false;
      },
    });
  }
}
