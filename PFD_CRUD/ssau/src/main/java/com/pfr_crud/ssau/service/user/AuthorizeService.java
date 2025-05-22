package com.pfr_crud.ssau.service.user;

import com.pfr_crud.ssau.dto.UserDTO;
import com.pfr_crud.ssau.model.Role;
import com.pfr_crud.ssau.model.RoleName;
import com.pfr_crud.ssau.model.User;
import com.pfr_crud.ssau.repository.UserRepository;
import com.pfr_crud.ssau.service.RoleService;
import com.pfr_crud.ssau.service.results.AuthStatus;
import com.pfr_crud.ssau.service.results.OperationResult;
import com.pfr_crud.ssau.service.results.RegisterStatus;
import lombok.RequiredArgsConstructor;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.stereotype.Service;

import java.util.*;

@Service
@RequiredArgsConstructor
public class AuthorizeService {
    private final UserRepository userRepository;
    private final PasswordEncoder passwordEncoder;
    private final RoleService roleService;

    public void setRole(User user, RoleName roleName) {
        Set<Role> roles = new HashSet<>();

        switch (roleName) {
            case ROLE_ADMIN -> {
                roles.add(roleService.findByName(RoleName.ROLE_ADMIN));
                roles.add(roleService.findByName(RoleName.ROLE_WORKER));
                roles.add(roleService.findByName(RoleName.ROLE_USER));
            }
            case ROLE_WORKER -> {
                roles.add(roleService.findByName(RoleName.ROLE_WORKER));
                roles.add(roleService.findByName(RoleName.ROLE_USER));
            }
            case ROLE_USER -> roles.add(roleService.findByName(RoleName.ROLE_USER));
        }

        user.setRoles(roles);
    }

    public void createInitialAdminIfNotExists() {
        if (userRepository.existsUserWithAllRoles()) {
            return;
        }

        Optional<User> foundUser = userRepository.findByLogin("admin");

        User admin;

        String message;

        if (foundUser.isPresent()) {
            admin = foundUser.get();
            message = "Существующий пользователь с логином admin получил роли ADMIN, WORKER, USER";
        }
        else {
            admin = new User();
            admin.setLogin("admin");
            admin.setPassword(passwordEncoder.encode("admin"));
            admin.setEnabled(true);

            message = "Создан пользователь admin с паролем admin и ролями ADMIN, WORKER, USER";
        }

        setRole(admin, RoleName.ROLE_ADMIN);

        userRepository.save(admin);

        System.out.println(message);
    }

    public OperationResult<UserDTO, AuthStatus> authenticate(UserDTO user) {
        Optional<User> foundUser = userRepository.findByLogin(user.getLogin());

        Map<String, String> errors = new HashMap<>();

        if (foundUser.isEmpty()) {
            errors.put("login", "Неверный логин");
            return new OperationResult<>(AuthStatus.WRONG_LOGIN, null,
                    "Неверные данные", errors);
        }

        if (!passwordEncoder.matches(user.getPassword(), foundUser.get().getPassword())){
            errors.put("password", "Неверный пароль");
            return new OperationResult<>(AuthStatus.WRONG_PASSWORD, null,
                    "Неверные данные", errors);
        }

        return new OperationResult<>(AuthStatus.SUCCESS, new UserDTO(foundUser.get()));
    }

    public OperationResult<UserDTO, RegisterStatus> register(UserDTO user) {
        Optional<User> foundUser = userRepository.findByLogin(user.getLogin());

        Map<String, String> errors = new HashMap<>();

        if (foundUser.isPresent()) {
            errors.put("login", "Пользователь с таким логином уже существует");
            return new OperationResult<>(RegisterStatus.LOGIN_DUPLICATE, null,
                    "Неверные данные", errors);
        }

        User newUser = new User();
        newUser.setLogin(user.getLogin());
        newUser.setPassword(passwordEncoder.encode(user.getPassword()));
        setRole(newUser, RoleName.ROLE_USER);

        User savedUser = userRepository.save(newUser);

        return new OperationResult<>(RegisterStatus.SUCCESS, new UserDTO(savedUser));
    }
}
