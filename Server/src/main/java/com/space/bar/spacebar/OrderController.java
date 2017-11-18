package com.space.bar.spacebar;

import com.space.bar.spacebar.network.ErrorResponse;
import com.space.bar.spacebar.network.ChangeOrderRequest;
import com.space.bar.spacebar.network.OrderCreationRequest;
import com.space.bar.spacebar.orders.Menu;
import com.space.bar.spacebar.orders.Order;
import com.space.bar.spacebar.users.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.HashMap;
import java.util.Map;
import java.util.Set;
import java.util.function.Predicate;
import java.util.stream.Collectors;

@RestController
@RequestMapping("orders")
public class OrderController {
    private final UserService service;
    private final Map<Integer,Order> orders = new HashMap<>();

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
            Order order = new Order(Menu.getMenu().getMenuItemById(request.getItem()), request.getUsername());
            orders.put(order.getId(), order);
            return ResponseEntity.ok().build();
        }
    }

    @PostMapping("claim")
    public ResponseEntity<?> claimOrder(@RequestBody ChangeOrderRequest orderRequest) {
        if (service.getUser(orderRequest.getUsername()) == null) {
            return ResponseEntity.status(HttpStatus.UNAUTHORIZED).body(new ErrorResponse("User not found."));
        } else if (!orders.containsKey(orderRequest.getOrder())) {
            return ResponseEntity.status(HttpStatus.NOT_FOUND).body(new ErrorResponse("Order not found"));
        } else if (orders.get(orderRequest.getOrder()).getFromUser().equals(orderRequest.getUsername())) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body(new ErrorResponse("You cannot claim your own order"));
        } else if (orders.get(orderRequest.getOrder()).getStatus() != Order.Status.OPEN) {
            return ResponseEntity.status(HttpStatus.CONFLICT).body(new ErrorResponse("Order is not open"));
        } else {
            orders.get(orderRequest.getOrder()).setAssignee(orderRequest.getUsername());
            return ResponseEntity.ok().build();
        }
    }

    @GetMapping
    public ResponseEntity<?> getOrders(@RequestParam("filter") String filter, @RequestParam("username") String username) {
        if (service.getUser(username) == null) {
            return ResponseEntity.status(HttpStatus.UNAUTHORIZED).body(new ErrorResponse("User not found."));
        }
        switch (filter) {
            case "own":
                return respondWithFilteredOrders(o -> o.getFromUser().equals(username));
            case "open":
                return respondWithFilteredOrders(o -> o.getStatus() == Order.Status.OPEN && !o.getFromUser().equals(username));
            case "claimed":
                return respondWithFilteredOrders(o -> o.getStatus() == Order.Status.ASSIGNED && o.getAssignee().equals(username));
            default:
                return ResponseEntity.badRequest().body(new ErrorResponse("Filter must be one of 'own', 'open', 'claimed'"));
        }
    }

    private ResponseEntity<Set<Order>> respondWithFilteredOrders(Predicate<Order> filter) {
        return ResponseEntity.ok().body(filterOrders(filter));
    }

    private Set<Order> filterOrders(Predicate<Order> filter) {
        return orders.values().stream().filter(filter).collect(Collectors.toSet());
    }
}
