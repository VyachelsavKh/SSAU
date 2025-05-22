package com.pfr_crud.ssau.repository;

import com.pfr_crud.ssau.model.DocumentType;
import jakarta.transaction.Transactional;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Modifying;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

import java.util.List;
import java.util.Optional;

@Repository
public interface DocumentTypeRepository extends JpaRepository<DocumentType, Long> {
    @Query("SELECT dt FROM DocumentType dt WHERE dt.id = :id")
    Optional<DocumentType> findById(@Param("id") Long id);

    @Query("SELECT COUNT(dt) > 0 FROM DocumentType dt WHERE dt.id = :id")
    boolean existsById(@Param("id") Long id);

    @Query("SELECT dt FROM DocumentType dt WHERE dt.name = :name")
    Optional<DocumentType> findByName(@Param("name") String name);

    @Query("SELECT COUNT(dt) > 0 FROM DocumentType dt WHERE dt.name = :name")
    boolean existsByName(@Param("name") String name);

    @Query("SELECT dt FROM DocumentType dt WHERE LOWER(dt.name) LIKE LOWER(CONCAT('%', :substring, '%'))")
    List<DocumentType> findByPartialName(@Param("substring") String substring);

    @Query("SELECT dt FROM DocumentType dt")
    List<DocumentType> findAll();

    @Modifying
    @Transactional
    @Query("DELETE FROM DocumentType dt WHERE dt.id = :id")
    void deleteById(@Param("id") Long id);
}