package com.pfr_crud.ssau.repository;

import com.pfr_crud.ssau.model.Street;
import jakarta.transaction.Transactional;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Modifying;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

import java.util.List;
import java.util.Optional;

@Repository
public interface StreetRepository extends JpaRepository<Street, Long> {

    @Query("SELECT s FROM Street s WHERE s.id = :id")
    Optional<Street> findById(@Param("id") Long id);

    @Query("SELECT COUNT(s) > 0 FROM Street s WHERE s.id = :id")
    boolean existsById(@Param("id") Long id);

    @Query("SELECT s FROM Street s WHERE s.name = :name")
    Optional<Street> findByName(@Param("name") String name);

    @Query("SELECT COUNT(s) > 0 FROM Street s WHERE s.name = :name")
    boolean existsByName(@Param("name") String name);

    @Query("SELECT s FROM Street s WHERE LOWER(s.name) LIKE LOWER(CONCAT('%', :substring, '%'))")
    List<Street> findByPartialName(@Param("substring") String substring);

    @Query("SELECT s FROM Street s")
    List<Street> findAll();

    @Modifying
    @Transactional
    @Query("DELETE FROM Street s WHERE s.id = :id")
    void deleteById(@Param("id") Long id);
}