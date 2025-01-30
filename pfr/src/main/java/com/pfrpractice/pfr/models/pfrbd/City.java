package com.pfrpractice.pfr.models.pfrbd;

import jakarta.persistence.*;

@Entity
@Table(name = "Город")
public class City {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "CityID")
    private Long cityId;

    @Column(name = "Название города", unique = true)
    private String cityName;

    public City() {    }
    public City(String cityName) {
        this.cityName = cityName;
    }

    public Long getCityId() {
        return cityId;
    }

    public void setCityId(Long cityId) {
        this.cityId = cityId;
    }

    public String getCityName() {
        return cityName;
    }

    public void setCityName(String cityName) {
        this.cityName = cityName;
    }

}