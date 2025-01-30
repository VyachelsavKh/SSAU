package com.pfrpractice.pfr.controllers;

import com.pfrpractice.pfr.models.pfrbd.Citizen;
import com.pfrpractice.pfr.models.pfrbd.Payment;
import com.pfrpractice.pfr.pfrbdrepo.CitizenRepository;
import com.pfrpractice.pfr.pfrbdrepo.PaymentRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestParam;

import java.util.LinkedList;
import java.util.List;
import java.util.Optional;

@Controller
public class PaymentsController {
    @Autowired
    PaymentRepository paymentRepository;
    @Autowired
    CitizenRepository citizenRepository;

    @GetMapping("/payments")
    public String index(Model model) {
        Iterable<Payment> payments = paymentRepository.findAll();
        model.addAttribute("payments", payments);

        Iterable<Citizen> citizens = citizenRepository.findAll();
        model.addAttribute("citizens", citizens);
        return "payments";
    }

    @PostMapping("/paymentsAction")
    public String managePayments(@RequestParam("action") String action,
                                 @RequestParam(name = "citizenId", required = false) Long citizenId,
                                 @RequestParam(name = "paymentDate", required = false) String paymentDate,
                                 @RequestParam(name = "paymentAmount", required = false) Double paymentAmount,
                                 Model model) {
        if (citizenId == null) {
            return "redirect:/payments";
        }

        Citizen citizen = citizenRepository.findById(citizenId).orElse(null);

        switch (action) {
            case "search":
                if(citizen != null) {
                    List<Payment> payments = paymentRepository.findByCitizen(citizen);
                    model.addAttribute("payments", payments);
                }
                Iterable<Citizen> citizens = citizenRepository.findAll();
                model.addAttribute("citizens", citizens);
                return "payments";
            case "add":
                Payment payment = new Payment();
                payment.setCitizen(citizen);
                payment.setPaymentDate(java.sql.Date.valueOf(paymentDate));
                payment.setPaymentAmount(paymentAmount);
                paymentRepository.save(payment);
                return "redirect:/payments";
            case "delete":
                List<Payment> payments = paymentRepository.findByCitizen(citizen);
                if (!payments.isEmpty()) {
                    paymentRepository.delete(payments.get(0)); // удаляем первую найденную оплату
                }
                return "redirect:/payments";
        }

        return "redirect:payments";
    }
}