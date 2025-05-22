package com.pfr_crud.ssau.dto;

import com.fasterxml.jackson.annotation.JsonFormat;
import com.pfr_crud.ssau.model.Payment;
import jakarta.persistence.*;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotNull;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.math.BigDecimal;
import java.time.LocalDate;

@Data
@NoArgsConstructor
@AllArgsConstructor
@Builder
public class PaymentDTO {
    private Long id;

    @NotNull(message = "Должен быть id пользователя")
    private Long userId;

    @NotNull(message = "Дата выплаты не может быть пустой")
    private LocalDate paymentDate;

    private BigDecimal amount;
    private boolean isPaid = false;

    public PaymentDTO(Payment payment) {
        this.id = payment.getId();
        this.userId = payment.getUserId();
        this.paymentDate = payment.getPaymentDate();
        this.amount = payment.getAmount();
        this.isPaid = payment.isPaid();
    }
}