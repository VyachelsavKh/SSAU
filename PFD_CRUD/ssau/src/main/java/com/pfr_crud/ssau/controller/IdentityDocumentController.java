package com.pfr_crud.ssau.controller;

import com.pfr_crud.ssau.controller.response.ResponseParser;
import com.pfr_crud.ssau.dto.IdentityDocumentDTO;
import com.pfr_crud.ssau.model.IdentityDocument;
import com.pfr_crud.ssau.service.identity_document.IdentityDocumentService;
import com.pfr_crud.ssau.service.results.*;
import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/api/identity-documents")
@RequiredArgsConstructor
public class IdentityDocumentController {
    private final IdentityDocumentService identityDocumentService;
    private final ResponseParser responseParser;

    @PostMapping
    public ResponseEntity<?> create(@Valid @RequestBody IdentityDocumentDTO newDocument) {
        OperationResult<IdentityDocument, CreateStatus> createdDocument =
                identityDocumentService.create(newDocument);

        return responseParser.parseCreate(createdDocument);
    }

    @GetMapping
    public ResponseEntity<?> getAll() {
        List<IdentityDocument> documents =
                identityDocumentService.getAll();

        return ResponseEntity.ok(documents);
    }

    @GetMapping("/{documentId}")
    public ResponseEntity<?> get(@PathVariable Long documentId) {
        OperationResult<IdentityDocument, GetStatus> foundDocument =
                identityDocumentService.get(documentId);

        return responseParser.parseGet(foundDocument);
    }

    @PutMapping("/{documentId}")
    public ResponseEntity<?> update(@PathVariable Long documentId, @Valid @RequestBody IdentityDocumentDTO newDocument) {
        OperationResult<IdentityDocument, UpdateStatus> updatedDocument =
                identityDocumentService.update(documentId, newDocument);

        return responseParser.parseUpdate(updatedDocument);
    }

    @DeleteMapping("/{documentId}")
    public ResponseEntity<?> delete(@PathVariable Long documentId) {
        OperationResult<IdentityDocument, DeleteStatus> deletedDocument =
                identityDocumentService.delete(documentId);

        return responseParser.parseDelete(deletedDocument);
    }
}