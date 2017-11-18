package com.space.bar.spacebar.network;

public class BuySkillRequest {
    private int skill;
    private String username;

    public void setSkill(int skill) {
        this.skill = skill;
    }

    public void setUsername(String username) {
        this.username = username;
    }

    public int getSkill() {
        return skill;
    }

    public String getUsername() {
        return username;
    }
}
