import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DictionaryService, DictionaryDTO } from './dictionary.service';

export interface StreetDTO extends DictionaryDTO { }

@Injectable({
  providedIn: 'root',
})
export class StreetService extends DictionaryService<StreetDTO> {
  constructor(http: HttpClient) {
    super(http, '/api/streets');
  }
}