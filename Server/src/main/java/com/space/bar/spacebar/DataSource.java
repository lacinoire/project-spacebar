package com.space.bar.spacebar;

import com.space.bar.spacebar.orders.MenuItem;
import org.springframework.context.annotation.Bean;
import org.springframework.stereotype.Component;

import java.util.HashSet;
import java.util.Set;

@Component
public class DataSource {
    private static Set<MenuItem> allItems = new HashSet<>();

    static {
        allItems.add(new MenuItem("Beer", 500, 320, 100));
        allItems.add(new MenuItem("Beer", 1000, 610, 200));
        allItems.add(new MenuItem("Water", 750, 300, 80));
        allItems.add(new MenuItem("Shot", 20, 290, 100));
        allItems.add(new MenuItem("Cocktail", 200, 560, 180));
    }

    @Bean
    public static Set<MenuItem> getAllItems() {
        return allItems;
    }
}
