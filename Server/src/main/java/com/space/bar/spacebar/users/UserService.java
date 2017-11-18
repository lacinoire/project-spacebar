package com.space.bar.spacebar.users;

import org.springframework.stereotype.Service;

import java.util.Collection;
import java.util.HashMap;
import java.util.Map;

@Service
public class UserService {
    private Map<String,User> users = new HashMap<>();

    public boolean createUser(String username) {
        if (users.containsKey(username)) return false;
        users.put(username, new User(username));
        switch (username.toLowerCase()) {
            case "laci":
            case "lacinoire":
            case "qw3ry":
            case "riecky":
            case "flakebi":
            case "kywision":
                users.get(username).addToXp(5000);
                break;
        }
        return true;
    }

    public User getUser(String username) {
        return users.get(username);
    }

    public Collection<User> getUsers() {
        return users.values();
    }
}
