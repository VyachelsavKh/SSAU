import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { UsersService } from '../../services/users.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-edit-user-role-modal',
  templateUrl: './edit-user-role-modal.component.html',
  styleUrls: ['./edit-user-role-modal.component.css'],
  imports: [
    FormsModule,
    CommonModule
  ]
})
export class EditUserRoleModalComponent implements OnInit {
  @Input() userId!: number;
  @Input() userLogin!: string;
  @Output() closed = new EventEmitter<void>();

  availableRoles = [
    { label: 'Пользователь', value: 'ROLE_USER' },
    { label: 'Рабочий', value: 'ROLE_WORKER' },
    { label: 'Админ', value: 'ROLE_ADMIN' }
  ];

  originalRole: string = 'ROLE_USER';
  selectedRole: string = 'ROLE_USER';

  loading = false;

  constructor(private usersService: UsersService) {}

  ngOnInit(): void {
    this.loading = true;
    this.usersService.getRoles(this.userId).subscribe(roles => {
      if (roles.includes('ROLE_ADMIN')) {
        this.originalRole = 'ROLE_ADMIN';
      } else if (roles.includes('ROLE_WORKER')) {
        this.originalRole = 'ROLE_WORKER';
      } else {
        this.originalRole = 'ROLE_USER';
      }
      this.selectedRole = this.originalRole;
      this.loading = false;
    });
  }

  restoreRole(): void {
    this.selectedRole = this.originalRole;
  }

  saveRole(): void {
    this.usersService.setRole(this.userId, this.selectedRole).subscribe(() => {
      alert('Роль успешно обновлена!');
      this.close();
    });
  }

  close(): void {
    this.closed.emit();
  }
}
