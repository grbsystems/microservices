package com.leidos.springboot.gatewayService.service;

import com.leidos.springboot.gatewayService.bean.auth.User;

public interface ILoginService {
    String login(String username, String password);
    User saveUser(User user);

    boolean logout(String token);

    Boolean isValidToken(String token);

    String createNewToken(String token);
}
