import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PersonalDataService, PersonalData } from '../../services/personal-data.service';

@Component({
  selector: 'app-personal-data',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './personal-data.component.html',
  styleUrls: ['./personal-data.component.css'],
})
export class PersonalDataComponent {
  form: FormGroup;
  originalData: PersonalData | null = null;
  serverError: string | null = null;
  successMessage: string | null = null;
  genders = [
    { value: 'MALE', label: 'Мужской' },
    { value: 'FEMALE', label: 'Женский' },
  ];

  constructor(private fb: FormBuilder, private personalDataService: PersonalDataService) {
    this.form = this.fb.group({
      lastName: ['', Validators.required],
      firstName: ['', Validators.required],
      middleName: ['', Validators.required],
      birthDate: ['', Validators.required],
      gender: [''],
    });

    this.loadPersonalData();
  }

  loadPersonalData() {
    this.personalDataService.getPersonalData().subscribe({
      next: (data) => {
        this.originalData = data;
        this.form.patchValue(data);
      },
      error: (err) => {
        if (err.status === 404) {
          console.log('Нет данных пользователя, форма остаётся пустой');
        } else {
          console.error('Ошибка загрузки личных данных', err);
        }
      },
    });
  }

  restore() {
    if (this.originalData) {
      this.form.reset();
      this.form.patchValue(this.originalData);
      this.serverError = null;
      this.successMessage = null;
    }
  }

  save() {
    this.serverError = null;
    this.successMessage = null;
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    const formValue = this.form.value;
    const payload: PersonalData = {
      lastName: formValue.lastName,
      firstName: formValue.firstName,
      middleName: formValue.middleName,
      birthDate: formValue.birthDate,
    };
    if (formValue.gender) {
      payload.gender = formValue.gender;
    }

    this.personalDataService.updatePersonalData(payload).subscribe({
      next: () => {
        this.successMessage = 'Данные успешно сохранены.';
        this.loadPersonalData();
      },
      error: (err) => {
        if (err.error?.description) {
          this.serverError = err.error.description;
        } else {
          this.serverError = 'Произошла ошибка при сохранении';
        }
      },
    });
  }
}
