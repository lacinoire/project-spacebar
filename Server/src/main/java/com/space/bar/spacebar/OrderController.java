package com.space.bar.spacebar;

import com.space.bar.spacebar.network.ErrorResponse;
import com.space.bar.spacebar.network.OrderCreationRequest;
import com.space.bar.spacebar.orders.Menu;
import com.space.bar.spacebar.orders.Order;
import com.space.bar.spacebar.users.User;
import com.space.bar.spacebar.users.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.HashSet;
import java.util.Set;
import java.util.function.Predicate;
import java.util.stream.Collectors;

@RestController
@RequestMapping("orders")
public class OrderController {
    private final UserService service;
    private final Set<Order> orders = new HashSet<>();

    @Autowired
    public OrderController(UserService service) {
        this.service = service;
    }

    @PostMapping("new")
    public ResponseEntity<?> createOrder(@RequestBody OrderCreationRequest request) {
        if (service.getUser(request.getUsername()) == null) {
            return ResponseEntity.status(HttpStatus.UNAUTHORIZED).body(new ErrorResponse("User not found."));
        } else if (Menu.getMenu().getMenuItemById(request.getItem()) == null) {
            return ResponseEntity.status(HttpStatus.NOT_FOUND).body(new ErrorResponse("Item not found"));
        } else {
            orders.add(new Order(Menu.getMenu().getMenuItemById(request.getItem()), service.getUser(request.getUsername())));
            return ResponseEntity.ok().build();
        }
    }

    @GetMapping
    public ResponseEntity<?> getOrders(@RequestParam("filter") String filter, @RequestParam("username") String username) {
        if (service.getUser(username) == null) {
            return ResponseEntity.status(HttpStatus.UNAUTHORIZED).body(new ErrorResponse("User not found."));
        }
        User user = service.getUser(username);
        switch (filter) {
            case "own":
                return respondWithFilteredOrders(o -> o.getFromUser().equals(user));
            case "open":
                return respondWithFilteredOrders(o -> o.getStatus() == Order.Status.OPEN && !o.getFromUser().equals(user));
            case "claimed":
                return respondWithFilteredOrders(o -> o.getStatus() == Order.Status.ASSIGNED && o.getAssignee().equals(user));
            default:
                return ResponseEntity.badRequest().body(new ErrorResponse("Filter must be one of 'own', 'open', 'claimed'"));
        }
    }

    private ResponseEntity<Set<Order>> respondWithFilteredOrders(Predicate<Order> filter) {
        return ResponseEntity.ok().body(filterOrders(filter));
    }

    private Set<Order> filterOrders(Predicate<Order> filter) {
        return orders.stream().filter(filter).collect(Collectors.toSet());
    }
}
