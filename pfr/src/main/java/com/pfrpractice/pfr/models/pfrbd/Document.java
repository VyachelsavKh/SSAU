package com.pfrpractice.pfr.models.pfrbd;

import jakarta.persistence.*;

@Entity
@Table(name = "Документ удостоверяющий гражданина",
        uniqueConstraints = {@UniqueConstraint(columnNames = {"Серия", "Номер"})})
public class Document {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "DocumentID")
    private Long documentId;

    @ManyToOne
    @JoinColumn(name = "DocTypeID", referencedColumnName = "DocTypeID")
    private DocType docType;

    @Column(name = "Серия")
    private Long series;

    @Column(name = "Номер")
    private Long number;

    @Column(name = "Дата выдачи")
    private String issueDate;

    @Column(name = "Кем выдан")
    private String issuedBy;

    public Document() {}

    public Document(DocType docType, Long series, Long number, String issueDate, String issuedBy) {
        this.docType = docType;
        this.series = series;
        this.number = number;
        this.issueDate = issueDate;
        this.issuedBy = issuedBy;
    }

    public Long getDocumentId() {
        return documentId;
    }

    public void setDocumentId(Long documentId) {
        this.documentId = documentId;
    }

    public DocType getDocType() {
        return docType;
    }

    public void setDocType(DocType docType) {
        this.docType = docType;
    }

    public Long getSeries() {
        return series;
    }

    public void setSeries(Long series) {
        this.series = series;
    }

    public Long getNumber() {
        return number;
    }

    public void setNumber(Long number) {
        this.number = number;
    }

    public String getIssueDate() {
        return issueDate;
    }

    public void setIssueDate(String issueDate) {
        this.issueDate = issueDate;
    }

    public String getIssuedBy() {
        return issuedBy;
    }

    public void setIssuedBy(String issuedBy) {
        this.issuedBy = issuedBy;
    }

}
