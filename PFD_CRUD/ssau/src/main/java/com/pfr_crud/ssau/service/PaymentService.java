package com.pfr_crud.ssau.service;

import com.pfr_crud.ssau.dto.PaymentDTO;
import com.pfr_crud.ssau.model.Payment;
import com.pfr_crud.ssau.repository.PaymentRepository;
import com.pfr_crud.ssau.service.results.*;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;

@Service
@RequiredArgsConstructor
public class PaymentService {
    private final PaymentRepository paymentRepository;

    public OperationResult<Payment, CreateStatus> create(PaymentDTO newPayment) {
        Optional<Long> foundId = paymentRepository.findIdByDTO(newPayment);

        if (foundId.isPresent())
            return new OperationResult<>(CreateStatus.DUPLICATE_CONFLICT, null,
                    "Выплата с такими параметрами уже существует");

        System.out.println(newPayment.getUserId());
        System.out.println(newPayment.getPaymentDate());

        Payment saved = paymentRepository.save(new Payment(newPayment));

        return new OperationResult<>(CreateStatus.SUCCESS, saved);
    }

    public List<Payment> getAll() {
        return paymentRepository.findAll();
    }

    public List<Payment> getAllByUserId(Long id) {
        return paymentRepository.findAllByUserId(id);
    }

    public OperationResult<Payment, GetStatus> get(Long id) {
        Optional<Payment> foundPayment = paymentRepository.findById(id);

        if (foundPayment.isEmpty()) {
            return new OperationResult<>(GetStatus.NOT_FOUND, null, "Выплаты с таким id не существует");
        }

        return new OperationResult<>(GetStatus.SUCCESS, foundPayment.get());
    }

    public OperationResult<Payment, GetStatus> getByUserId(Long userId, Long paymentId) {
        Optional<Payment> foundPayment = paymentRepository.findByUserIdANDId(userId, paymentId);

        if (foundPayment.isEmpty()) {
            return new OperationResult<>(GetStatus.NOT_FOUND, null, "Такой выплаты у пользователя не существует");
        }

        return new OperationResult<>(GetStatus.SUCCESS, foundPayment.get());
    }

    private static void updatePayment(Payment payment, PaymentDTO newPayment) {
        payment.setUserId(newPayment.getUserId());
        payment.setPaymentDate(newPayment.getPaymentDate());
        payment.setAmount(newPayment.getAmount());
        payment.setPaid(newPayment.isPaid());
    }

    public OperationResult<Payment, UpdateStatus> update(Long id, PaymentDTO newPayment) {
        Optional<Payment> foundPayment = paymentRepository.findById(id);

        if (foundPayment.isEmpty()) {
            return new OperationResult<>(UpdateStatus.NOT_FOUND, null,
                    "Выплаты с таким id не существует");
        }

        Payment existingPayment = foundPayment.get();

        updatePayment(existingPayment, newPayment);

        Optional<Long> foundId = paymentRepository.findIdByDTO(new PaymentDTO(existingPayment));

        if (foundId.isPresent() && !foundId.get().equals(id)) {
            return new OperationResult<>(UpdateStatus.DUPLICATE_CONFLICT, null,
                    "Выплата с такими параметрами уже существует");
        }

        Payment updated = paymentRepository.save(existingPayment);

        return new OperationResult<>(UpdateStatus.SUCCESS, updated);
    }

    public OperationResult<Payment, DeleteStatus> delete(Long id) {
        if (!paymentRepository.existsById(id))
            return new OperationResult<>(DeleteStatus.NOT_FOUND_ERROR, null,
                    "Выплаты с таким id не существует");

        paymentRepository.deleteById(id);

        return new OperationResult<>(DeleteStatus.SUCCESS, null);
    }
}
