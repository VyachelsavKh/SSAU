package com.pfr_crud.ssau.dto;

import com.pfr_crud.ssau.model.Street;
import jakarta.validation.constraints.NotBlank;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@Builder
@NoArgsConstructor
@AllArgsConstructor
public class StreetDTO {
    private Long id;

    @NotBlank(message = "Название улицы не может быть пустым")
    private String name;
}
