package com.pfr_crud.ssau.service.city;

import com.pfr_crud.ssau.dto.CityDTO;
import com.pfr_crud.ssau.model.City;
import com.pfr_crud.ssau.repository.CityRepository;
import com.pfr_crud.ssau.service.address.AddressChecker;
import com.pfr_crud.ssau.service.results.*;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;

@Service
@RequiredArgsConstructor
public class CityService {
    private final CityRepository cityRepository;
    private final AddressChecker addressChecker;

    public OperationResult<City, CreateStatus> create(CityDTO city) {
        if (cityRepository.existsByName(city.getName())) {
            return new OperationResult<>(CreateStatus.DUPLICATE_CONFLICT, null, "Город с таким именем уже существует");
        }

        City saved = cityRepository.save(new City(city.getName()));

        return new OperationResult<>(CreateStatus.SUCCESS, saved);
    }

    public List<City> getAll(String search) {
        List<City> cities;

        if (search == null || search.isBlank()) {
            cities = cityRepository.findAll();
        }
        else {
            cities = cityRepository.findByPartialName(search);
        }

        return cities;
    }

    public OperationResult<City, GetStatus> get(Long id) {
        Optional<City> foundCity = cityRepository.findById(id);

        if (!foundCity.isPresent()) {
            return new OperationResult<>(GetStatus.NOT_FOUND, null, "Города с таким id не существует");
        }

        return new OperationResult<>(GetStatus.SUCCESS, foundCity.get());
    }

    public OperationResult<City, UpdateStatus> update(Long id, CityDTO newCity) {
        Optional<City> foundCity  = cityRepository.findById(id);

        if (foundCity.isEmpty()) {
            return new OperationResult<>(UpdateStatus.NOT_FOUND, null, "Город с таким id не существует");
        }

        if (cityRepository.existsByName(newCity.getName()) &&
                !foundCity.get().getName().equals(newCity.getName())) {
            return new OperationResult<>(UpdateStatus.DUPLICATE_CONFLICT, null, "Город с таким именем уже существует");
        }

        City city = foundCity.get();
        city.setName(newCity.getName());
        City updated = cityRepository.save(city);

        return new OperationResult<>(UpdateStatus.SUCCESS, updated);
    }

    public OperationResult<City, DeleteStatus> delete(Long id) {
        return delete(id, false);
    }

    public OperationResult<City, DeleteStatus> delete(Long id, boolean cascade) {
        if (!cityRepository.existsById(id))
            return new OperationResult<>(DeleteStatus.NOT_FOUND_ERROR, null,
                    "Города с таким id не существует");

        if (!cascade) {
            Long dependentsCount = addressChecker.countDependenciesByCityId(id);

            if (dependentsCount > 0) {
                return new OperationResult<>(DeleteStatus.DEPENDENCY_EXISTS, null,
                        String.format("Существует %d зависимостей", dependentsCount));
            }
        }

        cityRepository.deleteById(id);

        return new OperationResult<>(DeleteStatus.SUCCESS, null);
    }
}
