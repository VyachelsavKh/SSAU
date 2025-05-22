package com.pfr_crud.ssau.service.address;

import com.pfr_crud.ssau.repository.AddressRepository;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;

@Service
@RequiredArgsConstructor
public class AddressChecker {
    private final AddressRepository addressRepository;

    public Long countDependenciesByCityId(Long id) {
        return addressRepository.countDependenciesByCityId(id);
    }

    public Long countDependenciesByStreetId(Long id) {
        return addressRepository.countDependenciesByStreetId(id);
    }
}
