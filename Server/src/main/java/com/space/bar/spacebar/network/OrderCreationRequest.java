package com.space.bar.spacebar.network;

public class OrderCreationRequest {
    private String username;
    private int item;

    public OrderCreationRequest() {}

    public void setUsername(String username) {
        this.username = username;
    }

    public void setItem(int item) {
        this.item = item;
    }

    public String getUsername() {
        return username;
    }

    public int getItem() {
        return item;
    }
}
