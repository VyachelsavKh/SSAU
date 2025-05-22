package com.pfr_crud.ssau.controller;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;

@Controller
public class SpaRedirectController {

    @RequestMapping(value = {
            "/",
            "/{path:^(?!api|static|.*\\..*).*$}",
            "/{path:^(?!api|static|.*\\..*).*$}/**"
    })
    public String forward() {
        return "forward:/index.html";
    }
}