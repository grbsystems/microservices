package com.leidos.springboot.configurationservice;

import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.boot.test.autoconfigure.web.servlet.WebMvcTest;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.context.junit4.SpringRunner;

//@RunWith(SpringRunner.class)
//@SpringBootTest
@WebMvcTest
public class ConfigurationserviceApplicationTests {

	@Test
	public void contextLoads() {
		Foo foo = new Foo();

		foo.Add(4, 6);
	}

}
