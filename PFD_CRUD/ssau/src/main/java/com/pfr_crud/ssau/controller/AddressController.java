package com.pfr_crud.ssau.controller;

import com.pfr_crud.ssau.controller.response.ResponseParser;
import com.pfr_crud.ssau.dto.AddressDTO;
import com.pfr_crud.ssau.model.Address;
import com.pfr_crud.ssau.service.address.AddressService;
import com.pfr_crud.ssau.service.results.*;
import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/api/addresses")
@RequiredArgsConstructor
public class AddressController {
    private final AddressService addressService;
    private final ResponseParser responseParser;

    @PostMapping
    public ResponseEntity<?> create(@Valid @RequestBody AddressDTO newAddress) {
        OperationResult<Address, CreateStatus> createdAddress = addressService.create(newAddress);

        return responseParser.parseCreate(createdAddress);
    }

    @GetMapping
    public ResponseEntity<?> getAll() {
        List<Address> foundCities = addressService.getAll();

        return ResponseEntity.ok(foundCities);
    }

    @GetMapping("/{addressId}")
    public ResponseEntity<?> get(@PathVariable Long addressId) {
        OperationResult<Address, GetStatus> foundAddress = addressService.get(addressId);

        return responseParser.parseGet(foundAddress);
    }

    @PutMapping("/{addressId}")
    public ResponseEntity<?> update(@PathVariable Long addressId, @Valid @RequestBody AddressDTO newAddress) {
        OperationResult<Address, UpdateStatus> updatedAddress = addressService.update(addressId, newAddress);

        return responseParser.parseUpdate(updatedAddress);
    }

    @DeleteMapping("/{addressId}")
    public ResponseEntity<?> delete(@PathVariable Long addressId) {
        OperationResult<Address, DeleteStatus> deletedAddress = addressService.delete(addressId);

        return responseParser.parseDelete(deletedAddress);
    }
}