package com.pfrpractice.pfr.models.pfrbd;

import jakarta.persistence.*;

@Entity
@Table(name = "Адрес проживания")
public class LivingAddress {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "LivingAddressID")
    private Long livingAddressId;

    @ManyToOne
    @JoinColumn(name = "CityID", referencedColumnName = "CityID")
    private City city;

    @ManyToOne
    @JoinColumn(name = "StreetID", referencedColumnName = "StreetID")
    private Street street;

    @Column(name = "Дом")
    private String house;

    @Column(name = "Квартира")
    private String apartment;

    public LivingAddress() {}

    public LivingAddress(City city, Street street, String house, String apartment) {
        this.city = city;
        this.street = street;
        this.house = house;
        this.apartment = apartment;
    }

    public Long getLivingAddressId() {
        return livingAddressId;
    }

    public void setLivingAddressId(Long livingAddressId) {
        this.livingAddressId = livingAddressId;
    }

    public City getCity() {
        return city;
    }

    public void setCity(City city) {
        this.city = city;
    }

    public Street getStreet() {
        return street;
    }

    public void setStreet(Street street) {
        this.street = street;
    }

    public String getHouse() {
        return house;
    }

    public void setHouse(String house) {
        this.house = house;
    }

    public String getApartment() {
        return apartment;
    }

    public void setApartment(String apartment) {
        this.apartment = apartment;
    }

}
