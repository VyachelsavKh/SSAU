package com.pfr_crud.ssau.repository;

import com.pfr_crud.ssau.dto.IdentityDocumentDTO;
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
public interface IdentityDocumentRepository extends JpaRepository<IdentityDocument, Long> {
    @Query("SELECT id FROM IdentityDocument id WHERE id.id = :id")
    Optional<IdentityDocument> findById(@Param("id") Long id);

    @Query("SELECT COUNT(id) > 0 FROM IdentityDocument id WHERE id.id = :id")
    boolean existsById(@Param("id") Long id);

    @Query("SELECT COUNT(id) FROM IdentityDocument id WHERE id.documentTypeId = :id")
    Long countDependenciesByDocumentTypeId(@Param("id") Long id);

    @Query("""
    SELECT id.id from IdentityDocument id 
    WHERE id.documentTypeId = :#{#dto.documentTypeId}
    AND id.series = :#{#dto.series}
    AND id.number = :#{#dto.number}
    """)
    Optional<Long> findIdByDTO(@Param("dto") IdentityDocumentDTO dto);

    @Query("SELECT id FROM IdentityDocument id")
    List<IdentityDocument> findAll();

    @Modifying
    @Transactional
    @Query("DELETE FROM IdentityDocument id WHERE id.id = :id")
    void deleteById(@Param("id") Long id);
}
