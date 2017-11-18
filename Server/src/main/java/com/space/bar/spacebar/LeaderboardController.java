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
import java.util.Map;
import java.util.concurrent.atomic.AtomicInteger;

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
        Map<Integer, User> leaderboard = new HashMap<>();
        AtomicInteger idx = new AtomicInteger(1);
        userService.getUsers().stream().sorted(Comparator.comparingInt(User::getTotalXp)).forEach(u -> leaderboard.put(idx.incrementAndGet(), u));
        return ResponseEntity.ok().body(leaderboard);
    }
}
