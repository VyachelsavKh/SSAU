import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DictionaryService, DictionaryDTO } from './dictionary.service';

export interface DocumentDTO extends DictionaryDTO { }

@Injectable({
  providedIn: 'root',
})
export class DocTypeService extends DictionaryService<DocumentDTO> {
  constructor(http: HttpClient) {
    super(http, '/api/document-types');
  }
}
