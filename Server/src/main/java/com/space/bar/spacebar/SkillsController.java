package com.space.bar.spacebar;

import com.space.bar.spacebar.network.BuySkillRequest;
import com.space.bar.spacebar.network.ErrorResponse;
import com.space.bar.spacebar.skills.Skill;
import com.space.bar.spacebar.skills.SkillService;
import com.space.bar.spacebar.users.User;
import com.space.bar.spacebar.users.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.stream.Collectors;

@RestController
@RequestMapping("skills")
public class SkillsController {
    private SkillService skills;
    private UserService users;

    @Autowired
    public SkillsController(SkillService skills, UserService users) {
        this.skills = skills;
        this.users = users;
    }

    @GetMapping
    public ResponseEntity<?> getSkills(@RequestParam("username") String username, @RequestParam(name = "filter", defaultValue = "all") String filter) {
        if (users.getUser(username) == null) {
            return ResponseEntity.status(HttpStatus.UNAUTHORIZED).body(new ErrorResponse("User not found"));
        }
        switch(filter.toLowerCase()) {
            case "bought":
                return ResponseEntity.ok().body(skills.getPurchased(users.getUser(username)).collect(Collectors.toList()));
            case "available":
                return ResponseEntity.ok().body(skills.getAvailable(users.getUser(username)).collect(Collectors.toList()));
            case "basic":
                return ResponseEntity.ok().body(skills.getBasic().collect(Collectors.toList()));
            case "all":
                return ResponseEntity.ok().body(skills.getAll().collect(Collectors.toList()));
            default:
                return ResponseEntity.status(HttpStatus.BAD_REQUEST).body(new ErrorResponse("Unknown filter parameter."));
        }
    }

    @PostMapping
    public ResponseEntity<?> buySkill(@RequestBody BuySkillRequest request) {
        User user = users.getUser(request.getUsername());
        Skill skill = skills.getSkill(request.getSkill());
        if (user == null) {
            return ResponseEntity.status(HttpStatus.UNAUTHORIZED).body(new ErrorResponse("User not found"));
        } else if (skill == null) {
            return ResponseEntity.status(HttpStatus.NOT_FOUND).body(new ErrorResponse("Skill not found"));
        } else if (skills.getAvailable(user).noneMatch(skill::equals)) {
            return ResponseEntity.status(HttpStatus.NOT_FOUND).body(new ErrorResponse("Skill is not available"));
        } else if (skill.xpCost > user.getUsableXp()) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body(new ErrorResponse("You cannot afford that skill just yet."));
        }
        user.buySkill(skill);
        return ResponseEntity.ok().build();
    }
}
