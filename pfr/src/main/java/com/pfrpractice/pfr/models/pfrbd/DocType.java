package com.pfrpractice.pfr.models.pfrbd;

import jakarta.persistence.*;

@Entity
@Table(name = "Тип документа")
public class DocType {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "DocTypeID")
    private Long docTypeId;

    @Column(name = "Название типа документа")
    private String docTypeName;

    public DocType() {}
    public DocType(String docTypeName) {
        this.docTypeName = docTypeName;
    }

    public Long getDocTypeId() {
        return docTypeId;
    }

    public void setDocTypeId(Long docTypeId) {
        this.docTypeId = docTypeId;
    }

    public String getDocTypeName() {
        return docTypeName;
    }

    public void setDocTypeName(String docTypeName) {
        this.docTypeName = docTypeName;
    }

}
