package com.space.bar.spacebar.orders;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.*;

@Service
public class Menu {
    private final Map<Integer,MenuItem> drinks = new HashMap<>();

    @Autowired
    public Menu(ItemProvider items) {
        this(items.getAllItems());
    }

    public Menu(Collection<MenuItem> items) {
        items.forEach(this::addMenuItem);
    }

    public Collection<MenuItem> getDrinks() {
        return drinks.values();
    }

    public MenuItem getMenuItemById(int id) {
        return drinks.getOrDefault(id, null);
    }

    private void addMenuItem(MenuItem item) {
        drinks.put(item.getId(), item);
    }
}
