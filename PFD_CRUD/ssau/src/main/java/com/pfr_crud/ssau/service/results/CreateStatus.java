package com.pfr_crud.ssau.service.results;

public enum CreateStatus implements OperationStatus {
    SUCCESS,
    DUPLICATE_CONFLICT,
    DEPENDENCY_NOT_FOUND,
}
