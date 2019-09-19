package com.leidos.springboot.gatewayService.repository;

import com.leidos.springboot.gatewayService.bean.auth.User;
import org.springframework.data.mongodb.repository.MongoRepository;
import org.springframework.data.mongodb.repository.Query;
import org.springframework.stereotype.Repository;

@Repository
public interface UserRepository extends MongoRepository<User,String> {
    @Query(value="{'email' : ?0}")
    User findByEmail(String email);
}
