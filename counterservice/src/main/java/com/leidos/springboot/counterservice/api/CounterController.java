package com.leidos.springboot.counterservice.api;


import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RestController;
import java.util.concurrent.atomic.AtomicLong;

@RestController
public class CounterController {

    private static AtomicLong count = new AtomicLong(0);

    @GetMapping(path = "/count")
    public long getCount(){
        return count.getAndIncrement();
    }
}
