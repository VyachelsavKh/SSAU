package com.pfr_crud.ssau.repository;

import com.pfr_crud.ssau.model.Role;
import com.pfr_crud.ssau.model.User;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

import java.util.List;
import java.util.Optional;

@Repository
public interface UserRepository extends JpaRepository<User, Long> {
    Optional<User> findByLogin(String login);

    @Query(value = """
    SELECT EXISTS (
        SELECT 1
        FROM users u
        JOIN user_roles ur ON u.id = ur.user_id
        JOIN roles r ON r.id = ur.role_id
        GROUP BY u.id
        HAVING COUNT(DISTINCT r.name) = 3
            AND SUM(CASE WHEN r.name = 'ROLE_USER' THEN 1 ELSE 0 END) > 0
            AND SUM(CASE WHEN r.name = 'ROLE_WORKER' THEN 1 ELSE 0 END) > 0
            AND SUM(CASE WHEN r.name = 'ROLE_ADMIN' THEN 1 ELSE 0 END) > 0
    )
    """, nativeQuery = true)
    boolean existsUserWithAllRoles();

    @Query("SELECT COUNT(u) FROM User u WHERE u.addressId = :id")
    Long countByAddressId(@Param("id") Long id);
}