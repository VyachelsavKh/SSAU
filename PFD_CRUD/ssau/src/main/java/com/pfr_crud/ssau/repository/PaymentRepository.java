package com.pfr_crud.ssau.repository;

import com.pfr_crud.ssau.dto.PaymentDTO;
import com.pfr_crud.ssau.model.Payment;
import jakarta.transaction.Transactional;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Modifying;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

import java.util.List;
import java.util.Optional;

@Repository
public interface PaymentRepository extends JpaRepository<Payment, Long> {
    @Query("SELECT p FROM Payment p WHERE p.id = :id")
    Optional<Payment> findById(@Param("id") Long id);

    @Query("SELECT COUNT(p) > 0 FROM Payment p WHERE p.id = :id")
    boolean existsById(@Param("id") Long id);

    @Query("""
    SELECT p.id FROM Payment p
    WHERE p.userId = :#{#dto.userId}
    AND p.paymentDate = :#{#dto.paymentDate}
    """)
    Optional<Long> findIdByDTO(@Param("dto") PaymentDTO dto);

    @Query("SELECT p FROM Payment p")
    List<Payment> findAll();

    @Query("SELECT p FROM Payment p WHERE p.userId = :id")
    List<Payment> findAllByUserId(@Param("id") Long id);

    @Query("SELECT p FROM Payment p WHERE p.userId = :userId AND p.id = :id")
    Optional<Payment> findByUserIdANDId(@Param("userId") Long userId, @Param("id") Long id);

    @Query("""
    SELECT COUNT(p) FROM Payment p
    WHERE p.userId = :#{#dto.userId}
    AND p.paymentDate = :#{#dto.paymentDate}
    """)
    boolean existsByDTO(@Param("dto") PaymentDTO dto);

    @Modifying
    @Transactional
    @Query("DELETE FROM Payment p WHERE p.id = :id")
    void deleteById(@Param("id") Long id);
}
