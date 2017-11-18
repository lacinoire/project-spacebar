package com.space.bar.spacebar.skills;

import com.space.bar.spacebar.DataSource;
import com.space.bar.spacebar.users.User;
import org.springframework.stereotype.Service;

import java.util.HashSet;
import java.util.Set;
import java.util.stream.Stream;

@Service
public class SkillService {
    private Set<Skill> basicSkills = new HashSet<>();
    private Set<Skill> allSkills = new HashSet<>();

    public SkillService() {
        for (Skill skill : DataSource.getAllSkills()) {
            if (DataSource.getBasicSkills().contains(skill)) {
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
        return Stream.concat(
                getBasic().filter(skill -> !user.getSkills().contains(skill)),
                user.getSkills().stream().flatMap(this::getNext));
    }

    public Stream<Skill> getPurchased(User user) {
        return user.getSkills().stream();
    }

    public Stream<Skill> getBasic() {
        return basicSkills.stream();
    }

    public Stream<Skill> getNext(Skill skill) {
        return allSkills.stream().filter(s -> skill.nextSkills.contains(s.name));
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
