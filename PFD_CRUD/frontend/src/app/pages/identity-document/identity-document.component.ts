import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IdentityDocumentService, IdentityDocument } from '../../services/identity-document.service';
import { DocTypeService, DocumentDTO } from '../../services/Dictionary/doc-type.service';

@Component({
  selector: 'app-identity-document',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './identity-document.component.html',
  styleUrls: ['./identity-document.component.css'],
})
export class IdentityDocumentComponent {
  form: FormGroup;
  originalData: IdentityDocument | null = null;
  documentTypes: DocumentDTO[] = [];
  serverError: string | null = null;
  successMessage: string | null = null;

  constructor(
    private fb: FormBuilder,
    private identityDocumentService: IdentityDocumentService,
    private docTypeService: DocTypeService
  ) {
    this.form = this.fb.group({
      documentTypeId: [null, Validators.required],
      series: ['', Validators.required],
      number: ['', Validators.required],
      issueDate: [''],
      issuedBy: [''],
    });

    this.loadDocument();
    this.loadDocumentTypes();
  }

  loadDocument() {
    this.identityDocumentService.getDocument().subscribe({
      next: (data) => {
        this.originalData = data;
        this.form.patchValue(data);
      },
      error: (err) => {
        if (err.status === 404) {
          console.log('Документ ещё не создан');
        } else {
          console.error('Ошибка загрузки документа', err);
        }
      },
    });
  }

  loadDocumentTypes() {
    this.docTypeService.getAll().subscribe({
      next: (types) => this.documentTypes = types,
      error: (err) => console.error('Ошибка загрузки типов документов', err),
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
    const payload: IdentityDocument = {
      documentTypeId: formValue.documentTypeId,
      series: formValue.series,
      number: formValue.number,
    };
    if (formValue.issueDate) payload.issueDate = formValue.issueDate;
    if (formValue.issuedBy) payload.issuedBy = formValue.issuedBy;

    this.identityDocumentService.updateDocument(payload).subscribe({
      next: () => {
        this.successMessage = 'Документ успешно сохранён.';
        this.loadDocument();
      },
      error: (err) => {
        if (err.error?.description) {
          this.serverError = err.error.description;
        } else {
          this.serverError = 'Ошибка при сохранении документа';
        }
      },
    });
  }
}
