package com.pfrpractice.pfr.controllers;

import com.pfrpractice.pfr.models.pfrbd.City;
import com.pfrpractice.pfr.models.pfrbd.DocType;
import com.pfrpractice.pfr.models.pfrbd.Street;
import com.pfrpractice.pfr.pfrbdrepo.CityRepository;
import com.pfrpractice.pfr.pfrbdrepo.DocTypeRepository;
import com.pfrpractice.pfr.pfrbdrepo.StreetRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestParam;

import java.util.Comparator;
import java.util.List;
import java.util.stream.Collectors;
import java.util.stream.StreamSupport;

@Controller
public class DictionariesController {

    @Autowired
    private CityRepository cityRepository;
    @Autowired
    private StreetRepository streetRepository;
    @Autowired
    private DocTypeRepository docTypeRepository;

    @GetMapping("/dictionaries")
    public String index(Model model) {
        Iterable<City> cityIterable = cityRepository.findAll();
        List<City> cities = StreamSupport.stream(cityIterable.spliterator(), false)
                .sorted(Comparator.comparing(City::getCityId))
                .collect(Collectors.toList());
        model.addAttribute("cities", cities);

        Iterable<Street> streetIterable = streetRepository.findAll();
        List<Street> streets = StreamSupport.stream(streetIterable.spliterator(), false)
                .sorted(Comparator.comparing(Street::getStreetId))
                .collect(Collectors.toList());
        model.addAttribute("streets", streets);

        Iterable<DocType> docTypeIterable = docTypeRepository.findAll();
        List<DocType> docTypes = StreamSupport.stream(docTypeIterable.spliterator(), false)
                .sorted(Comparator.comparing(DocType::getDocTypeId))
                .collect(Collectors.toList());
        model.addAttribute("docTypes", docTypes);

        return "dictionaries";
    }

    @PostMapping("/cityAction")
    public String handleCityAction(
            @RequestParam("action") String action,
            @RequestParam("cityName") String cityName,
            Model model) {

        switch (action) {
            case "search":
                return findCities(model, cityName);
            case "add":
                return addCity(model, cityName);
            case "delete":
                return deleteCity(model, cityName);
            default:
                return "redirect:/dictionaries";
        }
    }

    private String findCities(Model model, String searchCity) {
        Iterable<City> cityIterable = cityRepository.findAll();
        List<City> cities;
        if (searchCity != null && !searchCity.isEmpty()) {
            cities = StreamSupport.stream(cityIterable.spliterator(), false)
                    .filter(city -> city.getCityName().toLowerCase().contains(searchCity.toLowerCase()))
                    .sorted((c1, c2) -> c1.getCityId().compareTo(c2.getCityId()))
                    .collect(Collectors.toList());
        } else {
            cities = StreamSupport.stream(cityIterable.spliterator(), false)
                    .sorted((c1, c2) -> c1.getCityId().compareTo(c2.getCityId()))
                    .collect(Collectors.toList());
        }
        model.addAttribute("cities", cities);
        return "dictionaries";
    }

    private String addCity(Model model, String addCity) {
        City city = new City(addCity);
        try {
            cityRepository.save(city);
        } catch (Exception e) { }
        return "redirect:/dictionaries";
    }

    private String deleteCity(Model model, String delCityName) {
        try {
            cityRepository.findByCityName(delCityName).ifPresent(cityRepository::delete);
        } catch (Exception e) { }
        return "redirect:/dictionaries";
    }

    @PostMapping("/streetAction")
    public String handleStreetAction(
            @RequestParam("action") String action,
            @RequestParam("streetName") String streetName,
            Model model) {

        switch (action) {
            case "search":
                return findStreets(model, streetName);
            case "add":
                return addStreet(model, streetName);
            case "delete":
                return deleteStreet(model, streetName);
            default:
                return "redirect:/dictionaries";
        }
    }

    private String findStreets(Model model, String searchStreet) {
        Iterable<Street> streetIterable = streetRepository.findAll();
        List<Street> streets;
        if (searchStreet != null && !searchStreet.isEmpty()) {
            streets = StreamSupport.stream(streetIterable.spliterator(), false)
                    .filter(street -> street.getStreetName().toLowerCase().contains(searchStreet.toLowerCase()))
                    .sorted((s1, s2) -> s1.getStreetId().compareTo(s2.getStreetId()))
                    .collect(Collectors.toList());
        } else {
            streets = StreamSupport.stream(streetIterable.spliterator(), false)
                    .sorted((s1, s2) -> s1.getStreetId().compareTo(s2.getStreetId()))
                    .collect(Collectors.toList());
        }
        model.addAttribute("streets", streets);
        return "dictionaries";
    }

    private String addStreet(Model model, String addStreet) {
        Street street = new Street(addStreet);
        try {
            streetRepository.save(street);
        } catch (Exception e) { }
        return "redirect:/dictionaries";
    }

    private String deleteStreet(Model model, String delStreetName) {
        try {
            streetRepository.findByStreetName(delStreetName).ifPresent(streetRepository::delete);
        } catch (Exception e) { }
        return "redirect:/dictionaries";
    }

    @PostMapping("/docTypeAction")
    public String handleDocTypeAction(
            @RequestParam("action") String action,
            @RequestParam("docTypeName") String docTypeName,
            Model model) {

        switch (action) {
            case "search":
                return findDocTypes(model, docTypeName);
            case "add":
                return addDocType(model, docTypeName);
            case "delete":
                return deleteDocType(model, docTypeName);
            default:
                return "redirect:/dictionaries";
        }
    }

    private String findDocTypes(Model model, String searchDocType) {
        Iterable<DocType> docTypeIterable = docTypeRepository.findAll();
        List<DocType> docTypes;
        if (searchDocType != null && !searchDocType.isEmpty()) {
            docTypes = StreamSupport.stream(docTypeIterable.spliterator(), false)
                    .filter(docType -> docType.getDocTypeName().toLowerCase().contains(searchDocType.toLowerCase()))
                    .sorted((d1, d2) -> d1.getDocTypeId().compareTo(d2.getDocTypeId()))
                    .collect(Collectors.toList());
        } else {
            docTypes = StreamSupport.stream(docTypeIterable.spliterator(), false)
                    .sorted((d1, d2) -> d1.getDocTypeId().compareTo(d2.getDocTypeId()))
                    .collect(Collectors.toList());
        }
        model.addAttribute("docTypes", docTypes);
        return "dictionaries";
    }

    private String addDocType(Model model, String addDocType) {
        DocType docType = new DocType(addDocType);
        try {
            docTypeRepository.save(docType);
        } catch (Exception e) { }
        return "redirect:/dictionaries";
    }

    private String deleteDocType(Model model, String delDocTypeName) {
        try {
            docTypeRepository.findByDocTypeName(delDocTypeName).ifPresent(docTypeRepository::delete);
        } catch (Exception e) { }
        return "redirect:/dictionaries";
    }
}
