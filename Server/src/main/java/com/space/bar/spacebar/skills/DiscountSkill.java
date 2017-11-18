package com.space.bar.spacebar.skills;

import com.space.bar.spacebar.orders.MenuView.MenuItemView;

import java.util.function.Function;

public class DiscountSkill extends Skill {
    private Function<MenuItemView, MenuItemView> map;

    public DiscountSkill(String name, int xpCost, Function<MenuItemView, MenuItemView> effect) {
        super(name, xpCost);
        map = effect;
    }

    public MenuItemView applySkill(MenuItemView item) {
        return map.apply(item);
    }
}
