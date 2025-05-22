CREATE TABLE IF NOT EXISTS citizen_descriptions (
    id SERIAL PRIMARY KEY,
    last_name   VARCHAR(255) NOT NULL,
    first_name  VARCHAR(255) NOT NULL,
    middle_name VARCHAR(255) NOT NULL,
    birth_date  VARCHAR(255) NOT NULL,
    gender SMALLINT,

    CONSTRAINT uq_description UNIQUE (last_name, first_name, middle_name, birth_date)
);