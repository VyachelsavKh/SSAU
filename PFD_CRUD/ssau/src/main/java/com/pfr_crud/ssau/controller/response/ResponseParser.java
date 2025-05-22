package com.pfr_crud.ssau.controller.response;

import com.pfr_crud.ssau.service.results.*;
import lombok.RequiredArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Service;

@Service
@RequiredArgsConstructor
public class ResponseParser {
    public ResponseEntity<?> parseCreate(OperationResult<?, CreateStatus> response) {
        return switch (response.getStatus()) {
            case SUCCESS -> ResponseEntity
                    .status(HttpStatus.CREATED)
                    .body(response.getResult());
            case DUPLICATE_CONFLICT -> ErrorResponseBuilder
                    .status(HttpStatus.CONFLICT)
                    .description(response.getDescription())
                    .data(response.getData())
                    .build();
            case DEPENDENCY_NOT_FOUND -> ErrorResponseBuilder
                    .status(HttpStatus.BAD_REQUEST)
                    .description(response.getDescription())
                    .data(response.getData())
                    .build();
        };
    }

    public ResponseEntity<?> parseGet(OperationResult<?, GetStatus> response) {
        return switch (response.getStatus()) {
            case SUCCESS -> ResponseEntity
                    .ok(response.getResult());
            case NOT_FOUND -> ErrorResponseBuilder
                    .status(HttpStatus.NOT_FOUND)
                    .description(response.getDescription())
                    .data(response.getData())
                    .build();
        };
    }

    public ResponseEntity<?> parseUpdate(OperationResult<?, UpdateStatus> response) {
        return switch (response.getStatus()) {
            case SUCCESS ->  ResponseEntity
                        .ok(response.getResult());
            case NOT_FOUND -> ErrorResponseBuilder
                        .status(HttpStatus.NOT_FOUND)
                        .description(response.getDescription())
                        .data(response.getData())
                        .build();
            case DUPLICATE_CONFLICT -> ErrorResponseBuilder
                        .status(HttpStatus.CONFLICT)
                        .description(response.getDescription())
                        .data(response.getData())
                        .build();
            case DEPENDENCY_NOT_FOUND -> ErrorResponseBuilder
                        .status(HttpStatus.BAD_REQUEST)
                        .description(response.getDescription())
                        .data(response.getData())
                        .build();
        };
    }

    public ResponseEntity<?> parseDelete(OperationResult<?, DeleteStatus> response) {
        return switch (response.getStatus()) {
            case SUCCESS -> ResponseEntity
                        .ok().build();
            case NOT_FOUND_ERROR -> ErrorResponseBuilder
                        .status(HttpStatus.NOT_FOUND)
                        .description(response.getDescription())
                        .data(response.getData())
                        .build();
            case DEPENDENCY_EXISTS -> ErrorResponseBuilder
                        .status(HttpStatus.CONFLICT)
                        .description(response.getDescription())
                        .data(response.getData())
                        .build();
        };
    }

    public ResponseEntity<?> parseAuth(OperationResult<?, AuthStatus> response) {
        return switch (response.getStatus()) {
            case SUCCESS -> ResponseEntity.ok(response.getResult());
            case WRONG_LOGIN, WRONG_PASSWORD -> ErrorResponseBuilder
                    .status(HttpStatus.UNAUTHORIZED)
                    .description(response.getDescription())
                    .data(response.getData())
                    .build();
        };
    }

    public ResponseEntity<?> parseRegister(OperationResult<?, RegisterStatus> response) {
        return switch (response.getStatus()) {
            case SUCCESS -> ResponseEntity.status(HttpStatus.CREATED).body(response.getResult());
            case LOGIN_DUPLICATE -> ErrorResponseBuilder
                    .status(HttpStatus.CONFLICT)
                    .description(response.getDescription())
                    .data(response.getData())
                    .build();
        };
    }

    public ResponseEntity<?> parseResult(OperationResult response) {
        if (response.getStatus() instanceof UpdateStatus) {
            @SuppressWarnings("unchecked")
            OperationResult<?, UpdateStatus> updateResult =
                    (OperationResult<?, UpdateStatus>) response;

            return parseUpdate(updateResult);
        }
        else if (response.getStatus() instanceof CreateStatus) {
            @SuppressWarnings("unchecked")
            OperationResult<?, CreateStatus> createResult =
                    (OperationResult<?, CreateStatus>) response;

            return parseCreate(createResult);
        }
        else if (response.getStatus() instanceof GetStatus) {
            @SuppressWarnings("unchecked")
            OperationResult<?, GetStatus> getResult =
                    (OperationResult<?, GetStatus>) response;

            return parseGet(getResult);
        } else if (response.getStatus() instanceof DeleteStatus) {
            @SuppressWarnings("unchecked")
            OperationResult<?, DeleteStatus> getResult =
                    (OperationResult<?, DeleteStatus>) response;

            return parseDelete(getResult);
        } else if (response.getStatus() instanceof AuthStatus) {
            @SuppressWarnings("unchecked")
            OperationResult<?, AuthStatus> getResult =
                    (OperationResult<?, AuthStatus>) response;

            return parseAuth(getResult);
        } else if (response.getStatus() instanceof RegisterStatus) {
            @SuppressWarnings("unchecked")
            OperationResult<?, RegisterStatus> getResult =
                    (OperationResult<?, RegisterStatus>) response;

            return parseRegister(getResult);
        } else return ResponseEntity.status(HttpStatus.BAD_REQUEST).build();
    }
}
