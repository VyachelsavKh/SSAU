package com.pfrpractice.pfr;

import org.springframework.boot.SpringApplication;

public class TestPfrApplication {

	public static void main(String[] args) {
		SpringApplication.from(PfrApplication::main).with(TestcontainersConfiguration.class).run(args);
	}

}
