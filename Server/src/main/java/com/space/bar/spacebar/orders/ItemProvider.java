package com.space.bar.spacebar.orders;

import com.space.bar.spacebar.StorageWriter;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.context.annotation.Primary;
import org.springframework.stereotype.Component;
import org.springframework.stereotype.Service;

import javax.annotation.PreDestroy;
import java.util.HashSet;
import java.util.Set;

public class ItemProvider {
    private HashSet<MenuItem> allItems = new HashSet<>();

    public ItemProvider() {
        allItems.add(new MenuItem("Beer", 500, 320, 100));
        allItems.add(new MenuItem("Beer", 1000, 610, 200));
        allItems.add(new MenuItem("Water", 750, 300, 80));
        allItems.add(new MenuItem("Shot", 20, 290, 100));
        allItems.add(new MenuItem("Cocktail", 200, 560, 180));
        allItems.add(new MenuItem("Cocktail", 1000, 2000, 1000));
    }

    public ItemProvider(HashSet<MenuItem> items) {
        allItems = items;
    }

    @PreDestroy
    public void store() {
        StorageWriter.saveToFile(allItems, "items.ser");
    }

    public Set<MenuItem> getAllItems() {
        return allItems;
    }
}
