package com.pfr_crud.ssau.service.results;

public enum UpdateStatus implements OperationStatus {
    SUCCESS,
    NOT_FOUND,
    DUPLICATE_CONFLICT,
    DEPENDENCY_NOT_FOUND,
}