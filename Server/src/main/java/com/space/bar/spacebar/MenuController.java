package com.space.bar.spacebar;

import com.space.bar.spacebar.orders.Menu;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("menu")
public class MenuController {
    @GetMapping
    public ResponseEntity<?> getMenu(@RequestParam("username") String username) {
        return ResponseEntity.ok().body(Menu.getMenu());
    }
}
