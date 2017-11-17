package com.space.bar.spacebar.users;

import org.springframework.stereotype.Service;

import java.util.Set;

@Service
public class UserService {
    private Set<String> users;

    public boolean createUser(String username) {
        return users.add(username);
    }
}
