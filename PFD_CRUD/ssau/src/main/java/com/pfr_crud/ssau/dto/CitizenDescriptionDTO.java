package com.pfr_crud.ssau.dto;

import com.fasterxml.jackson.annotation.JsonFormat;
import com.pfr_crud.ssau.model.CitizenDescription;
import com.pfr_crud.ssau.model.Gender;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotNull;
import lombok.*;

import java.time.LocalDate;

@Data
@NoArgsConstructor
@AllArgsConstructor
@Builder
public class CitizenDescriptionDTO {
    private Long id;

    @NotBlank(message = "Фамилия не может быть пустой")
    private String lastName;

    @NotBlank(message = "Имя не может быть пустым")
    private String firstName;

    @NotBlank(message = "Отчество не может быть пустым")
    private String middleName;

    @NotNull(message = "Дата рождения обязательна")
    private LocalDate birthDate;

    private Gender gender;

    public CitizenDescriptionDTO(CitizenDescription citizenDescription) {
        this.id = citizenDescription.getId();
        this.lastName = citizenDescription.getLastName();
        this.firstName = citizenDescription.getFirstName();
        this.middleName = citizenDescription.getMiddleName();
        this.birthDate = citizenDescription.getBirthDate();
        this.gender = citizenDescription.getGender();
    }
}