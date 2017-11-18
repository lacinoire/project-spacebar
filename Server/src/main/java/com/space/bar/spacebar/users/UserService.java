package com.space.bar.spacebar.users;

import com.space.bar.spacebar.StorageWriter;

import javax.annotation.PreDestroy;
import java.util.Collection;
import java.util.HashMap;

public class UserService {
    private HashMap<String,User> users;

    public UserService(HashMap<String, User> users) {
        this.users = users;
    }

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

    @PreDestroy
    public void store() {
        StorageWriter.saveToFile(users, "users.ser");
    }

    public User getUser(String username) {
        return users.get(username);
    }

    public Collection<User> getUsers() {
        return users.values();
    }
}
