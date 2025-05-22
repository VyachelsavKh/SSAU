package com.pfr_crud.ssau.dto;

import com.pfr_crud.ssau.model.Address;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotNull;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@Builder
@NoArgsConstructor
@AllArgsConstructor
public class AddressDTO {
    private Long id;

    @NotNull(message = "Должен быть id города")
    private Long cityId;

    @NotNull(message = "Должен быть id улицы")
    private Long streetId;

    @NotBlank(message = "Номер дома не может быть пустым")
    private String houseNumber;

    @NotBlank(message = "Номер квартиры не может быть пустым")
    private String apartmentNumber;

    public AddressDTO(Address address) {
        this.id = address.getId();
        this.cityId = address.getCityId();
        this.streetId = address.getStreetId();
        this.houseNumber = address.getHouseNumber();
        this.apartmentNumber = address.getApartmentNumber();
    }
}
