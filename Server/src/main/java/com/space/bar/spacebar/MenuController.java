package com.space.bar.spacebar;

import com.space.bar.spacebar.network.ErrorResponse;
import com.space.bar.spacebar.orders.ItemProvider;
import com.space.bar.spacebar.orders.MenuView;
import com.space.bar.spacebar.skills.SkillService;
import com.space.bar.spacebar.users.User;
import com.space.bar.spacebar.users.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
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
    private final ItemProvider itemProvider;

    @Autowired
    public MenuController(UserService users, SkillService skills, ItemProvider itemProvider) {
        this.users = users;
        this.skills = skills;
        this.itemProvider = itemProvider;
    }

    @GetMapping
    public ResponseEntity<?> getMenu(@RequestParam("username") String username) {
        if (users.getUser(username) == null) {
            return ResponseEntity.status(HttpStatus.UNAUTHORIZED).body(new ErrorResponse("User not found"));
        }
        User user = users.getUser(username);
        MenuView view = new MenuView(skills.getPurchased(user).collect(Collectors.toList()), itemProvider.getAllItems(), skills);
        return ResponseEntity.ok().body(view);
    }
}
