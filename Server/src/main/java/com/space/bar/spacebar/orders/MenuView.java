package com.space.bar.spacebar.orders;

import com.space.bar.spacebar.skills.DiscountSkill;
import com.space.bar.spacebar.skills.Skill;

import java.util.Collection;
import java.util.Set;
import java.util.stream.Collectors;

public class MenuView extends Menu {
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

    public MenuView(Collection<Skill> skills, Set<MenuItem> allMenuItems) {
        super(allMenuItems);
        this.skills = skills;
    }

    @Override
    public Collection<MenuItem> getDrinks() {
        return super.getDrinks().stream().map(this::getItemWithSkillsApplied).collect(Collectors.toSet());
    }

    @Override
    public MenuItem getMenuItemById(int id) {
        return getItemWithSkillsApplied(super.getMenuItemById(id));
    }

    private MenuItem getItemWithSkillsApplied(MenuItem item) {
        MenuItemView miv = new MenuItemView(item);
        for (Skill s : skills) {
            if (s instanceof DiscountSkill) {
                miv = ((DiscountSkill) s).applySkill(miv);
            }
        }
        return miv;
    }
}
