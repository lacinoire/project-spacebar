package com.space.bar.spacebar.network;

public class ErrorResponse {
    private final String error;

    public ErrorResponse(String msg) {
        this.error = msg;
    }

    public String getError() {
        return error;
    }
}
