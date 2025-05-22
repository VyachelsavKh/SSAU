package com.pfr_crud.ssau.service;

import com.pfr_crud.ssau.model.Role;
import com.pfr_crud.ssau.model.RoleName;
import com.pfr_crud.ssau.repository.RoleRepository;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;

@Service
@RequiredArgsConstructor
public class RoleService {
    private final RoleRepository roleRepository;

    public boolean existsByName(RoleName name) {
        return roleRepository.existsByName(name);
    }

    public void create(Role role){
        roleRepository.save(role);
    }

    public Role findByName(RoleName name) {
        return roleRepository.findByName(name);
    }
}
