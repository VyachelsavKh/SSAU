package com.pfr_crud.ssau.model;

public enum Gender {
    MALE(1),
    FEMALE(2);

    private final int value;

    Gender(int value) {
        this.value = value;
    }

    public int getValue() {
        return value;
    }

    public static Gender fromInt(int i) {
        switch (i) {
            case 1:
                return MALE;
            case 2:
                return FEMALE;
            default:
                throw new IllegalArgumentException("Unexpected value: " + i);
        }
    }
}