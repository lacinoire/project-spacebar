package com.space.bar.spacebar;

import com.space.bar.spacebar.responses.ErrorResponse;
import com.space.bar.spacebar.users.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.Map;

@RestController
@RequestMapping("users")
public class UserController {
    private UserService service;

    @Autowired public UserController(UserService service) {
        this.service = service;
    }

    @PostMapping
    public ResponseEntity<?> createUser(@RequestBody Map<String, String> user) {
        if (user.size() != 1) {
            return ResponseEntity.badRequest().body(new ErrorResponse("Please add exactly 1 user per call."));
        } else if (!user.containsKey("username")) {
            return ResponseEntity.badRequest().body(new ErrorResponse("Please add the attribute 'username'."));
        } else {
            boolean created = service.createUser(user.get("username"));
            if (created) {
                return ResponseEntity.ok().build();
            } else {
                return ResponseEntity.badRequest().body(new ErrorResponse("username taken."));
            }
        }
    }

    @GetMapping
    public ResponseEntity<?> getUser(@RequestParam("username") String username) {
        return ResponseEntity.ok().body(service.getUser(username));
    }
}
