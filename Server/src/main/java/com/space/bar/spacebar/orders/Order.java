package com.space.bar.spacebar.orders;

import com.space.bar.spacebar.users.User;

public class Order {
    private static int nextId = 1;

    public class Status {
        private boolean isClaimed = false;
        private boolean isFinished = false;
        private boolean isApproved = false;

        public boolean isFinished() {
            return isFinished;
        }

        public boolean isApproved() {
            return isApproved;
        }

        public boolean isClaimed() {
            return isClaimed;
        }
    }

    private final int id;
    private final MenuItem item;
    private final String fromUser;
    private String assignee = null;
    private Status status = new Status();

    public Order(MenuItem item, String fromUser) {
        this.id = nextId++;
        this.item = item;
        this.fromUser = fromUser;
    }

    public void setAssignee(String user) {
        if (status.isClaimed) throw new IllegalStateException();
        this.assignee = user;
        this.status.isClaimed = true;
    }

    public void finishOrder() {
        if (!status.isClaimed || status.isFinished) throw new IllegalStateException();
        this.status.isFinished = true;
    }

    public void approveOrder() {
        if (!status.isClaimed || status.isApproved) throw new IllegalStateException();
        this.status.isApproved = true;
    }

    public int getId() {
        return id;
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
