package com.pfr_crud.ssau.dto;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@Builder
@NoArgsConstructor
@AllArgsConstructor
public class FullUserDTO {
    private Long id;
    private String login;
    private CitizenDescriptionDTO description;
    private IdentityDocumentDTO document;
    private AddressDTO address;
}
