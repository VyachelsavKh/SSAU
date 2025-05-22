package com.pfr_crud.ssau.service.results;

import lombok.AllArgsConstructor;
import lombok.Data;

import java.util.Map;

@Data
@AllArgsConstructor
public class OperationResult<T, S extends OperationStatus> {

    private final S status;
    private final T result;
    private final String description;
    private final Map<String, String> data;

    public OperationResult(S status, T result) {
        this(status, result, null, null);
    }

    public OperationResult(S status, T result, String description) {
        this(status, result, description, null);
    }
}