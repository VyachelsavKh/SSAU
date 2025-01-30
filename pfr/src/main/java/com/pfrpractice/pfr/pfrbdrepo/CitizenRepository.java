package com.pfrpractice.pfr.pfrbdrepo;

import com.pfrpractice.pfr.models.pfrbd.Citizen;
import com.pfrpractice.pfr.models.pfrbd.Document;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.CrudRepository;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

import java.util.Date;
import java.util.List;
import java.util.Optional;

@Repository
public interface CitizenRepository extends JpaRepository<Citizen, Long> {

    Optional<Citizen> findByDocument(Document document);

    @Query("SELECT c FROM Citizen c WHERE " +
            "(:series IS NULL OR c.document.series = :series) AND " +
            "(:number IS NULL OR c.document.number = :number) AND " +
            "(:lastName IS NULL OR c.lastName = :lastName) AND " +
            "(:firstName IS NULL OR c.firstName = :firstName) AND " +
            "(:middleName IS NULL OR c.middleName = :middleName) AND " +
            "(:birthDate IS NULL OR c.birthDate = :birthDate) AND " +
            "(:gender IS NULL OR c.gender = :gender) AND " +
            "(:city IS NULL OR c.livingAddress.city.cityId = :city) AND " +
            "(:street IS NULL OR c.livingAddress.street.streetId = :street) AND " +
            "(:house IS NULL OR c.livingAddress.house = :house) AND " +
            "(:apartment IS NULL OR c.livingAddress.apartment = :apartment)")
    List<Citizen> findByCriteria(@Param("series") Long series,
                                 @Param("number") Long number,
                                 @Param("lastName") String lastName,
                                 @Param("firstName") String firstName,
                                 @Param("middleName") String middleName,
                                 @Param("birthDate") String birthDate,
                                 @Param("gender") String gender,
                                 @Param("city") Long city,
                                 @Param("street") Long street,
                                 @Param("house") String house,
                                 @Param("apartment") String apartment);
}