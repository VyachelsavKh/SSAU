CREATE TABLE IF NOT EXISTS addresses (
    id SERIAL PRIMARY KEY,
    city_id   INTEGER NOT NULL,
    street_id INTEGER NOT NULL,
    house_number     VARCHAR(255) NOT NULL,
    apartment_number VARCHAR(255) NOT NULL,

    CONSTRAINT uq_address_unique UNIQUE (city_id, street_id, house_number, apartment_number),

    CONSTRAINT fk_address_city
        FOREIGN KEY (city_id)
            REFERENCES cities(id)
                ON DELETE CASCADE,

    CONSTRAINT fk_address_street
        FOREIGN KEY (street_id)
            REFERENCES streets(id)
                ON DELETE CASCADE
);