package com.space.bar.spacebar.users;

import org.springframework.stereotype.Service;

import java.util.HashMap;
import java.util.Map;

@Service
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
