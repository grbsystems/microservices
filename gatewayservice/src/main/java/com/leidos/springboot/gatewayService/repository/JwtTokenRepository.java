package com.leidos.springboot.gatewayService.repository;

import com.leidos.springboot.gatewayService.bean.auth.JwtToken;
import org.springframework.data.mongodb.repository.MongoRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface JwtTokenRepository extends MongoRepository<JwtToken,String> {
}
