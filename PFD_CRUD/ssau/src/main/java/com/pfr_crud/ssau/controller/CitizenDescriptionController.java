package com.pfr_crud.ssau.controller;

import com.pfr_crud.ssau.controller.response.ResponseParser;
import com.pfr_crud.ssau.dto.CitizenDescriptionDTO;
import com.pfr_crud.ssau.model.CitizenDescription;
import com.pfr_crud.ssau.service.CitizenDescriptionService;
import com.pfr_crud.ssau.service.results.*;
import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/api/citizen-descriptions")
@RequiredArgsConstructor
public class CitizenDescriptionController {
    private final CitizenDescriptionService citizenDescriptionService;
    private final ResponseParser responseParser;

    @PostMapping
    public ResponseEntity<?> create(@Valid @RequestBody CitizenDescriptionDTO newDocument) {
        OperationResult<CitizenDescription, CreateStatus> createdDocument =
                citizenDescriptionService.create(newDocument);

        return responseParser.parseCreate(createdDocument);
    }

    @GetMapping
    public ResponseEntity<?> getAll() {
        List<CitizenDescription> documents =
                citizenDescriptionService.getAll();

        return ResponseEntity.ok(documents);
    }

    @GetMapping("/{documentId}")
    public ResponseEntity<?> get(@PathVariable Long documentId) {
        OperationResult<CitizenDescription, GetStatus> foundDocument =
                citizenDescriptionService.get(documentId);

        return responseParser.parseGet(foundDocument);
    }

    @PutMapping("/{documentId}")
    public ResponseEntity<?> update(@PathVariable Long documentId, @Valid @RequestBody CitizenDescriptionDTO newDocument) {
        OperationResult<CitizenDescription, UpdateStatus> updatedDocument =
                citizenDescriptionService.update(documentId, newDocument);

        return responseParser.parseUpdate(updatedDocument);
    }

    @DeleteMapping("/{documentId}")
    public ResponseEntity<?> delete(@PathVariable Long documentId) {
        OperationResult<CitizenDescription, DeleteStatus> deletedDocument =
                citizenDescriptionService.delete(documentId);

        return responseParser.parseDelete(deletedDocument);
    }
}
