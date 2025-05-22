CREATE TABLE IF NOT EXISTS identity_documents (
    id SERIAL PRIMARY KEY,
    document_type_id INTEGER NOT NULL,
    series     VARCHAR(255) NOT NULL,
    number     VARCHAR(255) NOT NULL,
    issue_date DATE,
    issued_by  VARCHAR(255),
    CONSTRAINT uq_identity_doc UNIQUE (document_type_id, series, number),
    CONSTRAINT fk_doc_type
        FOREIGN KEY (document_type_id)
            REFERENCES document_types(id)
            ON DELETE CASCADE
);