package com.pfr_crud.ssau.controller;

import com.pfr_crud.ssau.controller.response.ResponseParser;
import com.pfr_crud.ssau.dto.CityDTO;
import com.pfr_crud.ssau.model.City;
import com.pfr_crud.ssau.service.city.CityService;
import com.pfr_crud.ssau.service.results.*;
import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/api/cities")
@RequiredArgsConstructor
public class CityController {
    private final CityService cityService;
    private final ResponseParser responseParser;

    @PostMapping
    public ResponseEntity<?> create(@Valid @RequestBody CityDTO newCity) {
        OperationResult<City, CreateStatus> createdCity = cityService.create(newCity);

        return responseParser.parseCreate(createdCity);
    }

    @GetMapping
    public ResponseEntity<?> getAll(@RequestParam(required = false) String search) {
        List<City> cities = cityService.getAll(search);

        return ResponseEntity.ok(cities);
    }

    @GetMapping("/{cityId}")
    public ResponseEntity<?> get(@PathVariable Long cityId) {
        OperationResult<City, GetStatus> foundCity = cityService.get(cityId);

        return responseParser.parseGet(foundCity);
    }

    @PutMapping("/{cityId}")
    public ResponseEntity<?> update(@PathVariable Long cityId, @Valid @RequestBody CityDTO newCity) {
        OperationResult<City, UpdateStatus> updatedCity = cityService.update(cityId, newCity);

        return responseParser.parseUpdate(updatedCity);
    }

    @DeleteMapping("/{cityId}")
    public ResponseEntity<?> delete(@PathVariable Long cityId) {
        OperationResult<City, DeleteStatus> deletedCity = cityService.delete(cityId);

        return responseParser.parseDelete(deletedCity);
    }

    @DeleteMapping("/{cityId}/cascade")
    public ResponseEntity<?> deleteCascade(@PathVariable Long cityId) {
        OperationResult<City, DeleteStatus> deletedCity = cityService.delete(cityId, true);

        return responseParser.parseDelete(deletedCity);
    }
}