CREATE TABLE IF NOT EXISTS users (
    id             SERIAL PRIMARY KEY,
    login          VARCHAR(255) UNIQUE NOT NULL,
    password       VARCHAR(255) NOT NULL,
    enabled        BOOLEAN NOT NULL DEFAULT TRUE,
    address_id     INTEGER,
    description_id INTEGER UNIQUE,
    document_id    INTEGER UNIQUE,

    CONSTRAINT fk_user_address
        FOREIGN KEY (address_id)
            REFERENCES addresses(id)
                ON DELETE CASCADE,

    CONSTRAINT fk_user_description
        FOREIGN KEY (description_id)
            REFERENCES citizen_descriptions(id),

    CONSTRAINT fk_user_document
        FOREIGN KEY (document_id)
            REFERENCES identity_documents(id)
);