package com.pfr_crud.ssau.controller;

import com.pfr_crud.ssau.controller.response.ResponseParser;
import com.pfr_crud.ssau.dto.PaymentDTO;
import com.pfr_crud.ssau.model.Payment;
import com.pfr_crud.ssau.service.PaymentService;
import com.pfr_crud.ssau.service.results.*;
import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/api/payments")
@RequiredArgsConstructor
public class PaymentController {
    private final PaymentService paymentService;
    private final ResponseParser responseParser;

    @PostMapping
    public ResponseEntity<?> create(@Valid @RequestBody PaymentDTO newPayment) {
        OperationResult<Payment, CreateStatus> createdAddress = paymentService.create(newPayment);

        return responseParser.parseCreate(createdAddress);
    }

    @GetMapping
    public ResponseEntity<?> getAll() {
        List<Payment> foundCities = paymentService.getAll();

        return ResponseEntity.ok(foundCities);
    }

    @GetMapping("/{paymentId}")
    public ResponseEntity<?> get(@PathVariable Long paymentId) {
        OperationResult<Payment, GetStatus> foundAddress = paymentService.get(paymentId);

        return responseParser.parseGet(foundAddress);
    }

    @PutMapping("/{paymentId}")
    public ResponseEntity<?> update(@PathVariable Long paymentId, @Valid @RequestBody PaymentDTO newPayment) {
        OperationResult<Payment, UpdateStatus> updatedAddress = paymentService.update(paymentId, newPayment);

        return responseParser.parseUpdate(updatedAddress);
    }

    @DeleteMapping("/{paymentId}")
    public ResponseEntity<?> delete(@PathVariable Long paymentId) {
        OperationResult<Payment, DeleteStatus> deletedAddress = paymentService.delete(paymentId);

        return responseParser.parseDelete(deletedAddress);
    }
}