package com.pfr_crud.ssau.repository;

import com.pfr_crud.ssau.dto.AddressDTO;
import com.pfr_crud.ssau.model.Address;
import jakarta.transaction.Transactional;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Modifying;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

import java.util.List;
import java.util.Optional;

@Repository
public interface AddressRepository extends JpaRepository<Address, Long> {

    @Query("SELECT a FROM Address a WHERE a.id = :id")
    Optional<Address> findById(@Param("id") Long id);

    @Query("SELECT COUNT(a) FROM Address a WHERE a.cityId = :cityId")
    Long countDependenciesByCityId(@Param("cityId") Long cityId);

    @Query("SELECT COUNT(a) FROM Address a WHERE a.streetId = :streetId")
    Long countDependenciesByStreetId(@Param("streetId") Long streetId);

    @Query("SELECT COUNT(a) FROM Address a WHERE a.cityId = :cityId AND a.streetId = :streetId")
    Long countDependenciesByCityAndStreetId(@Param("cityId") Long cityId, @Param("streetId") Long streetId);

    @Query("""
        SELECT a.id FROM Address a
        WHERE a.cityId = :#{#dto.cityId}
          AND a.streetId = :#{#dto.streetId}
          AND a.houseNumber = :#{#dto.houseNumber}
          AND a.apartmentNumber = :#{#dto.apartmentNumber}
    """)
    Optional<Long> findIdByDTO(@Param("dto") AddressDTO dto);

    @Query("SELECT a FROM Address a")
    List<Address> findAll();

    @Modifying
    @Transactional
    @Query("DELETE FROM Address a WHERE a.id = :id")
    void deleteById(@Param("id") Long id);
}
