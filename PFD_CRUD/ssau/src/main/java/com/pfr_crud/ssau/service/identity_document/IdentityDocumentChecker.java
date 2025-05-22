package com.pfr_crud.ssau.service.identity_document;

import com.pfr_crud.ssau.repository.IdentityDocumentRepository;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;

@Service
@RequiredArgsConstructor
public class IdentityDocumentChecker {
    private final IdentityDocumentRepository identityDocumentRepository;

    public Long countDependenciesByDocumentTypeId(Long id) {
        return identityDocumentRepository.countDependenciesByDocumentTypeId(id);
    }
}
