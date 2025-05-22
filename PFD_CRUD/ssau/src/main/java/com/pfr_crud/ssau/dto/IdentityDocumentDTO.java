package com.pfr_crud.ssau.dto;

import com.fasterxml.jackson.annotation.JsonFormat;
import com.fasterxml.jackson.annotation.JsonProperty;
import com.pfr_crud.ssau.model.IdentityDocument;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotNull;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.time.LocalDate;

@Data
@Builder
@NoArgsConstructor
@AllArgsConstructor
public class IdentityDocumentDTO {
    private Long id;

    @NotNull(message = "Должен быть id типа документа")
    private Long documentTypeId;

    @NotBlank(message = "Серия документа не может быть пустой")
    private String series;

    @NotBlank(message = "Номер документа не может быть пустым")
    private String number;

    private LocalDate issueDate;
    private String issuedBy;

    public IdentityDocumentDTO(IdentityDocument identityDocument)
    {
        this.id = identityDocument.getId();
        this.documentTypeId = identityDocument.getDocumentTypeId();
        this.series = identityDocument.getSeries();
        this.number = identityDocument.getNumber();
        this.issueDate = identityDocument.getIssueDate();
        this.issuedBy = identityDocument.getIssuedBy();
    }
}
