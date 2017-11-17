package com.space.bar.spacebar;

import com.space.bar.spacebar.users.UserService;
import org.springframework.beans.factory.annotation.Autowired;
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
    public Map<String, String> createUser(@RequestBody Map<String, String> user) {
        Map<String, String> retVal = new HashMap<>(1);
        if (user.size() != 1) {
            retVal.put("error", "Please add exactly 1 user per call.");
        } else if (!user.containsKey("username")) {
            retVal.put("error", "Please add the attribute 'username'.");
        } else {
            boolean created = service.createUser(user.get("username"));
            if (created) {
                retVal.put("created", "true");
            } else {
                retVal.put("error", "username taken.");
            }
        }
        return retVal;
    }
}
