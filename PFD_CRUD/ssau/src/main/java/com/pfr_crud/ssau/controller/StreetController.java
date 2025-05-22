package com.pfr_crud.ssau.controller;

import com.pfr_crud.ssau.controller.response.ResponseParser;
import com.pfr_crud.ssau.dto.StreetDTO;
import com.pfr_crud.ssau.model.Street;
import com.pfr_crud.ssau.service.street.StreetService;
import com.pfr_crud.ssau.service.results.*;
import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/api/streets")
@RequiredArgsConstructor
public class StreetController {
    private final StreetService streetService;
    private final ResponseParser responseParser;

    @PostMapping
    public ResponseEntity<?> create(@Valid @RequestBody StreetDTO newStreet) {
        OperationResult<Street, CreateStatus> createdStreet = streetService.create(newStreet);

        return responseParser.parseCreate(createdStreet);
    }

    @GetMapping
    public ResponseEntity<?> getAll(@RequestParam(required = false) String search) {
        List<Street> streets = streetService.getAll(search);

        return ResponseEntity.ok(streets);
    }

    @GetMapping("/{streetId}")
    public ResponseEntity<?> get(@PathVariable Long streetId) {
        OperationResult<Street, GetStatus> foundStreet = streetService.get(streetId);

        return responseParser.parseGet(foundStreet);
    }

    @PutMapping("/{streetId}")
    public ResponseEntity<?> update(@PathVariable Long streetId, @Valid @RequestBody StreetDTO newStreet) {
        OperationResult<Street, UpdateStatus> updatedStreet = streetService.update(streetId, newStreet);

        return responseParser.parseUpdate(updatedStreet);
    }

    @DeleteMapping("/{streetId}")
    public ResponseEntity<?> delete(@PathVariable Long streetId) {
        OperationResult<Street, DeleteStatus> deletedStreet = streetService.delete(streetId);

        return responseParser.parseDelete(deletedStreet);
    }

    @DeleteMapping("/{streetId}/cascade")
    public ResponseEntity<?> deleteCascade(@PathVariable Long streetId) {
        OperationResult<Street, DeleteStatus> deletedStreet = streetService.delete(streetId, true);

        return responseParser.parseDelete(deletedStreet);
    }
}