package com.pfrpractice.pfr.pfrbdrepo;

import com.pfrpractice.pfr.models.pfrbd.Citizen;
import com.pfrpractice.pfr.models.pfrbd.Document;
import com.pfrpractice.pfr.models.pfrbd.Payment;
import org.springframework.data.repository.CrudRepository;

import java.util.List;
import java.util.Optional;

public interface PaymentRepository extends CrudRepository<Payment, Long> {
    List<Payment> findByCitizen(Citizen citizen);
}
