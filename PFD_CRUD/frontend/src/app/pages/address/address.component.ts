import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { AddressService, Address } from '../../services/address.service';
import { CityService, CityDTO } from '../../services/Dictionary/city.service';
import { StreetService, StreetDTO } from '../../services/Dictionary/street.service';

@Component({
  selector: 'app-address',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './address.component.html',
  styleUrls: ['./address.component.css'],
})
export class AddressComponent {
  form: FormGroup;
  originalData: Address | null = null;
  serverErrors: { [key: string]: string } = {};
  successMessage: string | null = null;
  cities: CityDTO[] = [];
  streets: StreetDTO[] = [];

  constructor(
    private fb: FormBuilder,
    private addressService: AddressService,
    private cityService: CityService,
    private streetService: StreetService
  ) {
    this.form = this.fb.group({
      cityId: [null, Validators.required],
      streetId: [null, Validators.required],
      houseNumber: [null, Validators.required],
      apartmentNumber: [null, Validators.required],
    });

    this.loadAddress();
    this.loadCities();
    this.loadStreets();
  }

  loadAddress() {
    this.addressService.getAddress().subscribe({
      next: (data) => {
        this.originalData = data;
        this.form.patchValue(data);
      },
      error: (err) => {
        if (err.status === 404) {
          console.log('Адрес ещё не создан');
        } else {
          console.error('Ошибка загрузки адреса', err);
        }
      },
    });
  }

  loadCities() {
    this.cityService.getAll().subscribe({
      next: (cities) => (this.cities = cities),
      error: (err) => console.error('Ошибка загрузки городов', err),
    });
  }

  loadStreets() {
    this.streetService.getAll().subscribe({
      next: (streets) => (this.streets = streets),
      error: (err) => console.error('Ошибка загрузки улиц', err),
    });
  }

  restore() {
    if (this.originalData) {
      this.form.reset();
      this.form.patchValue(this.originalData);
      this.serverErrors = {};
      this.successMessage = null;
    }
  }

  save() {
    this.serverErrors = {};
    this.successMessage = null;

    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    const formValue = this.form.value;
    const payload: Address = {
      cityId: formValue.cityId,
      streetId: formValue.streetId,
      houseNumber: formValue.houseNumber,
      apartmentNumber: formValue.apartmentNumber,
    };

    this.addressService.updateAddress(payload).subscribe({
      next: () => {
        this.successMessage = 'Адрес успешно сохранён.';
        this.loadAddress();
      },
      error: (err) => {
        if (err.error?.data) {
          this.serverErrors = err.error.data;
        } else if (err.error?.description) {
          this.serverErrors = { general: err.error.description };
        } else {
          this.serverErrors = { general: 'Ошибка при сохранении адреса' };
        }
      },
    });
  }
}
