DROP TRIGGER IF EXISTS trigger_before_delete_user ON users;
DROP TRIGGER IF EXISTS trigger_delete_user ON users;
DROP TRIGGER IF EXISTS trigger_delete_description ON citizen_descriptions;
DROP TRIGGER IF EXISTS trigger_delete_document ON identity_documents;

DROP FUNCTION IF EXISTS delete_user_payments_cascade();
DROP FUNCTION IF EXISTS delete_user_cascade();
DROP FUNCTION IF EXISTS delete_description_cascade();
DROP FUNCTION IF EXISTS delete_document_cascade();

CREATE OR REPLACE FUNCTION delete_user_payments_cascade()
    RETURNS TRIGGER AS $$
BEGIN
    DELETE FROM payments WHERE user_id = OLD.id;

    RETURN OLD;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION delete_user_cascade()
RETURNS TRIGGER AS $$
BEGIN
    IF OLD.description_id IS NOT NULL THEN
        DELETE FROM citizen_descriptions WHERE id = OLD.description_id;
    END IF;

    IF OLD.document_id IS NOT NULL THEN
        DELETE FROM identity_documents WHERE id = OLD.document_id;
    END IF;

    RETURN OLD;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION delete_description_cascade()
RETURNS TRIGGER AS $$
DECLARE
    user_record RECORD;
BEGIN
    SELECT * INTO user_record
    FROM users
    WHERE description_id = OLD.id;

    IF FOUND THEN
        IF user_record.document_id IS NOT NULL THEN
			UPDATE users SET document_id = NULL WHERE document_id = user_record.document_id;
            DELETE FROM identity_documents WHERE id = user_record.document_id;
        END IF;
		UPDATE users SET description_id = NULL WHERE description_id = OLD.id;
        DELETE FROM users WHERE id = user_record.id;
    END IF;

    RETURN OLD;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION delete_document_cascade()
RETURNS TRIGGER AS $$
DECLARE
    user_record RECORD;
BEGIN
    SELECT * INTO user_record
    FROM users
    WHERE document_id = OLD.id;

    IF FOUND THEN
        IF user_record.description_id IS NOT NULL THEN
			UPDATE users SET description_id = NULL WHERE description_id = user_record.description_id;
            DELETE FROM citizen_descriptions WHERE id = user_record.description_id;
        END IF;
		UPDATE users SET document_id = NULL WHERE document_id = OLD.id;
        DELETE FROM users WHERE id = user_record.id;
    END IF;

    RETURN OLD;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trigger_before_delete_user
    BEFORE DELETE ON users
    FOR EACH ROW
EXECUTE FUNCTION delete_user_payments_cascade();

CREATE TRIGGER trigger_delete_user
AFTER DELETE ON users
FOR EACH ROW
EXECUTE FUNCTION delete_user_cascade();

CREATE TRIGGER trigger_delete_description
BEFORE DELETE ON citizen_descriptions
FOR EACH ROW
EXECUTE FUNCTION delete_description_cascade();

CREATE TRIGGER trigger_delete_document
BEFORE DELETE ON identity_documents
FOR EACH ROW
EXECUTE FUNCTION delete_document_cascade();