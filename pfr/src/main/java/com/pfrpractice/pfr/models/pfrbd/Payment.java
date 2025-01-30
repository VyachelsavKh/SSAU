package com.pfrpractice.pfr.models.pfrbd;

import jakarta.persistence.*;
import java.util.Date;

@Entity
@Table(name = "Пенсионная выплата")
public class Payment {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "PaymentID")
    private Long paymentId;

    @ManyToOne
    @JoinColumn(name = "CitizenID", referencedColumnName = "CitizenID")
    private Citizen citizen;

    @Column(name = "Дата выплаты")
    private Date paymentDate;

    @Column(name = "Сумма выплаты")
    private Double paymentAmount;

    public Payment() {}

    public Payment(Citizen citizen, Date paymentDate, Double paymentAmount) {
        this.citizen = citizen;
        this.paymentDate = paymentDate;
        this.paymentAmount = paymentAmount;
    }

    public Long getPaymentId() {
        return paymentId;
    }

    public void setPaymentId(Long paymentId) {
        this.paymentId = paymentId;
    }

    public Citizen getCitizen() {
        return citizen;
    }

    public void setCitizen(Citizen citizen) {
        this.citizen = citizen;
    }

    public Date getPaymentDate() {
        return paymentDate;
    }

    public void setPaymentDate(Date paymentDate) {
        this.paymentDate = paymentDate;
    }

    public Double getPaymentAmount() {
        return paymentAmount;
    }

    public void setPaymentAmount(Double paymentAmount) {
        this.paymentAmount = paymentAmount;
    }

}
