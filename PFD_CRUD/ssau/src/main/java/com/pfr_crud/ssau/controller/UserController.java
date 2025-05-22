package com.pfr_crud.ssau.controller;

import com.pfr_crud.ssau.controller.response.ResponseParser;
import com.pfr_crud.ssau.model.*;
import com.pfr_crud.ssau.dto.AddressDTO;
import com.pfr_crud.ssau.dto.CitizenDescriptionDTO;
import com.pfr_crud.ssau.dto.IdentityDocumentDTO;
import com.pfr_crud.ssau.service.results.*;
import com.pfr_crud.ssau.service.user.UserService;
import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.springframework.http.ResponseEntity;
import org.springframework.security.core.annotation.AuthenticationPrincipal;
import org.springframework.web.bind.annotation.*;
import java.util.List;

@RestController
@RequestMapping("/api/user")
@RequiredArgsConstructor
public class UserController {
    private final UserService userService;
    private final ResponseParser responseParser;

    @GetMapping("/document")
    public ResponseEntity<?> getDocument(@AuthenticationPrincipal User user) {
        OperationResult<IdentityDocument, GetStatus> updatedDocument =
                userService.getDocument(user);

        return responseParser.parseGet(updatedDocument);
    }

    @PutMapping("/document")
    public ResponseEntity<?> updateDocument(@AuthenticationPrincipal User user, @Valid @RequestBody IdentityDocumentDTO newDocument) {
        OperationResult<IdentityDocument, ? extends OperationStatus> updatedDocument =
                userService.updateDocument(user, newDocument);

        return responseParser.parseResult(updatedDocument);
    }

    @GetMapping("/description")
    public ResponseEntity<?> getDescription(@AuthenticationPrincipal User user) {
        OperationResult<CitizenDescription, GetStatus> updatedDocument =
                userService.getDescription(user);

        return responseParser.parseGet(updatedDocument);
    }

    @PutMapping("/description")
    public ResponseEntity<?> updateDescription(@AuthenticationPrincipal User user, @Valid @RequestBody CitizenDescriptionDTO newDescription) {
        OperationResult<CitizenDescription, ? extends OperationStatus> updatedDescription =
                userService.updateDescription(user, newDescription);

        return responseParser.parseResult(updatedDescription);
    }

    @GetMapping("/address")
    public ResponseEntity<?> getAddress(@AuthenticationPrincipal User user) {
        OperationResult<Address, GetStatus> updatedDocument =
                userService.getAddress(user);

        return responseParser.parseGet(updatedDocument);
    }

    @PutMapping("/address")
    public ResponseEntity<?> updateAddress(@AuthenticationPrincipal User user, @Valid @RequestBody AddressDTO newAddress) {
        OperationResult<Address, ? extends OperationStatus> updatedDescription =
                userService.updateAddress(user, newAddress);

        return responseParser.parseResult(updatedDescription);
    }

    @GetMapping("/payments")
    public ResponseEntity<?> getPayments(@AuthenticationPrincipal User user) {
        List<Payment> payments = userService.getAllPayments(user);

        return ResponseEntity.ok(payments);
    }
}
