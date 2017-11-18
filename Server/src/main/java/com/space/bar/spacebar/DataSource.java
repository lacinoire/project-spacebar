package com.space.bar.spacebar;

import com.fasterxml.classmate.types.ResolvedPrimitiveType;
import com.space.bar.spacebar.orders.MenuItem;
import com.space.bar.spacebar.orders.MenuView;
import com.space.bar.spacebar.skills.DiscountSkill;
import com.space.bar.spacebar.skills.Skill;
import org.springframework.context.annotation.Bean;
import org.springframework.stereotype.Component;

import java.util.HashMap;
import java.util.HashSet;
import java.util.Map;
import java.util.Set;
import java.util.function.Function;
import java.util.stream.Collectors;

@Component
public class DataSource {
    private static Set<MenuItem> allItems = new HashSet<>();
    private static Map<Skill, Boolean> allSkills = new HashMap<>();

    static {
        allItems.add(new MenuItem("Beer", 500, 320, 100));
        allItems.add(new MenuItem("Beer", 1000, 610, 200));
        allItems.add(new MenuItem("Water", 750, 300, 80));
        allItems.add(new MenuItem("Shot", 20, 290, 100));
        allItems.add(new MenuItem("Cocktail", 200, 560, 180));

        Skill d1 = new DiscountSkill("2% discount on everything",  500, applyDiscount(0.98f));
        Skill d2 = new DiscountSkill("4% discount on everything", 1000, applyDiscount(0.96f / 0.98f));
        Skill d3 = new DiscountSkill("6% discount on everything", 1500, applyDiscount(0.94f / 0.96f));
        d1.nextSkills.add(d2.id);
        d2.nextSkills.add(d3.id);

        Skill other = new DiscountSkill("5% discount on non-alcoholic drinks", 630, item -> item.getContent().equals("Water")? applyDiscount(0.95f).apply(item) : item);

        allSkills.put(d1, true);
        allSkills.put(d2, false);
        allSkills.put(d3, false);
        allSkills.put(other, true);
    }

    @Bean
    public static Set<MenuItem> getAllItems() {
        return allItems;
    }

    public static Set<Skill> getBasicSkills() {
        return getAllSkills().stream().filter(s -> allSkills.get(s)).collect(Collectors.toSet());
    }

    @Bean
    public static Set<Skill> getAllSkills() {
        return allSkills.keySet();
    }

    private static Function<MenuView.MenuItemView,MenuView.MenuItemView> applyDiscount(float multiplier) {
        return miv -> {
            miv.setPrice((int)(miv.getPrice() * multiplier));
            return miv;
        };
    }
}
