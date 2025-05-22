import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { RouterModule, Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-main',
  imports: [
    RouterModule,
    CommonModule
  ],
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css'],
})
export class MainComponent implements OnInit {
  constructor(
    private authService: AuthService,
    private router: Router
  ) {}
  
  currentUserLogin?: string = '';
  
  ngOnInit() {
    this.currentUserLogin = this.authService.getCredentials()?.login;
  }
  hasRole(role: string): boolean {
    return this.authService.getRoles().includes(role);
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
