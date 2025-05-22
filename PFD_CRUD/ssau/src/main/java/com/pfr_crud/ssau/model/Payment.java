package com.pfr_crud.ssau.model;

import com.pfr_crud.ssau.dto.PaymentDTO;
import jakarta.persistence.*;
import lombok.*;
import java.time.LocalDate;
import java.math.BigDecimal;

@Data
@NoArgsConstructor
@AllArgsConstructor
@Builder
@Entity
@Table(name = "payments",
        uniqueConstraints = @UniqueConstraint(columnNames = {"user_id", "payment_date"})
)
public class Payment {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "user_id", nullable = false)
    private Long userId;

    @Column(name = "payment_date", nullable = false)
    private LocalDate paymentDate;

    @Column(name = "amount", precision = 10, scale = 2)
    private BigDecimal amount;

    @Column(name = "is_paid")
    private boolean isPaid = false;

    public Payment(PaymentDTO payment) {
        this.userId = payment.getUserId();
        this.paymentDate = payment.getPaymentDate();
        this.amount = payment.getAmount();
        this.isPaid = payment.isPaid();
    }
}