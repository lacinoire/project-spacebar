package com.space.bar.spacebar;

import com.space.bar.spacebar.data.ErrorResponse;
import com.space.bar.spacebar.users.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RestController;

import java.util.HashMap;
import java.util.Map;

@RestController
public class Controller {
    @Autowired
    UserService service;

    @PostMapping
    public ResponseEntity<?> createUser(@RequestBody Map<String, String> user) {
        Map<String, String> retVal = new HashMap<>(1);
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
}
