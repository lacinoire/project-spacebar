package com.space.bar.spacebar;

import com.space.bar.spacebar.users.User;
import com.space.bar.spacebar.users.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import java.util.Comparator;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.concurrent.atomic.AtomicInteger;
import java.util.stream.Collectors;

@RestController
@RequestMapping("leaderboard")
public class LeaderboardController {
    private UserService userService;

    @Autowired
    public LeaderboardController(UserService userService) {
        this.userService = userService;
    }

    @GetMapping
    public ResponseEntity<?> getLeaderboard() {
        List<User> leaderboard = userService.getUsers().stream().sorted(Comparator.comparingInt(u -> -u.getTotalXp())).collect(Collectors.toList());
        return ResponseEntity.ok().body(leaderboard);
    }
}
