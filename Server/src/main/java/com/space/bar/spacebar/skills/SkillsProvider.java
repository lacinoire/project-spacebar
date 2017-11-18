package com.space.bar.spacebar.skills;

import com.space.bar.spacebar.StorageWriter;
import com.space.bar.spacebar.orders.MenuItem;

import javax.annotation.PreDestroy;
import java.util.HashMap;
import java.util.Set;
import java.util.stream.Collectors;

public class SkillsProvider {
    private HashMap<Skill, Boolean> allSkills = new HashMap<>();

    public SkillsProvider() {
        Skill d1 = new DiscountSkill("2% discount on everything", 500, applyDiscount(0.98f));
        Skill d2 = new DiscountSkill("4% discount on everything", 1000, applyDiscount(0.96f / 0.98f));
        Skill d3 = new DiscountSkill("6% discount on everything", 1500, applyDiscount(0.94f / 0.96f));
        d1.nextSkills.add(d2.id);
        d2.nextSkills.add(d3.id);

        Skill other = new DiscountSkill("5% discount on non-alcoholic drinks", 630, item -> item.getContent().equals("Water") ? applyDiscount(0.95f).apply(item) : item);

        Skill bigCocktails = new UnlockSkill("Get access to the BIG cocktails",999,
                o -> o instanceof MenuItem
                        && ((MenuItem) o).getContent().equalsIgnoreCase("Cocktail")
                        && ((MenuItem) o).getSize() == 1000);

        allSkills.put(d1, true);
        allSkills.put(d2, false);
        allSkills.put(d3, false);
        allSkills.put(other, true);
        allSkills.put(bigCocktails, true);
    }

    public SkillsProvider(HashMap<Skill, Boolean> skills) {
        this.allSkills = skills;
    }

    @PreDestroy
    public void store() {
        StorageWriter.saveToFile(allSkills, "skills.ser");
    }

    public Set<Skill> getBasicSkills() {
        return getAllSkills().stream().filter(s -> allSkills.get(s)).collect(Collectors.toSet());
    }

    public Set<Skill> getAllSkills() {
        return allSkills.keySet();
    }

    private static DiscountSkill.MappingFunction applyDiscount(float multiplier) {
        return miv -> {
            miv.setPrice((int) (miv.getPrice() * multiplier));
            return miv;
        };
    }
}
