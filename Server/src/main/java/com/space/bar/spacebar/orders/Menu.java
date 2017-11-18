package com.space.bar.spacebar.orders;

import java.util.Collections;
import java.util.HashSet;
import java.util.Set;

public class Menu {
    private static Menu menu = new Menu();
    public static Menu getMenu() {
        return menu;
    }

    private final Set<MenuItem> drinks = new HashSet<>();

    private Menu() {
        drinks.add(new MenuItem("Beer", 500, 320, 100));
        drinks.add(new MenuItem("Beer", 1000, 610, 200));
        drinks.add(new MenuItem("Water", 750, 300, 80));
        drinks.add(new MenuItem("Shot", 20, 290, 100));
        drinks.add(new MenuItem("Cocktail", 200, 560, 180));
    }

    public Set<MenuItem> getDrinks() {
        return Collections.unmodifiableSet(drinks);
    }
}
