package com.pfr_crud.ssau.service.identity_document;

import com.pfr_crud.ssau.dto.IdentityDocumentDTO;
import com.pfr_crud.ssau.model.DocumentType;
import com.pfr_crud.ssau.model.IdentityDocument;
import com.pfr_crud.ssau.repository.IdentityDocumentRepository;
import com.pfr_crud.ssau.service.document_type.DocumentTypeChecker;
import com.pfr_crud.ssau.service.results.*;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;

import java.util.*;

@Service
@RequiredArgsConstructor
public class IdentityDocumentService {
    private final IdentityDocumentRepository identityDocumentRepository;
    private final DocumentTypeChecker documentTypeChecker;

    public OperationResult<IdentityDocument, CreateStatus> create(IdentityDocumentDTO newDocument) {
        Map<String, String> notFoundDependency = new HashMap<>();

        if (newDocument.getDocumentTypeId() != null && !documentTypeChecker.existsById(newDocument.getDocumentTypeId())) {
            notFoundDependency.put("documentTypeId", "Типа документа с таким id не существует");
        }

        if (!notFoundDependency.isEmpty()) {
            return new OperationResult<>(CreateStatus.DEPENDENCY_NOT_FOUND, null,
                    "Связные сущности не найдены", notFoundDependency);
        }

        Optional<Long> foundId = identityDocumentRepository.findIdByDTO(newDocument);

        if (foundId.isPresent())
            return new OperationResult<>(CreateStatus.DUPLICATE_CONFLICT, null,
                    "Удостоверяющий документ с такими параметрами уже существует");

        IdentityDocument saved = identityDocumentRepository.save(new IdentityDocument(newDocument));

        return new OperationResult<>(CreateStatus.SUCCESS, saved);
    }

    public List<IdentityDocument> getAll() {
        return identityDocumentRepository.findAll();
    }

    public OperationResult<IdentityDocument, GetStatus> get(Long id) {
        Optional<IdentityDocument> foundDocument = identityDocumentRepository.findById(id);

        if (foundDocument.isEmpty()) {
            return new OperationResult<>(GetStatus.NOT_FOUND, null, "Документа с таким id не существует");
        }

        return new OperationResult<>(GetStatus.SUCCESS, foundDocument.get());
    }

    private static void updateDocument(IdentityDocument existingDocument, IdentityDocumentDTO newDocument) {
        existingDocument.setDocumentTypeId(newDocument.getDocumentTypeId());
        existingDocument.setSeries(newDocument.getSeries());
        existingDocument.setNumber(newDocument.getNumber());
        if (newDocument.getIssueDate() != null) existingDocument.setIssueDate(newDocument.getIssueDate());
        if (newDocument.getIssuedBy() != null) existingDocument.setIssuedBy(newDocument.getIssuedBy());
    }

    public OperationResult<IdentityDocument, UpdateStatus> update(Long id, IdentityDocumentDTO newDocument) {
        Optional<IdentityDocument> foundDocument = identityDocumentRepository.findById(id);

        if (foundDocument.isEmpty()) {
            return new OperationResult<>(UpdateStatus.NOT_FOUND, null, "Документа с таким id не существует");
        }

        IdentityDocument existingDocument = foundDocument.get();

        Map<String, String> notFoundDependency = new HashMap<>();

        if (newDocument.getDocumentTypeId() != null && !documentTypeChecker.existsById(newDocument.getDocumentTypeId())) {
            notFoundDependency.put("documentTypeId", "Типа документа с таким id не существует");
        }

        if (!notFoundDependency.isEmpty()) {
            return new OperationResult<>(UpdateStatus.DEPENDENCY_NOT_FOUND, null,
                    "Связные сущности не найдены", notFoundDependency);
        }

        updateDocument(existingDocument, newDocument);

        Optional<Long> newDocumentId = identityDocumentRepository.findIdByDTO(new IdentityDocumentDTO(existingDocument));

        if (newDocumentId.isPresent() && !newDocumentId.get().equals(id))
            return new OperationResult<>(UpdateStatus.DUPLICATE_CONFLICT, null,
                    "Удостоверяющий документ с такими параметрами уже существует");

        IdentityDocument savedDocument = identityDocumentRepository.save(existingDocument);

        return new OperationResult<>(UpdateStatus.SUCCESS, savedDocument);
    }

    public OperationResult<IdentityDocument, DeleteStatus> delete(Long id) {
        if (!identityDocumentRepository.existsById(id))
            return new OperationResult<>(DeleteStatus.NOT_FOUND_ERROR, null,
                    "Идентефицирующего документа с таким id не существует");

        identityDocumentRepository.deleteById(id);

        return new OperationResult<>(DeleteStatus.SUCCESS, null);
    }
}
