package com.space.bar.spacebar.skills;

import java.io.Serializable;
import java.util.function.Predicate;

public class UnlockSkill extends Skill {
    public interface SkillPredicate extends Predicate<Object>, Serializable {}
    private SkillPredicate isUnlockedPredicate;

    public UnlockSkill(String name, int xpCost, SkillPredicate isUnlockedPredicate, Integer... nextSkills) {
        super(name, xpCost, nextSkills);
        this.isUnlockedPredicate = isUnlockedPredicate;
    }

    public boolean isObjUnlocked(Object object) {
        return this.isUnlockedPredicate.test(object);
    }
}
