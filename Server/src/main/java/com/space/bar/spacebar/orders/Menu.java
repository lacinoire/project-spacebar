package com.space.bar.spacebar.orders;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.*;

@Service
public class Menu {
    private final Map<Integer,MenuItem> drinks = new HashMap<>();

    @Autowired public Menu(Set<MenuItem> menuItems) {
        menuItems.forEach(this::addMenuItem);
    }

    public Collection<MenuItem> getDrinks() {
        return drinks.values();
    }

    public MenuItem getMenuItemById(int id) {
        if (drinks.containsKey(id)) {
            return drinks.get(id);
        } else {
            return null;
        }
    }

    private void addMenuItem(MenuItem item) {
        drinks.put(item.getId(), item);
    }
}
