package com.pfr_crud.ssau.config;

import com.pfr_crud.ssau.model.Role;
import com.pfr_crud.ssau.model.RoleName;
import com.pfr_crud.ssau.service.RoleService;
import com.pfr_crud.ssau.service.user.AuthorizeService;
import lombok.RequiredArgsConstructor;
import org.springframework.boot.CommandLineRunner;
import org.springframework.stereotype.Component;

@Component
@RequiredArgsConstructor
public class StartupInitializer implements CommandLineRunner {
    private final AuthorizeService authorizeService;
    private final RoleService roleService;
    private final SqlScriptExecutor scriptExecutor;

    @Override
    public void run(String... args) {
        try {
            scriptExecutor.executeAllFrom("data_base");
            System.out.println("Все скрипты выполнены");
        } catch (Exception e) {
            System.out.println(e.getMessage());
        }

        for (RoleName roleName : RoleName.values()) {
            if (!roleService.existsByName(roleName)) {
                roleService.create(Role.builder().name(roleName).build());
            }
        }

        authorizeService.createInitialAdminIfNotExists();
    }
}
