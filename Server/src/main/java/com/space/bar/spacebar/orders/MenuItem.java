package com.space.bar.spacebar.orders;

public class MenuItem {
    private static int nextId = 1;

    private final int id;
    private final String content;
    private final int size;
    private final int price;
    private final int xpGain;

    public MenuItem(String content, int size, int price, int xpGain) {
        this.id = nextId++;
        this.content = content;
        this.size = size;
        this.price = price;
        this.xpGain = xpGain;
    }

    public int getId() {
        return id;
    }

    public String getContent() {
        return content;
    }

    public int getSize() {
        return size;
    }

    public int getPrice() {
        return price;
    }

    public int getXpGain() {
        return xpGain;
    }
}
