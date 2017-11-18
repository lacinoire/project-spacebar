package com.space.bar.spacebar.network;

public class OrderCreationRequest {
    private final String username;
    private final int item;

    public OrderCreationRequest(String username, int item) {
        this.username = username;
        this.item = item;
    }

    public String getUsername() {
        return username;
    }

    public int getItem() {
        return item;
    }
}
