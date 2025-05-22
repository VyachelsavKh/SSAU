package com.pfr_crud.ssau.service.user;

import com.pfr_crud.ssau.dto.AddressDTO;
import com.pfr_crud.ssau.dto.CitizenDescriptionDTO;
import com.pfr_crud.ssau.dto.IdentityDocumentDTO;
import com.pfr_crud.ssau.model.*;
import com.pfr_crud.ssau.repository.UserRepository;
import com.pfr_crud.ssau.service.PaymentService;
import com.pfr_crud.ssau.service.address.AddressService;
import com.pfr_crud.ssau.service.CitizenDescriptionService;
import com.pfr_crud.ssau.service.identity_document.IdentityDocumentService;
import com.pfr_crud.ssau.service.results.*;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;

import java.util.Optional;
import java.util.List;

@Service
@RequiredArgsConstructor
public class UserService {
    private final UserRepository userRepository;
    private final IdentityDocumentService identityDocumentService;
    private final CitizenDescriptionService citizenDescriptionService;
    private final AddressService addressService;
    private final PaymentService paymentService;

    public OperationResult<IdentityDocument, GetStatus> getDocument(User user) {
        if (user.getDocumentId() != null) {
            return identityDocumentService.get(user.getDocumentId());
        } else {
            return new OperationResult<>(GetStatus.NOT_FOUND, null,
                    "У пользователя ещё не привязан документ, удостоверяющий личность");
        }
    }

    public OperationResult<IdentityDocument, ? extends OperationStatus> updateDocument(User user, IdentityDocumentDTO newDocument) {
        if (user.getDocumentId() == null) {
            OperationResult<IdentityDocument, CreateStatus> createdDocument = identityDocumentService.create(newDocument);
            if (createdDocument.getStatus() == CreateStatus.SUCCESS) {
                OperationResult<IdentityDocument, GetStatus> savedDocument = identityDocumentService.get(createdDocument.getResult().getId());
                user.setDocumentId(savedDocument.getResult().getId());
                userRepository.save(user);
            }
            return createdDocument;
        } else {
            return identityDocumentService.update(user.getDocumentId(), newDocument);
        }
    }

    public OperationResult<CitizenDescription, GetStatus> getDescription(User user) {
        if (user.getDescriptionId() != null) {
            return citizenDescriptionService.get(user.getDescriptionId());
        } else {
            return new OperationResult<>(GetStatus.NOT_FOUND, null,
                    "У пользователя ещё не привязана личная информация");
        }
    }

    public OperationResult<CitizenDescription, ? extends OperationStatus> updateDescription(User user, CitizenDescriptionDTO newDescription) {
        if (user.getDescriptionId() == null) {
            OperationResult<CitizenDescription, CreateStatus> createdDescription = citizenDescriptionService.create(newDescription);
            if (createdDescription.getStatus() == CreateStatus.SUCCESS) {
                OperationResult<CitizenDescription, GetStatus> savedDescription = citizenDescriptionService.get(createdDescription.getResult().getId());
                user.setDescriptionId(savedDescription.getResult().getId());
                userRepository.save(user);
            }
            return createdDescription;
        } else {
            return citizenDescriptionService.update(user.getDescriptionId(), newDescription);
        }
    }

    public OperationResult<Address, GetStatus> getAddress(User user) {
        if (user.getAddressId() != null) {
            return addressService.get(user.getAddressId());
        } else {
            return new OperationResult<>(GetStatus.NOT_FOUND, null,
                    "У пользователя ещё не привязан адрес проживания");
        }
    }

    private OperationResult<Address, GetStatus> setAddress(User user, Long addressId) {
        OperationResult<Address, GetStatus> foundAddress = addressService.get(addressId);
        user.setAddressId(foundAddress.getResult().getId());
        userRepository.save(user);
        return new OperationResult<>(foundAddress.getStatus(), foundAddress.getResult());
    }

    public OperationResult<Address, ? extends OperationStatus> updateAddress(User user, AddressDTO newAddress) {
        Optional<Long> existingAddressId = addressService.findIdByDTO(newAddress);

        System.out.println(newAddress);

        if (existingAddressId.isPresent())
            return setAddress(user, existingAddressId.get());
        else {
            OperationResult<Address, CreateStatus> createdAddress = addressService.create(newAddress);
            if (createdAddress.getStatus() == CreateStatus.SUCCESS) {
                Long addressId = createdAddress.getResult().getId();
                setAddress(user, addressId);
            }
            return createdAddress;
        }
    }

    public List<Payment> getAllPayments(User user) {
        return paymentService.getAllByUserId(user.getId());
    }
}
