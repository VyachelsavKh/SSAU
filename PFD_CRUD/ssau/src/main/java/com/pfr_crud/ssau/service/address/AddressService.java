package com.pfr_crud.ssau.service.address;

import com.pfr_crud.ssau.dto.AddressDTO;
import com.pfr_crud.ssau.model.Address;
import com.pfr_crud.ssau.repository.AddressRepository;
import com.pfr_crud.ssau.service.city.CityChecker;
import com.pfr_crud.ssau.service.results.*;
import com.pfr_crud.ssau.service.street.StreetChecker;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;

import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Optional;

@Service
@RequiredArgsConstructor
public class AddressService {
    private final AddressRepository addressRepository;
    private final CityChecker cityChecker;
    private final StreetChecker streetChecker;

    public OperationResult<Address, CreateStatus> create(AddressDTO address) {
        Map<String, String> notFoundDependency = new HashMap<>();

        if (!cityChecker.existsById(address.getCityId())) {
            notFoundDependency.put("cityId", "Города с таким id не существует");
        }

        if (!streetChecker.existsById(address.getStreetId())) {
            notFoundDependency.put("streetId", "Улицы с таким id не существует");
        }

        if (!notFoundDependency.isEmpty())
            return new OperationResult<>(CreateStatus.DEPENDENCY_NOT_FOUND, null,
                    "Связные сущности не найдены", notFoundDependency);

        Optional<Long> foundId = addressRepository.findIdByDTO(address);

        if (foundId.isPresent())
            return new OperationResult<>(CreateStatus.DUPLICATE_CONFLICT, null,
                    "Адрес с такими параметрами уже существует");

        Address saved = addressRepository.save(new Address(address));

        return new OperationResult<>(CreateStatus.SUCCESS, saved);
    }

    public List<Address> getAll() {
        return addressRepository.findAll();
    }

    public Optional<Long> findIdByDTO(AddressDTO address)
    {
        return addressRepository.findIdByDTO(address);
    }

    public OperationResult<Address, GetStatus> get(Long id) {
        Optional<Address> foundAddress = addressRepository.findById(id);

        if (foundAddress.isEmpty()) {
            return new OperationResult<>(GetStatus.NOT_FOUND, null, "Адреса с таким id не существует");
        }

        return new OperationResult<>(GetStatus.SUCCESS, foundAddress.get());
    }

    private static void updateAddress(Address existingAddress, AddressDTO newAddress) {
        existingAddress.setCityId(newAddress.getCityId());
        existingAddress.setStreetId(newAddress.getStreetId());
        existingAddress.setHouseNumber(newAddress.getHouseNumber());
        existingAddress.setApartmentNumber(newAddress.getApartmentNumber());
    }

    public OperationResult<Address, UpdateStatus> update(Long id, AddressDTO newAddress) {
        Optional<Address> foundAddress = addressRepository.findById(id);

        if (foundAddress.isEmpty()) {
            return new OperationResult<>(UpdateStatus.NOT_FOUND, null, "Адреса с таким id не существует");
        }

        Map<String, String> notFoundDependency = new HashMap<>();

        if (newAddress.getCityId() != null && !cityChecker.existsById(newAddress.getCityId())) {
            notFoundDependency.put("cityId", "Города с таким id не существует");
        }

        if (newAddress.getStreetId() != null & !streetChecker.existsById(newAddress.getStreetId())) {
            notFoundDependency.put("streetId", "Улицы с таким id не существует");
        }

        if (!notFoundDependency.isEmpty()) {
            return new OperationResult<>(UpdateStatus.DEPENDENCY_NOT_FOUND, null,
                    "Связные сущности не найдены", notFoundDependency);
        }

        Address existingAddress = foundAddress.get();

        updateAddress(existingAddress, newAddress);

        Optional<Long> newAddressId = addressRepository.findIdByDTO(new AddressDTO(existingAddress));

        if (newAddressId.isPresent() && !newAddressId.get().equals(id)) {
            return new OperationResult<>(UpdateStatus.DUPLICATE_CONFLICT, null,
                    "Адрес с такими параметрами уже существует");
        }

        Address updated = addressRepository.save(existingAddress);

        return new OperationResult<>(UpdateStatus.SUCCESS, updated);
    }

    public OperationResult<Address, DeleteStatus> delete(Long id) {
        if (!addressRepository.existsById(id))
            return new OperationResult<>(DeleteStatus.NOT_FOUND_ERROR, null,
                    "Адреса с таким id не существует");

        addressRepository.deleteById(id);

        return new OperationResult<>(DeleteStatus.SUCCESS, null);
    }
}
