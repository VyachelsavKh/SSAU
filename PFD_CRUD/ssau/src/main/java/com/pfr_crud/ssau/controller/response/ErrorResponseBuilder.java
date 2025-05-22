package com.pfr_crud.ssau.controller.response;

import jakarta.servlet.http.HttpServletRequest;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.context.request.RequestAttributes;
import org.springframework.web.context.request.RequestContextHolder;
import org.springframework.web.context.request.ServletRequestAttributes;

public class ErrorResponseBuilder {

    private HttpStatus status;
    private String description;
    private Object data;

    public static ErrorResponseBuilder status(HttpStatus status) {
        ErrorResponseBuilder builder = new ErrorResponseBuilder();
        builder.status = status;
        return builder;
    }

    public ErrorResponseBuilder description(String description) {
        this.description = description;
        return this;
    }

    public ErrorResponseBuilder data(Object data) {
        this.data = data;
        return this;
    }

    public ResponseEntity<ErrorResponse> build() {
        String path = getCurrentRequestPath();

        ErrorResponse body = new ErrorResponse(
                status.value(),
                status.getReasonPhrase(),
                path,
                description,
                data
        );

        return ResponseEntity.status(status).body(body);
    }

    private String getCurrentRequestPath() {
        RequestAttributes requestAttributes = RequestContextHolder.getRequestAttributes();
        if (requestAttributes instanceof ServletRequestAttributes servletRequestAttributes) {
            HttpServletRequest request = servletRequestAttributes.getRequest();
            return request.getRequestURI();
        }
        return "unknown";
    }
}