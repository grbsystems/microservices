package com.leidos.springboot.counterservice.api;


import org.springframework.beans.factory.annotation.Value;
import org.springframework.boot.autoconfigure.EnableAutoConfiguration;
import org.springframework.cloud.client.discovery.EnableDiscoveryClient;
import org.springframework.cloud.context.config.annotation.RefreshScope;
import org.springframework.context.annotation.Configuration;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RestController;
import java.util.concurrent.atomic.AtomicLong;

@RestController
@Configuration
@EnableAutoConfiguration
@RefreshScope
@EnableDiscoveryClient
public class CounterController {

    private static AtomicLong count = new AtomicLong(0);

    @Value(value = "${counter.prefixMessage}")
    private String prefixMessage;

    @GetMapping(path = "/count")
    public String getCount(){
        return prefixMessage + count.getAndIncrement();
    }
}
