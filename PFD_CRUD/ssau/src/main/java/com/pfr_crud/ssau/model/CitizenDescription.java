package com.pfr_crud.ssau.model;

import com.pfr_crud.ssau.dto.CitizenDescriptionDTO;
import jakarta.persistence.*;
import lombok.*;
import java.time.LocalDate;

@Data
@NoArgsConstructor
@AllArgsConstructor
@Builder
@Entity
@Table(
        name = "citizen_descriptions",
        uniqueConstraints = @UniqueConstraint(columnNames = {"last_name", "first_name", "middle_name", "birth_date"})
)
public class CitizenDescription {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "last_name", nullable = false, length = 255)
    private String lastName;

    @Column(name = "first_name", nullable = false, length = 255)
    private String firstName;

    @Column(name = "middle_name", nullable = false, length = 255)
    private String middleName;

    @Column(name = "birth_date", nullable = false, length = 255)
    private LocalDate birthDate;

    @Enumerated(EnumType.ORDINAL)
    @Column(name = "gender", nullable = false)
    private Gender gender;

    public CitizenDescription(CitizenDescriptionDTO citizenDescriptionDTO) {
        this.id = citizenDescriptionDTO.getId();
        this.lastName = citizenDescriptionDTO.getLastName();
        this.firstName = citizenDescriptionDTO.getFirstName();
        this.middleName = citizenDescriptionDTO.getMiddleName();
        this.birthDate = citizenDescriptionDTO.getBirthDate();
        this.gender = citizenDescriptionDTO.getGender();
    }
}
