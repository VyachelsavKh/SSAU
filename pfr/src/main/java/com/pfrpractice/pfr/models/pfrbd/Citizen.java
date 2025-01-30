package com.pfrpractice.pfr.models.pfrbd;

import jakarta.persistence.*;
import java.util.Date;

@Entity
@Table(name = "Гражданин")
public class Citizen {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "CitizenID")
    private Long citizenId;

    @ManyToOne
    @JoinColumn(name = "DocumentID", referencedColumnName = "DocumentID")
    private Document document;

    @Column(name = "Фамилия")
    private String lastName;

    @Column(name = "Имя")
    private String firstName;

    @Column(name = "Отчество")
    private String middleName;

    @Column(name = "ДатаРождения")
    private String birthDate;

    @Column(name = "Пол")
    private String gender;

    @ManyToOne
    @JoinColumn(name = "LivingAddressID", referencedColumnName = "LivingAddressID")
    private LivingAddress livingAddress;

    public Citizen() {}

    public Citizen(Document document, String lastName, String firstName, String middleName, String birthDate, String gender, LivingAddress livingAddress) {
        if (document.getSeries() == null) {
            throw new IllegalArgumentException("Series must not be empty");
        }
        if (document.getNumber() == null) {
            throw new IllegalArgumentException("Number must not be empty");
        }

        this.document = document;
        this.lastName = lastName;
        this.firstName = firstName;
        this.middleName = middleName;
        this.birthDate = birthDate;
        this.gender = gender;
        this.livingAddress = livingAddress;
    }

    public Long getCitizenId() {
        return citizenId;
    }

    public void setCitizenId(Long citizenId) {
        this.citizenId = citizenId;
    }

    public Document getDocument() {
        return document;
    }

    public void setDocument(Document document) {
        this.document = document;
    }

    public String getLastName() {
        return lastName;
    }

    public void setLastName(String lastName) {
        this.lastName = lastName;
    }

    public String getFirstName() {
        return firstName;
    }

    public void setFirstName(String firstName) {
        this.firstName = firstName;
    }

    public String getMiddleName() {
        return middleName;
    }

    public void setMiddleName(String middleName) {
        this.middleName = middleName;
    }

    public String getBirthDate() {
        return birthDate;
    }

    public void setBirthDate(String birthDate) {
        this.birthDate = birthDate;
    }

    public String getGender() {
        return gender;
    }

    public void setGender(String gender) {
        this.gender = gender;
    }

    public LivingAddress getLivingAddress() {
        return livingAddress;
    }

    public void setLivingAddress(LivingAddress livingAddress) {
        this.livingAddress = livingAddress;
    }

}
