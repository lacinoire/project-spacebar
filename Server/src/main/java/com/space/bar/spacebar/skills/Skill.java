package com.space.bar.spacebar.skills;

import java.util.Arrays;
import java.util.HashSet;
import java.util.Set;

public abstract class Skill {
    private static int nextId = 1;

    public final int id = nextId++;
    public final String name;
    public final int xpCost;
    public final Set<Integer> nextSkills = new HashSet<>();

    public Skill(String name, int xpCost, Integer... nextSkills) {
        this.name = name;
        this.xpCost = xpCost;
        this.nextSkills.addAll(Arrays.asList(nextSkills));
    }
}
