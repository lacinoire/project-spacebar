package com.space.bar.spacebar;

import com.space.bar.spacebar.orders.Menu;
import com.space.bar.spacebar.orders.MenuView;
import com.space.bar.spacebar.skills.SkillService;
import com.space.bar.spacebar.users.User;
import com.space.bar.spacebar.users.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import java.util.stream.Collectors;

@RestController
@RequestMapping("menu")
public class MenuController {
    private final UserService users;
    private final SkillService skills;

    @Autowired
    public MenuController(UserService users, SkillService skills) {
        this.users = users;
        this.skills = skills;
    }

    @GetMapping
    public ResponseEntity<?> getMenu(@RequestParam("username") String username) {
        User user = users.getUser(username);
        MenuView view = new MenuView(skills.getPurchased(user).collect(Collectors.toList()));
        return ResponseEntity.ok().body(view);
    }
}
