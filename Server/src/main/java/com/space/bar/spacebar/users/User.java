package com.space.bar.spacebar.users;

public class User {
    private final String username;
    private int totalXp = 0;


    public User(String username) {
        this.username = username;
    }

    public String getUsername() {
        return username;
    }

    public int getTotalXp() {
        return totalXp;
    }

    public int addToXp(int add) {
        return totalXp += add;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        User user = (User) o;
        return username != null ? username.equals(user.username) : user.username == null;
    }

    @Override
    public int hashCode() {
        return username != null ? username.hashCode() : 0;
    }
}
