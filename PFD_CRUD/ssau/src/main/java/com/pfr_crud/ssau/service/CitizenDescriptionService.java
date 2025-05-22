package com.pfr_crud.ssau.service;

import com.pfr_crud.ssau.dto.CitizenDescriptionDTO;
import com.pfr_crud.ssau.model.CitizenDescription;
import com.pfr_crud.ssau.repository.CitizenDescriptionRepository;
import com.pfr_crud.ssau.service.results.*;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;

@Service
@RequiredArgsConstructor
public class CitizenDescriptionService {
    private final CitizenDescriptionRepository citizenDescriptionRepository;

    public OperationResult<CitizenDescription, CreateStatus> create(CitizenDescriptionDTO newDescription) {
        Optional<Long> foundId = citizenDescriptionRepository.findIdByDTO(newDescription);

        if (foundId.isPresent())
            return new OperationResult<>(CreateStatus.DUPLICATE_CONFLICT, null,
                "Личная информация с такими параметрами уже существует");

        CitizenDescription saved = citizenDescriptionRepository.save(new CitizenDescription(newDescription));

        return new OperationResult<>(CreateStatus.SUCCESS, saved);
    }

    public List<CitizenDescription> getAll() {
        return citizenDescriptionRepository.findAll();
    }

    public OperationResult<CitizenDescription, GetStatus> get(Long id) {
        Optional<CitizenDescription> foundDescription = citizenDescriptionRepository.findById(id);

        if (foundDescription.isEmpty()) {
            return new OperationResult<>(GetStatus.NOT_FOUND, null, "Личной информации с таким id не существует");
        }

        return new OperationResult<>(GetStatus.SUCCESS, foundDescription.get());
    }

    private void updateDescription(CitizenDescription existingDescription, CitizenDescriptionDTO newDescription) {
        existingDescription.setLastName(newDescription.getLastName());
        existingDescription.setFirstName(newDescription.getFirstName());
        existingDescription.setMiddleName(newDescription.getMiddleName());
        existingDescription.setBirthDate(newDescription.getBirthDate());
        existingDescription.setGender(newDescription.getGender());
    }

    public OperationResult<CitizenDescription, UpdateStatus> update(Long id, CitizenDescriptionDTO newDescription) {
        Optional<CitizenDescription> foundDescription = citizenDescriptionRepository.findById(id);

        if (foundDescription.isEmpty()) {
            return new OperationResult<>(UpdateStatus.NOT_FOUND, null, "Личной информации с таким id не существует");
        }

        CitizenDescription existingDescription = foundDescription.get();

        updateDescription(existingDescription, newDescription);

        Optional<Long> newDescriptionId = citizenDescriptionRepository.findIdByDTO(new CitizenDescriptionDTO(existingDescription));

        if (newDescriptionId.isPresent() && newDescriptionId.get() != id)
            return new OperationResult<>(UpdateStatus.DUPLICATE_CONFLICT, null,
                    "Личная информация с такими параметрами уже существует");

        CitizenDescription savedDescription = citizenDescriptionRepository.save(existingDescription);

        return new OperationResult<>(UpdateStatus.SUCCESS, savedDescription);
    }

    public OperationResult<CitizenDescription, DeleteStatus> delete(Long id) {
        if (!citizenDescriptionRepository.existsById(id))
            return new OperationResult<>(DeleteStatus.NOT_FOUND_ERROR, null,
                    "Личной информации с таким id не существует");

        citizenDescriptionRepository.deleteById(id);

        return new OperationResult<>(DeleteStatus.SUCCESS, null);
    }
}
