package com.pfr_crud.ssau.service.street;

import com.pfr_crud.ssau.dto.StreetDTO;
import com.pfr_crud.ssau.model.Street;
import com.pfr_crud.ssau.repository.StreetRepository;
import com.pfr_crud.ssau.service.address.AddressChecker;
import com.pfr_crud.ssau.service.results.*;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;

@Service
@RequiredArgsConstructor
public class StreetService {
    private final StreetRepository streetRepository;
    private final AddressChecker addressChecker;

    public OperationResult<Street, CreateStatus> create(StreetDTO street) {
        if (streetRepository.existsByName(street.getName())) {
            return new OperationResult<>(CreateStatus.DUPLICATE_CONFLICT, null, "Улица с таким именем уже существует");
        }

        Street saved = streetRepository.save(new Street(street.getName()));

        return new OperationResult<>(CreateStatus.SUCCESS, saved);
    }

    public List<Street> getAll(String search) {
        List<Street> streets;

        if (search == null || search.isBlank()) {
            streets = streetRepository.findAll();
        } else {
            streets = streetRepository.findByPartialName(search);
        }

        return streets;
    }

    public OperationResult<Street, GetStatus> get(Long id) {
        Optional<Street> foundStreet = streetRepository.findById(id);

        if (foundStreet.isEmpty()) {
            return new OperationResult<>(GetStatus.NOT_FOUND, null, "Улицы с таким id не существует");
        }

        return new OperationResult<>(GetStatus.SUCCESS, foundStreet.get());
    }

    public OperationResult<Street, UpdateStatus> update(Long id, StreetDTO newStreet) {
        Optional<Street> existingStreet = streetRepository.findById(id);

        if (existingStreet.isEmpty()) {
            return new OperationResult<>(UpdateStatus.NOT_FOUND, null, "Улицы с таким id не существует");
        }

        if (streetRepository.existsByName(newStreet.getName()) &&
                !existingStreet.get().getName().equals(newStreet.getName())) {
            return new OperationResult<>(UpdateStatus.DUPLICATE_CONFLICT, null, "Улица с таким именем уже существует");
        }

        Street street = existingStreet.get();
        street.setName(newStreet.getName());
        Street updated = streetRepository.save(street);

        return new OperationResult<>(UpdateStatus.SUCCESS, updated);
    }

    public OperationResult<Street, DeleteStatus> delete(Long id) {
        return delete(id, false);
    }

    public OperationResult<Street, DeleteStatus> delete(Long id, boolean cascade) {
        if (!streetRepository.existsById(id))
            return new OperationResult<>(DeleteStatus.NOT_FOUND_ERROR, null,
                    "Города с таким id не существует");

        if (!cascade) {
            Long dependentsCount = addressChecker.countDependenciesByStreetId(id);

            if (dependentsCount > 0) {
                return new OperationResult<>(DeleteStatus.DEPENDENCY_EXISTS, null,
                        String.format("Существует %d зависимостей", dependentsCount));
            }
        }

        streetRepository.deleteById(id);

        return new OperationResult<>(DeleteStatus.SUCCESS, null);
    }
}
