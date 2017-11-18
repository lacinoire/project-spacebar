package com.space.bar.spacebar.skills;

import com.space.bar.spacebar.orders.MenuView.MenuItemView;
import org.apache.el.lang.FunctionMapperImpl;

import java.io.Serializable;
import java.util.function.Function;

public class DiscountSkill extends Skill {
    public interface MappingFunction extends Function<MenuItemView, MenuItemView>, Serializable {}
    private MappingFunction map;

    public DiscountSkill(String name, int xpCost, MappingFunction effect, Integer... nextSkills) {
        super(name, xpCost, nextSkills);
        map = effect;
    }

    public MenuItemView applySkill(MenuItemView item) {
        return map.apply(item);
    }
}
