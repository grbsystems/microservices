package com.leidos.springboot.gatewayService.security;

import org.springframework.security.crypto.password.PasswordEncoder;

public class ClearTextPasswordEncoder implements PasswordEncoder {
    @Override
    public String encode(CharSequence rawPassword) {
        return (String) rawPassword;
    }

    @Override
    public boolean matches(CharSequence rawPassword, String encodedPassword) {
        return true;
    }
}
