package com.space.bar.spacebar.orders;

import java.util.*;

public class Menu {
    private static Menu menu = new Menu();
    public static Menu getMenu() {
        return menu;
    }

    private final Map<Integer,MenuItem> drinks = new HashMap<>();

    private Menu() {
        addMenuItem(new MenuItem("Beer", 500, 320, 100));
        addMenuItem(new MenuItem("Beer", 1000, 610, 200));
        addMenuItem(new MenuItem("Water", 750, 300, 80));
        addMenuItem(new MenuItem("Shot", 20, 290, 100));
        addMenuItem(new MenuItem("Cocktail", 200, 560, 180));
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
