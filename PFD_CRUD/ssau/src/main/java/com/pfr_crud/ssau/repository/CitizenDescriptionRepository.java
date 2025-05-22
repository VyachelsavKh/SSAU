package com.pfr_crud.ssau.repository;

import com.pfr_crud.ssau.dto.CitizenDescriptionDTO;
import com.pfr_crud.ssau.model.CitizenDescription;
import com.pfr_crud.ssau.model.IdentityDocument;
import jakarta.transaction.Transactional;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Modifying;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

import java.util.List;
import java.util.Optional;

@Repository
public interface CitizenDescriptionRepository extends JpaRepository<CitizenDescription, Long> {
    @Query("SELECT cd FROM CitizenDescription cd WHERE cd.id = :id")
    Optional<CitizenDescription> findById(@Param("id") Long id);

    @Query("SELECT COUNT(cd) FROM CitizenDescription cd WHERE cd.id = :id")
    boolean existsById(@Param("id") Long id);

    @Query("""
    SELECT cd.id FROM CitizenDescription cd 
    WHERE cd.lastName = :#{#dto.lastName}
    AND cd.firstName = :#{#dto.firstName}
    AND cd.middleName = :#{#dto.middleName}
    AND cd.birthDate = :#{#dto.birthDate}
    """)
    Optional<Long> findIdByDTO(@Param("dto") CitizenDescriptionDTO dto);

    @Query("SELECT cd FROM CitizenDescription cd")
    List<CitizenDescription> findAll();

    @Modifying
    @Transactional
    @Query("DELETE FROM CitizenDescription cd WHERE cd.id = :id")
    void deleteById(@Param("id") Long id);
}
