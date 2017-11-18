package com.space.bar.spacebar.orders;

import com.space.bar.spacebar.users.User;

public class Order {
    public enum Status {
        OPEN,
        ASSIGNED,
        FINISHED,
        APPROVED
    }

    private final MenuItem item;
    private final String fromUser;
    private String assignee = null;
    private Status status = Status.OPEN;

    public Order(MenuItem item, String fromUser) {
        this.item = item;
        this.fromUser = fromUser;
    }

    public void setAssignee(String user) {
        if (status != Status.OPEN) throw new IllegalStateException();
        this.assignee = user;
        this.status = Status.ASSIGNED;
    }

    public void finishOrder() {
        this.status = Status.FINISHED;
    }

    public void approveOrder() {
        this.status = Status.APPROVED;
    }

    public MenuItem getItem() {
        return item;
    }

    public String getFromUser() {
        return fromUser;
    }

    public String getAssignee() {
        return assignee;
    }

    public Status getStatus() {
        return status;
    }
}
