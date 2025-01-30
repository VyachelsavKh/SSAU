package com.pfrpractice.pfr.controllers;

import com.pfrpractice.pfr.models.pfrbd.*;
import com.pfrpractice.pfr.pfrbdrepo.*;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.ModelAttribute;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.w3c.dom.DocumentType;

import javax.print.Doc;
import java.sql.Date;
import java.util.*;
import java.util.stream.Collectors;
import java.util.stream.StreamSupport;

@Controller
public class CitizensController {

    @Autowired
    private CitizenRepository citizenRepository;
    @Autowired
    private CityRepository cityRepository;
    @Autowired
    private StreetRepository streetRepository;
    @Autowired
    private DocTypeRepository docTypeRepository;
    @Autowired
    private DocumentRepository documentRepository;
    @Autowired
    private LivingAddressRepository livingAddressRepository;

    @GetMapping("/citizens")
    public String index(Model model) {
        Iterable<Citizen> citizens = citizenRepository.findAll();
        model.addAttribute("citizens", citizens);

        Iterable<City> cities = cityRepository.findAll();
        model.addAttribute("cities", cities);

        Iterable<Street> streets = streetRepository.findAll();
        model.addAttribute("streets", streets);

        Iterable<DocType> docTypes = docTypeRepository.findAll();
        model.addAttribute("docTypes", docTypes);

        return "citizens"; // название шаблона представления
    }
    @PostMapping("/citizenAction")
    public String handleAction(@RequestParam(name = "action") String action,
                               @RequestParam(name = "docTypeId", required = false) Long docTypeId,
                               @RequestParam(name = "series", required = false) String series,
                               @RequestParam(name = "number", required = false) String number,
                               @RequestParam(name = "issuedBy", required = false) String issuedBy,
                               @RequestParam(name = "issueDate", required = false) String issueDate,
                               @RequestParam(name = "lastName", required = false) String lastName,
                               @RequestParam(name = "firstName", required = false) String firstName,
                               @RequestParam(name = "middleName", required = false) String middleName,
                               @RequestParam(name = "birthDate", required = false) String birthDate,
                               @RequestParam(name = "gender", required = false) String gender,
                               @RequestParam(name = "cityId", required = false) Long cityId,
                               @RequestParam(name = "streetId", required = false) Long streetId,
                               @RequestParam(name = "house", required = false) String house,
                               @RequestParam(name = "apartment", required = false) String apartment,
                               Model model) {
        try{
            switch (action) {
                case "add":
                    if (docTypeId != null && series != null && number != null) {
                        Optional<DocType> docType = docTypeRepository.findById(docTypeId);
                        Document document = new Document(docType.get(), Long.valueOf(series), Long.valueOf(number), issuedBy, issueDate);
                        documentRepository.save(document);

                        Optional<City> city = cityRepository.findById(cityId);
                        Optional<Street> street = streetRepository.findById(streetId);

                        LivingAddress address = new LivingAddress(city.get(), street.get(), house, apartment);
                        livingAddressRepository.save(address);

                        Citizen citizen = new Citizen(document, lastName, firstName, middleName, birthDate, gender, address);
                        citizenRepository.save(citizen);
                    }
                    return "redirect:citizens";
                case "delete":
                    if (docTypeId != null && series != null && number != null) {
                        Optional<Document> document = documentRepository.findBySeriesAndNumber(Long.valueOf(series), Long.valueOf(number));

                        Optional<Citizen> citizen = citizenRepository.findByDocument(document.get());

                        citizenRepository.delete(citizen.get());
                        documentRepository.delete(document.get());
                    }
                    return "redirect:citizens";
                case "search":
                    Optional<Document> document = documentRepository.findBySeriesAndNumber(Long.valueOf(series), Long.valueOf(number));

                    if(series != null && number != null && !series.isEmpty() && !number.isEmpty())
                    {
                        List<Citizen> citizens = new LinkedList<>();

                        if (document.isPresent()) {
                            Optional<Citizen> citizen = citizenRepository.findByDocument(document.get());

                            if(citizen.isPresent()){
                                citizens.add(citizen.get());
                            }
                        }

                        model.addAttribute("citizens", citizens);

                        return "citizens";
                    }

                    Optional<City> city = cityRepository.findById(cityId);
                    Optional<Street> street = streetRepository.findById(streetId);

                    Iterable<Citizen> citizens = citizenRepository.findAll();

                    model.addAttribute("citizens", citizens);
                    return "citizens";
            }
        }
        catch (Exception e) {}
        return "redirect:citizens";
    }
}