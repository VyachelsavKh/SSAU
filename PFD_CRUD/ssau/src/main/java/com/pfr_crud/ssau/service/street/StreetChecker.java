package com.pfr_crud.ssau.service.street;

import com.pfr_crud.ssau.repository.StreetRepository;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;

@Service
@RequiredArgsConstructor
public class StreetChecker {
    private final StreetRepository streetRepository;

    public boolean existsById(Long id) {
        return streetRepository.existsById(id);
    }

    public boolean existsByName(String name) {
        return streetRepository.existsByName(name);
    }
}
