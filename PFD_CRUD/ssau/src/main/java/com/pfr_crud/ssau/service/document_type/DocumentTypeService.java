package com.pfr_crud.ssau.service.document_type;

import com.pfr_crud.ssau.dto.DocumentTypeDTO;
import com.pfr_crud.ssau.model.DocumentType;
import com.pfr_crud.ssau.repository.DocumentTypeRepository;
import com.pfr_crud.ssau.service.identity_document.IdentityDocumentChecker;
import com.pfr_crud.ssau.service.results.*;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;

@Service
@RequiredArgsConstructor
public class DocumentTypeService {
    private final DocumentTypeRepository documentTypeRepository;
    private final IdentityDocumentChecker identityDocumentChecker;

    public OperationResult<DocumentType, CreateStatus> create(DocumentTypeDTO documentType) {
        if (documentTypeRepository.existsByName(documentType.getName())) {
            return new OperationResult<>(CreateStatus.DUPLICATE_CONFLICT, null, "Тип документа с таким именем уже существует");
        }

        DocumentType saved = documentTypeRepository.save(new DocumentType(documentType.getName()));

        return new OperationResult<>(CreateStatus.SUCCESS, saved);
    }

    public List<DocumentType> getAll(String search) {
        List<DocumentType> documentTypes;

        if (search == null || search.isBlank()) {
            documentTypes = documentTypeRepository.findAll();
        } else {
            documentTypes = documentTypeRepository.findByPartialName(search);
        }

        return documentTypes;
    }

    public OperationResult<DocumentType, GetStatus> get(Long id) {
        Optional<DocumentType> foundDocumentType = documentTypeRepository.findById(id);

        if (foundDocumentType.isEmpty()) {
            return new OperationResult<>(GetStatus.NOT_FOUND, null, "Типа документа с таким id не существует");
        }

        return new OperationResult<>(GetStatus.SUCCESS, foundDocumentType.get());
    }

    public OperationResult<DocumentType, UpdateStatus> update(Long id, DocumentTypeDTO newDocumentType) {
        Optional<DocumentType> existingDocumentType = documentTypeRepository.findById(id);

        if (existingDocumentType.isEmpty()) {
            return new OperationResult<>(UpdateStatus.NOT_FOUND, null, "Типа документа с таким id не существует");
        }

        if (documentTypeRepository.existsByName(newDocumentType.getName()) &&
                !existingDocumentType.get().getName().equals(newDocumentType.getName())) {
            return new OperationResult<>(UpdateStatus.DUPLICATE_CONFLICT, null, "Тип документа с таким именем уже существует");
        }

        DocumentType documentType = existingDocumentType.get();
        documentType.setName(newDocumentType.getName());
        DocumentType updated = documentTypeRepository.save(documentType);

        return new OperationResult<>(UpdateStatus.SUCCESS, updated);
    }

    public OperationResult<DocumentType, DeleteStatus> delete(Long id) {
        return delete(id, false);
    }

    public OperationResult<DocumentType, DeleteStatus> delete(Long id,  boolean cascade) {
        if (!documentTypeRepository.existsById(id))
            return new OperationResult<>(DeleteStatus.NOT_FOUND_ERROR, null,
                    "Города с таким id не существует");

        if (!cascade) {
            Long dependentsCount = identityDocumentChecker.countDependenciesByDocumentTypeId(id);

            if (dependentsCount > 0) {
                return new OperationResult<>(DeleteStatus.DEPENDENCY_EXISTS, null,
                        String.format("Существует %d зависимостей", dependentsCount));
            }
        }

        documentTypeRepository.deleteById(id);

        return new OperationResult<>(DeleteStatus.SUCCESS, null);
    }
}
