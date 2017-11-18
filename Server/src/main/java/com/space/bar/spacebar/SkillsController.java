package com.space.bar.spacebar;

import com.space.bar.spacebar.network.BuySkillRequest;
import com.space.bar.spacebar.skills.SkillService;
import com.space.bar.spacebar.users.UserService;
import org.springframework.beans.factory.annotation.Autowired;
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
        switch(filter) {
            case "bought":
                return ResponseEntity.ok().body(skills.getPurchased(users.getUser(username)).collect(Collectors.toList()));
            case "available":
                return ResponseEntity.ok().body(skills.getAvailable(users.getUser(username)).collect(Collectors.toList()));
            default:
                return ResponseEntity.ok().body(skills.getAll().collect(Collectors.toList()));
        }
    }

    @GetMapping("all")
    public ResponseEntity<?> getAllSkills() {
        return ResponseEntity.ok().body(skills.getAll().collect(Collectors.toList()));
    }

    @PostMapping
    public ResponseEntity<?> buySkill(@RequestBody BuySkillRequest request) {
        users.getUser(request.getUsername()).buySkill(skills.getSkill(request.getSkill()));
        return ResponseEntity.ok().build();
    }
}
