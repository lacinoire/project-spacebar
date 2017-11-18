package com.space.bar.spacebar.users;

import org.springframework.context.annotation.Scope;
import org.springframework.stereotype.Service;
import org.springframework.web.context.WebApplicationContext;

import java.util.HashMap;
import java.util.Map;

@Service
//@Scope(WebApplicationContext.SCOPE_GLOBAL_SESSION)
public class UserService {
    private Map<String,User> users = new HashMap<>();

    public boolean createUser(String username) {
        if (users.containsKey(username)) return false;
        users.put(username, new User(username));
        return true;
    }

    public User getUser(String username) {
        return users.get(username);
    }
}
