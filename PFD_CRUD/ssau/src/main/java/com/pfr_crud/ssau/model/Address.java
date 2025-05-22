package com.pfr_crud.ssau.model;

import com.pfr_crud.ssau.dto.AddressDTO;

import jakarta.persistence.*;
import lombok.*;

@Data
@NoArgsConstructor
@AllArgsConstructor
@Builder
@Entity
@Table(
        name = "addresses",
        uniqueConstraints = @UniqueConstraint(columnNames = {"city_id", "street_id", "house_number", "apartment_number"})
)
public class Address {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "city_id", nullable = false)
    private Long cityId;

    @Column(name = "street_id", nullable = false)
    private Long streetId;

    @Column(name = "house_number", nullable = false, length = 255)
    private String houseNumber;

    @Column(name = "apartment_number", nullable = false, length = 255)
    private String apartmentNumber;

    public Address(AddressDTO addressDTO) {
        this.id = addressDTO.getId();
        this.cityId = addressDTO.getCityId();
        this.streetId = addressDTO.getStreetId();
        this.houseNumber = addressDTO.getHouseNumber();
        this.apartmentNumber = addressDTO.getApartmentNumber();
    }
}