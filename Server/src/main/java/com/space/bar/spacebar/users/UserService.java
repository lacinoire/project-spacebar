package com.space.bar.spacebar.users;

import org.springframework.stereotype.Service;

import java.util.HashSet;
import java.util.Set;

@Service
public class UserService {
    private Set<String> users = new HashSet<>();

    public boolean createUser(String username) {
        return users.add(username);
    }
}
