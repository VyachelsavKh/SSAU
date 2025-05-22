package com.pfr_crud.ssau.dto;

import com.pfr_crud.ssau.model.DocumentType;
import jakarta.validation.constraints.NotBlank;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@Builder
@NoArgsConstructor
@AllArgsConstructor
public class DocumentTypeDTO {
    private Long id;

    @NotBlank(message = "Название типа документа не может быть пустым")
    private String name;
}
