package com.space.bar.spacebar.orders;

import com.space.bar.spacebar.skills.*;

import java.util.Collection;
import java.util.Optional;
import java.util.Set;
import java.util.stream.Collectors;

public class MenuView extends Menu {
    private final SkillService skillService;

    public class MenuItemView extends MenuItem {
        private int price;
        MenuItemView(MenuItem item) {
            super(item.getId(), item.getContent(), item.getSize(), item.getPrice(), item.getXpGain());
            this.price = item.getPrice();
        }

        public void setPrice(int price) {
            this.price = price;
        }

        @Override
        public int getPrice() {
            return price;
        }
    }

    private Collection<Skill> skills;

    public MenuView(Collection<Skill> skills, Set<MenuItem> allMenuItems, SkillService skillService) {
        super(allMenuItems);
        this.skills = skills;
        this.skillService = skillService;
    }

    @Override
    public Collection<MenuItem> getDrinks() {
        return super.getDrinks().stream().filter(this::isAvailable).map(this::getItemWithDisount).collect(Collectors.toSet());
    }

    @Override
    public MenuItem getMenuItemById(int id) {
        MenuItem item = getItemWithDisount(super.getMenuItemById(id));
        return isAvailable(item)? item : null;
    }

    private boolean isAvailable(MenuItem item) {
        return skillService.getAll().filter(s -> s instanceof UnlockSkill).map(s -> (UnlockSkill) s).noneMatch(s -> s.isObjUnlocked(item) && !skills.contains(s));
    }

    private MenuItem getItemWithDisount(MenuItem item) {
        MenuItemView miv = new MenuItemView(item);
        for (Skill s : skills) {
            if (s instanceof DiscountSkill) {
                miv = ((DiscountSkill) s).applySkill(miv);
            }
        }
        return miv;
    }
}
