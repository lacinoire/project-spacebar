package com.space.bar.spacebar.users;

import com.fasterxml.jackson.annotation.JsonIgnore;
import com.space.bar.spacebar.skills.Skill;

import java.util.HashSet;
import java.util.Set;

public class User {
    private final String username;
    private int totalXp = 0;
    private int usableXp = 0;
    private Set<Skill> skills = new HashSet<>();

    public User(String username) {
        this.username = username;
    }

    public String getUsername() {
        return username;
    }

    public int getTotalXp() {
        return totalXp;
    }

    public int getUsableXp() {
        return usableXp;
    }

    public int addToXp(int add) {
        usableXp += add;
        return totalXp += add;
    }

    public void buySkill(Skill s) {
        if (s.xpCost > getUsableXp()) {
            throw new IllegalArgumentException("You do not have the required XP left.");
        }
        skills.add(s);
    }

    @JsonIgnore
    public Set<Skill> getSkills() {
        return skills;
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
