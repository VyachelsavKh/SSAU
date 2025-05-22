package com.pfr_crud.ssau.dto;

import com.pfr_crud.ssau.model.City;
import jakarta.validation.constraints.NotBlank;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@Builder
@NoArgsConstructor
@AllArgsConstructor
public class CityDTO {
    private Long id;

    @NotBlank(message = "Название города не может быть пустым")
    private String name;
}
