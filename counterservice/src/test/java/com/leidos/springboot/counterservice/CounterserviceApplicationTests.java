package com.leidos.springboot.counterservice;

import com.leidos.springboot.counterservice.api.CounterController;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.boot.test.autoconfigure.web.servlet.WebMvcTest;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.context.junit4.SpringRunner;

//@RunWith(SpringRunner.class)
//@SpringBootTest
@WebMvcTest(CounterController.class)
public class CounterserviceApplicationTests {

	@Test
	public void contextLoads() {
		//assert(false);
	}

}
