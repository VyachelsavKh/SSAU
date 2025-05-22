import { Component, OnInit } from '@angular/core';
import { CityService }        from '../../services/Dictionary/city.service';
import { DocTypeService }     from '../../services/Dictionary/doc-type.service';
import { StreetService }      from '../../services/Dictionary/street.service';
import { DictionaryDTO }      from '../../services/Dictionary/dictionary.service';
import { DialogService } from '../../services/dialog.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-dictionaries',
  standalone: true,
  templateUrl: './dictionaries.component.html',
  styleUrls: ['./dictionaries.component.css'],
  imports: [
    CommonModule,
    FormsModule
  ],
})
export class DictionariesComponent implements OnInit {
  cities: DictionaryDTO[] = [];
  streets: DictionaryDTO[] = [];
  docTypes: DictionaryDTO[] = [];

  selectedItem: DictionaryDTO | null = null;
  selectedType: 'city' | 'street' | 'docType' | null = null;
  originalName: string = '';

  filter = {
    city: '',
    street: '',
    docType: ''
  };

  sortOrder = {
    city: 'asc',
    street: 'asc',
    docType: 'asc',
  };

  dictionaryTypes: ('city' | 'street' | 'docType')[] = ['city', 'street', 'docType'];

  constructor(
    private cityService: CityService,
    private streetService: StreetService,
    private docTypeService: DocTypeService,
    private dialogService: DialogService
  ) {}

  ngOnInit(): void {
    this.loadAll();
  }

  loadAll() {
    this.loadCities();
    this.loadStreets();
    this.loadDocTypes();
  }

  loadCities() {
    this.cityService.getAll(this.filter.city).subscribe(res => {
      this.cities = this.sort(res, 'city');
    });
  }

  loadStreets() {
    this.streetService.getAll(this.filter.street).subscribe(res => {
      this.streets = this.sort(res, 'street');
    });
  }

  loadDocTypes() {
    this.docTypeService.getAll(this.filter.docType).subscribe(res => {
      this.docTypes = this.sort(res, 'docType');
    });
  }

  sort(list: DictionaryDTO[], type: 'city' | 'street' | 'docType'): DictionaryDTO[] {
    const order = this.sortOrder[type];
    return list.sort((a, b) => {
      if (a.name < b.name) return order === 'asc' ? -1 : 1;
      if (a.name > b.name) return order === 'asc' ? 1 : -1;
      return 0;
    });
  }

  toggleSort(type: 'city' | 'street' | 'docType') {
    this.sortOrder[type] = this.sortOrder[type] === 'asc' ? 'desc' : 'asc';
    this.loadAll();
  }

  startEdit(item: DictionaryDTO, type: 'city' | 'street' | 'docType') {
    this.selectedItem = { ...item };
    this.selectedType = type;
    this.originalName = item.name;
  }

  resetEdit() {
    if (this.selectedItem) {
      this.selectedItem.name = this.originalName;
    }
  }

  saveEdit() {
    if (!this.selectedItem || !this.selectedType) return;

    const service = this.getService(this.selectedType);
    service.update(this.selectedItem.id!, this.selectedItem).subscribe({
      next: () => {
        this.selectedItem = null;
        this.selectedType = null;
        this.loadAll();
      },
      error: (err) => {
        this.dialogService.alert(err?.error?.description || 'Неизвестная ошибка при сохранении');
      }
    });
  }

  async deleteItem(item: DictionaryDTO, type: 'city' | 'street' | 'docType') {
    const confirmed = await this.dialogService.confirm(this.getDeletePrompt(type));
    if (!confirmed) return;

    const service = this.getService(type);

    service.delete(item.id!).subscribe({
      next: () => this.loadAll(),
      error: async (err) => {
        if (err?.status === 409) {
          const forceDelete = await this.dialogService.confirm(
            (err?.error?.description || 'Ошибка удаления') + '. Всё равно удалить?'
          );
          if (forceDelete) {
            service.deleteCascade(item.id!).subscribe({
              next: () => this.loadAll(),
              error: (cascadeErr) => {
                this.dialogService.alert(cascadeErr?.error?.description || 'Ошибка каскадного удаления');
              }
            });
          }
        } else {
          this.dialogService.alert(err?.error?.description || 'Ошибка удаления');
        }
      }
    });
  }

  createItem(type: 'city' | 'street' | 'docType') {
    const service = this.getService(type);
    const newName = prompt(this.getCreatePrompt(type));
    if (newName) {
      service.create({ name: newName }).subscribe({
        next: () => this.loadAll(),
        error: (err) => {
          this.dialogService.alert(err?.error?.description || 'Ошибка при создании');
        }
      });
    }
  }

  getService(type: 'city' | 'street' | 'docType') {
    switch (type) {
      case 'city': return this.cityService;
      case 'street': return this.streetService;
      case 'docType': return this.docTypeService;
    }
  }

  onFilterChange() {
    this.loadAll();
  }

  getCreatePrompt(type: 'city' | 'street' | 'docType'): string {
    const str = 'Введите название '
    switch (type) {
      case 'city':    return str + 'нового города';
      case 'street':  return str + 'новой улицы';
      case 'docType': return str + 'нового типа документов';
    }
  }

  getDeletePrompt(type: 'city' | 'street' | 'docType'): string {
    const str = 'Удалить '
    switch (type) {
      case 'city':    return str + 'город?';
      case 'street':  return str + 'улицу?';
      case 'docType': return str + 'тип документов?';
    }
  }
  
  getDictionaryName(type: 'city' | 'street' | 'docType'): string {
    switch (type) {
      case 'city': return 'Города';
      case 'street': return 'Улицы';
      case 'docType': return 'Типы документов';
    }
  }
  
  getUpdatePrompt(type: 'city' | 'street' | 'docType'): string {
    const str = 'Редактирование ';
    switch (type) {
      case 'city':    return str + 'города';
      case 'street':  return str + 'улицы';
      case 'docType': return str + 'типа документов';
    }
  }
  
  getItems(type: 'city' | 'street' | 'docType'): DictionaryDTO[] {
    switch (type) {
      case 'city': return this.cities;
      case 'street': return this.streets;
      case 'docType': return this.docTypes;
    }
  }
}