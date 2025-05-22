CREATE TABLE IF NOT EXISTS payments (
    id SERIAL PRIMARY KEY,
    user_id INTEGER NOT NULL,
    payment_date DATE NOT NULL,
    amount DECIMAL(10, 2) CHECK (amount > 0),
    is_paid BOOLEAN NOT NULL DEFAULT FALSE,

    CONSTRAINT fk_payment_user
        FOREIGN KEY (user_id)
            REFERENCES users(id),

    CONSTRAINT uq_payment UNIQUE (user_id, payment_date)
);