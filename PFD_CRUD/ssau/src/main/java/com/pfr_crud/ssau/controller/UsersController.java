package com.pfr_crud.ssau.controller;

import com.pfr_crud.ssau.controller.response.ResponseParser;
import com.pfr_crud.ssau.dto.FullUserDTO;
import com.pfr_crud.ssau.dto.UserDTO;
import com.pfr_crud.ssau.model.Payment;
import com.pfr_crud.ssau.model.Role;
import com.pfr_crud.ssau.service.PaymentService;
import com.pfr_crud.ssau.service.results.GetStatus;
import com.pfr_crud.ssau.service.results.OperationResult;
import com.pfr_crud.ssau.service.results.UpdateStatus;
import com.pfr_crud.ssau.service.user.UsersService;
import lombok.RequiredArgsConstructor;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/api/users")
@RequiredArgsConstructor
public class UsersController {
    private final ResponseParser responseParser;
    private final UsersService usersService;
    private final PaymentService paymentService;

    @PutMapping("/{userId}/roles")
    public ResponseEntity<?> setRole(@PathVariable Long userId, @RequestBody Role role) {
        OperationResult<UserDTO, UpdateStatus> updatedUser = usersService.setRole(userId, role);

        return responseParser.parseUpdate(updatedUser);
    }

    @GetMapping("/{userId}/roles")
    public ResponseEntity<?> getRoles(@PathVariable Long userId) {
        OperationResult<List<String>, GetStatus> foundUser = usersService.getRoles(userId);

        return responseParser.parseGet(foundUser);
    }

    @GetMapping("/{userId}/payments")
    public ResponseEntity<?> getPayments(@PathVariable Long userId) {
        List<Payment> payments = paymentService.getAllByUserId(userId);

        return ResponseEntity.ok(payments);
    }

    @GetMapping("/{userId}")
    public ResponseEntity<?> getUser(@PathVariable Long userId) {
        OperationResult<FullUserDTO, GetStatus> foundUser = usersService.get(userId);

        return responseParser.parseGet(foundUser);
    }

    @GetMapping()
    public ResponseEntity<?> getUsers() {
        List<FullUserDTO> foundUsers = usersService.getAll();

        return ResponseEntity.ok(foundUsers);
    }
}
