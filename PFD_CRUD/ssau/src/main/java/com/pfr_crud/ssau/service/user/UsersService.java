package com.pfr_crud.ssau.service.user;

import com.pfr_crud.ssau.dto.*;
import com.pfr_crud.ssau.model.Role;
import com.pfr_crud.ssau.model.User;
import com.pfr_crud.ssau.repository.UserRepository;
import com.pfr_crud.ssau.service.CitizenDescriptionService;
import com.pfr_crud.ssau.service.address.AddressService;
import com.pfr_crud.ssau.service.identity_document.IdentityDocumentService;
import com.pfr_crud.ssau.service.results.GetStatus;
import com.pfr_crud.ssau.service.results.OperationResult;
import com.pfr_crud.ssau.service.results.UpdateStatus;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;
import java.util.Set;
import java.util.stream.Collectors;

@Service
@RequiredArgsConstructor
public class UsersService {
    private final AuthorizeService authorizeService;
    private final UserRepository userRepository;
    private final AddressService addressService;
    private final IdentityDocumentService documentService;
    private final CitizenDescriptionService descriptionService;

    public OperationResult<UserDTO, UpdateStatus> setRole(Long userId, Role role) {
        Optional<User> foundUser = userRepository.findById(userId);

        if (foundUser.isEmpty()) {
            return new OperationResult<>(UpdateStatus.NOT_FOUND, null,
                    "Пользователь с таким id не найден");
        }

        User user = foundUser.get();

        authorizeService.setRole(user, role.getName());

        User savedUser = userRepository.save(user);

        return new OperationResult<>(UpdateStatus.SUCCESS, new UserDTO(savedUser));
    }

    private FullUserDTO getFullUser(User user) {
        FullUserDTO result = new FullUserDTO();
        result.setId(user.getId());
        result.setLogin(user.getLogin());

        Long descriptionId = user.getDescriptionId();
        Long documentId = user.getDocumentId();
        Long addressId = user.getAddressId();

        if (descriptionId != null)
            result.setDescription(new CitizenDescriptionDTO(descriptionService.get(descriptionId).getResult()));
        else result.setDescription(null);
        if (documentId != null)
            result.setDocument(new IdentityDocumentDTO(documentService.get(documentId).getResult()));
        else result.setDocument(null);
        if (addressId != null)
            result.setAddress(new AddressDTO(addressService.get(addressId).getResult()));
        else result.setAddress(null);

        return result;
    }

    public OperationResult<FullUserDTO, GetStatus> get(Long userId) {
        Optional<User> foundUser = userRepository.findById(userId);

        if (foundUser.isEmpty()) {
            return new OperationResult<>(GetStatus.NOT_FOUND, null,
                    "Пользователь с таким id не найден");
        }

        User user = foundUser.get();

        FullUserDTO result = getFullUser(user);

        return new OperationResult<>(GetStatus.SUCCESS, result);
    }

    public List<FullUserDTO> getAll() {
        List<User> users = userRepository.findAll();

        List<FullUserDTO> fullUsers = users.stream()
                .map(user -> getFullUser(user))
                .collect(Collectors.toList());

        return fullUsers;
    }

    public OperationResult<List<String>, GetStatus> getRoles(Long userId) {
        Optional<User> foundUser = userRepository.findById(userId);

        if (foundUser.isEmpty()) {
            return new OperationResult<>(GetStatus.NOT_FOUND, null,
                    "Пользователь с таким id не найден");
        }

        Set<Role> roles = foundUser.get().getRoles();

        List<String> rolesName = roles.stream().map(role -> role.getName().name()).collect(Collectors.toList());

        return new OperationResult<>(GetStatus.SUCCESS, rolesName);
    }

}
