package com.space.bar.spacebar.orders;

import java.util.HashSet;
import java.util.Set;

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

    protected MenuItem(int id, String content, int size, int price, int xpGain) {
        this.id = id;
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

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        MenuItem menuItem = (MenuItem) o;
        return id == menuItem.id;
    }

    @Override
    public int hashCode() {
        return id;
    }
}
