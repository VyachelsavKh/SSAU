package com.pfr_crud.ssau.controller;

import com.pfr_crud.ssau.controller.response.ResponseParser;
import com.pfr_crud.ssau.dto.DocumentTypeDTO;
import com.pfr_crud.ssau.model.DocumentType;
import com.pfr_crud.ssau.service.document_type.DocumentTypeService;
import com.pfr_crud.ssau.service.results.*;
import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/api/document-types")
@RequiredArgsConstructor
public class DocumentTypeController {
    private final DocumentTypeService documentTypeService;
    private final ResponseParser responseParser;

    @PostMapping
    public ResponseEntity<?> create(@Valid @RequestBody DocumentTypeDTO newDocumentType) {
        OperationResult<DocumentType, CreateStatus> createdDocumentType =
                documentTypeService.create(newDocumentType);

        return responseParser.parseCreate(createdDocumentType);
    }

    @GetMapping
    public ResponseEntity<?> getAll(@RequestParam(required = false) String search) {
        List<DocumentType> documentTypes =
                documentTypeService.getAll(search);

        return ResponseEntity.ok(documentTypes);
    }

    @GetMapping("/{documentTypeId}")
    public ResponseEntity<?> get(@PathVariable Long documentTypeId) {
        OperationResult<DocumentType, GetStatus> foundDocumentType =
                documentTypeService.get(documentTypeId);

        return responseParser.parseGet(foundDocumentType);
    }

    @PutMapping("/{documentTypeId}")
    public ResponseEntity<?> update(@PathVariable Long documentTypeId, @Valid @RequestBody DocumentTypeDTO newDocumentType) {
        OperationResult<DocumentType, UpdateStatus> updatedDocumentType =
                documentTypeService.update(documentTypeId, newDocumentType);

        return responseParser.parseUpdate(updatedDocumentType);
    }

    @DeleteMapping("/{documentTypeId}")
    public ResponseEntity<?> delete(@PathVariable Long documentTypeId) {
        OperationResult<DocumentType, DeleteStatus> deletedDocument =
                documentTypeService.delete(documentTypeId);

        return responseParser.parseDelete(deletedDocument);
    }

    @DeleteMapping("/{documentTypeId}/cascade")
    public ResponseEntity<?> deleteCascade(@PathVariable Long documentTypeId) {
        OperationResult<DocumentType, DeleteStatus> deletedDocument =
                documentTypeService.delete(documentTypeId, true);

        return responseParser.parseDelete(deletedDocument);
    }
}