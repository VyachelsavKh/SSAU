package com.pfrpractice.pfr.pfrbdrepo;

import com.pfrpractice.pfr.models.pfrbd.City;
import com.pfrpractice.pfr.models.pfrbd.Street;
import org.springframework.data.repository.CrudRepository;

import java.util.Optional;

public interface StreetRepository extends CrudRepository<Street, Long> {
    Optional<Street> findByStreetName(String streetName);
}
