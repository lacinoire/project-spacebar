package com.space.bar.spacebar.network;

public class OrderApprovement {
    private final String username;
    private final int order;

    public OrderApprovement(String username, int order) {
        this.username = username;
        this.order = order;
    }

    public String getUsername() {
        return username;
    }

    public int getOrder() {
        return order;
    }
}
