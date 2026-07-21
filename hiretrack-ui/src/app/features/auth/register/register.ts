import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../core/services/auth';
import { RegisterRequest } from '../../../core/models/auth.model';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './register.html',
  styleUrl: './register.scss',
})
export class RegisterComponent {
  registerRequest: RegisterRequest = {
    fullname: '',
    email: '',
    password: '',
    role: 'Candidate',
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

    this.authService.register(this.registerRequest).subscribe({
      next: (response) => {
        this.authService.saveToken(response);
        this.isLoading = false;

        const role = response.role;
        if (role === 'Employer') this.router.navigate(['/employer']);
        else this.router.navigate(['/jobs']);
      },
      error: (err) => {
        this.errorMessage = err.error?.message || 'Registration failed.';
        this.isLoading = false;
      },
    });
  }
}
