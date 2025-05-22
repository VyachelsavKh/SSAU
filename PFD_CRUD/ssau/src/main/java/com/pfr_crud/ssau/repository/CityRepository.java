package com.pfr_crud.ssau.repository;

import com.pfr_crud.ssau.model.City;
import jakarta.transaction.Transactional;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Modifying;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

import java.util.List;
import java.util.Optional;

@Repository
public interface CityRepository extends JpaRepository<City, Long> {

    @Query("SELECT c FROM City c WHERE c.id = :id")
    Optional<City> findById(@Param("id") Long id);

    @Query("SELECT COUNT(c) > 0 FROM City c WHERE c.id = :id")
    boolean existsById(@Param("id") Long id);

    @Query("SELECT c FROM City c WHERE c.name = :name")
    Optional<City> findByName(@Param("name") String name);

    @Query("SELECT COUNT(c) > 0 FROM City c WHERE c.name = :name")
    boolean existsByName(@Param("name") String name);

    @Query("SELECT c FROM City c WHERE LOWER(c.name) LIKE LOWER(CONCAT('%', :substring, '%'))")
    List<City> findByPartialName(@Param("substring") String substring);

    @Query("SELECT c FROM City c")
    List<City> findAll();

    @Modifying
    @Transactional
    @Query("DELETE FROM City c WHERE c.id = :id")
    void deleteById(@Param("id") Long id);
}