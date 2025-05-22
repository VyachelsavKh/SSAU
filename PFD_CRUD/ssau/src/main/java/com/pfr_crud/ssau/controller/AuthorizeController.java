package com.pfr_crud.ssau.controller;

import com.pfr_crud.ssau.controller.response.ResponseParser;
import com.pfr_crud.ssau.dto.UserDTO;
import com.pfr_crud.ssau.model.User;
import com.pfr_crud.ssau.service.results.AuthStatus;
import com.pfr_crud.ssau.service.results.OperationResult;
import com.pfr_crud.ssau.service.results.RegisterStatus;
import com.pfr_crud.ssau.service.user.AuthorizeService;
import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/api")
@RequiredArgsConstructor
public class AuthorizeController {
    private final AuthorizeService authorizeService;
    private final ResponseParser responseParser;

    @PostMapping("/login")
    public ResponseEntity<?> authenticate(@Valid @RequestBody UserDTO user) {
        OperationResult<UserDTO, AuthStatus> authUser = authorizeService.authenticate(user);

        return responseParser.parseAuth(authUser);
    }

    @PostMapping("/register")
    public ResponseEntity<?> register(@Valid @RequestBody UserDTO user) {
        OperationResult<UserDTO, RegisterStatus> registeredUser = authorizeService.register(user);

        return responseParser.parseRegister(registeredUser);
    }
}
