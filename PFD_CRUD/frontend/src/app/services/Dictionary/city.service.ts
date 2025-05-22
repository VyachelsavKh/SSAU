import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DictionaryService, DictionaryDTO } from './dictionary.service';

export interface CityDTO extends DictionaryDTO { }

@Injectable({
  providedIn: 'root',
})
export class CityService extends DictionaryService<CityDTO> {
  constructor(http: HttpClient) {
    super(http, '/api/cities');
  }
}
