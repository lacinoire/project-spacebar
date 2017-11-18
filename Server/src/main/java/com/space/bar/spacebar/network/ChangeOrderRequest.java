package com.space.bar.spacebar.network;

public class ChangeOrderRequest {
    private String username;
    private int order;

    public void setUsername(String username) {
        this.username = username;
    }

    public void setOrder(int order) {
        this.order = order;
    }

    public String getUsername() {
        return username;
    }

    public int getOrder() {
        return order;
    }
}
