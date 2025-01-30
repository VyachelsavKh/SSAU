package com.pfrpractice.pfr.pfrbdrepo;

import com.pfrpractice.pfr.models.pfrbd.DocType;
import com.pfrpractice.pfr.models.pfrbd.Street;
import org.springframework.data.repository.CrudRepository;

import java.util.Optional;

public interface DocTypeRepository extends CrudRepository<DocType, Long> {

    Optional<DocType> findByDocTypeName(String docTypeName);
}
