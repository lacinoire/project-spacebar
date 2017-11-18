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
    private final User fromUser;
    private User assignee = null;
    private Status status = Status.OPEN;

    public Order(MenuItem item, User fromUser) {
        this.item = item;
        this.fromUser = fromUser;
    }

    public void setAssignee(User user) {
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

    public User getFromUser() {
        return fromUser;
    }

    public User getAssignee() {
        return assignee;
    }

    public Status getStatus() {
        return status;
    }
}
