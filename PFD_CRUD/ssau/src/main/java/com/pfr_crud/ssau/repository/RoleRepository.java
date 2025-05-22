package com.pfr_crud.ssau.repository;

import com.pfr_crud.ssau.model.Role;
import com.pfr_crud.ssau.model.RoleName;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface RoleRepository extends JpaRepository<Role, Long> {
    boolean existsByName(RoleName name);

    Role findByName(RoleName name);
}
