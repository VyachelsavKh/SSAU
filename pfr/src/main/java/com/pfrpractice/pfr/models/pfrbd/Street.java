package com.pfrpractice.pfr.models.pfrbd;

import jakarta.persistence.*;

@Entity
@Table(name = "Улица")
public class Street {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "StreetID")
    private Long streetId;

    @Column(name = "Название улицы")
    private String streetName;

    public Street() {}
    public Street(String streetName) {
        this.streetName = streetName;
    }

    public Long getStreetId() {
        return streetId;
    }

    public void setStreetId(Long streetId) {
        this.streetId = streetId;
    }

    public String getStreetName() {
        return streetName;
    }

    public void setStreetName(String streetName) {
        this.streetName = streetName;
    }

}
