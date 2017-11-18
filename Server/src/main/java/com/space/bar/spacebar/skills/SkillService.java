package com.space.bar.spacebar.skills;

import com.space.bar.spacebar.users.User;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.HashSet;
import java.util.Set;
import java.util.stream.Stream;

@Service
public class SkillService {
    private Set<Skill> basicSkills = new HashSet<>();
    private Set<Skill> allSkills = new HashSet<>();

    @Autowired
    public SkillService(SkillsProvider provider) {
        for (Skill skill : provider.getAllSkills()) {
            if (provider.getBasicSkills().contains(skill)) {
                addBaseSkill(skill);
            } else {
                addSkill(skill);
            }
        }
    }

    public Stream<Skill> getAll() {
        return allSkills.stream();
    }

    public Stream<Skill> getAvailable(User user) {
        return Stream.concat(getBasic(), user.getSkills().stream().flatMap(this::getNext)).filter(skill -> !user.getSkills().contains(skill));
    }

    public Stream<Skill> getPurchased(User user) {
        return user.getSkills().stream();
    }

    public Stream<Skill> getBasic() {
        return basicSkills.stream();
    }

    public Stream<Skill> getNext(Skill skill) {
        return allSkills.stream().filter(s -> skill.nextSkills.contains(s.id));
    }

    public Skill getSkill(int id) {
        return getAll().filter(s -> s.id == id).findAny().orElse(null);
    }

    public void addBaseSkill(Skill skill) {
        basicSkills.add(skill);
        allSkills.add(skill);
    }

    public void addSkill(Skill skill) {
        allSkills.add(skill);
    }
}
