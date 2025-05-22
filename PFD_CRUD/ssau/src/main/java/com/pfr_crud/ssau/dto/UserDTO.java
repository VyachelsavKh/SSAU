package com.pfr_crud.ssau.dto;

import com.pfr_crud.ssau.model.Role;
import com.pfr_crud.ssau.model.RoleName;
import com.pfr_crud.ssau.model.User;
import jakarta.validation.constraints.NotBlank;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;
import com.fasterxml.jackson.annotation.JsonProperty;

import java.util.Set;
import java.util.stream.Collectors;

@Data
@Builder
@NoArgsConstructor
@AllArgsConstructor
public class UserDTO {
    @NotBlank(message = "Введите логин")
    @JsonProperty(access = JsonProperty.Access.WRITE_ONLY)
    private String login;

    @NotBlank(message = "Введите пароль")
    @JsonProperty(access = JsonProperty.Access.WRITE_ONLY)
    private String password;

    private Set<RoleName> roles;

    public UserDTO(User user) {
        roles = user.getRoles().stream()
                .map(Role::getName)
                .collect(Collectors.toSet());
    }
}
