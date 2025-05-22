package com.pfr_crud.ssau.model;

import com.pfr_crud.ssau.dto.IdentityDocumentDTO;
import jakarta.persistence.*;
import lombok.*;

import java.time.LocalDate;

@Data
@NoArgsConstructor
@AllArgsConstructor
@Builder
@Entity
@Table(
        name = "identity_documents",
        uniqueConstraints = @UniqueConstraint(columnNames = {"document_type_id", "series", "number"})
)
public class IdentityDocument {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "document_type_id", nullable = false)
    private Long documentTypeId;

    @Column(name = "series", nullable = false, length = 255)
    private String series;

    @Column(name = "number", nullable = false, length = 255)
    private String number;

    @Column(name = "issue_date", nullable = false)
    private LocalDate issueDate;

    @Column(name = "issued_by", nullable = false, length = 255)
    private String issuedBy;

    public IdentityDocument(IdentityDocumentDTO identityDocumentDTO) {
        this.id = identityDocumentDTO.getId();
        this.series = identityDocumentDTO.getSeries();
        this.number = identityDocumentDTO.getNumber();
        this.issueDate = identityDocumentDTO.getIssueDate();
        this.issuedBy = identityDocumentDTO.getIssuedBy();
        this.documentTypeId = identityDocumentDTO.getDocumentTypeId();
    }
}