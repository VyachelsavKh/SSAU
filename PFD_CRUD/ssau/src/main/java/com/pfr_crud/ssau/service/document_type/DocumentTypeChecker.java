package com.pfr_crud.ssau.service.document_type;

import com.pfr_crud.ssau.repository.DocumentTypeRepository;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;

@Service
@RequiredArgsConstructor
public class DocumentTypeChecker {
    private final DocumentTypeRepository documentTypeRepository;

    public boolean existsById(Long id) {
        return documentTypeRepository.existsById(id);
    }

    public boolean existsByName(String name) {
        return documentTypeRepository.existsByName(name);
    }
}
