package com.pfr_crud.ssau.service.city;

import com.pfr_crud.ssau.repository.CityRepository;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;

@Service
@RequiredArgsConstructor
public class CityChecker {
    private final CityRepository cityRepository;

    public boolean existsById(Long id) {
        return cityRepository.existsById(id);
    }

    public boolean existsByName(String name) {
        return cityRepository.existsByName(name);
    }
}
