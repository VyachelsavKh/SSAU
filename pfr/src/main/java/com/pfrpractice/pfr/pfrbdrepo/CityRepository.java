package com.pfrpractice.pfr.pfrbdrepo;

import com.pfrpractice.pfr.models.pfrbd.City;
import org.springframework.data.repository.CrudRepository;
import java.util.Optional;

public interface CityRepository extends CrudRepository<City, Long> {
    Optional<City> findByCityName(String cityName);
}
