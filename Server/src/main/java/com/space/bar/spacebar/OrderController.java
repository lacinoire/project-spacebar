package com.space.bar.spacebar;

import com.space.bar.spacebar.network.ErrorResponse;
import com.space.bar.spacebar.network.ChangeOrderRequest;
import com.space.bar.spacebar.network.OrderCreationRequest;
import com.space.bar.spacebar.orders.*;
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
    private final UserService users;
    private final Map<Integer,Order> orders = new HashMap<>();
    private final Menu menu;
    private final ItemProvider itemProvider;

    @Autowired
    public OrderController(UserService users, Menu menu, ItemProvider itemProvider) {
        this.users = users;
        this.menu = menu;
        this.itemProvider = itemProvider;
    }

    @PostMapping("new")
    public ResponseEntity<?> createOrder(@RequestBody OrderCreationRequest request) {
        if (users.getUser(request.getUsername()) == null) {
            return ResponseEntity.status(HttpStatus.UNAUTHORIZED).body(new ErrorResponse("User not found."));
        } else if (menu.getMenuItemById(request.getItem()) == null) {
            return ResponseEntity.status(HttpStatus.NOT_FOUND).body(new ErrorResponse("Item not found"));
        } else {
            MenuItem item = new MenuView(users.getUser(request.getUsername()).getSkills(), itemProvider.getAllItems()).getMenuItemById(request.getItem());
            Order order = new Order(item, request.getUsername());
            orders.put(order.getId(), order);
            return ResponseEntity.ok().build();
        }
    }

    @PostMapping("claim")
    public ResponseEntity<?> claimOrder(@RequestBody ChangeOrderRequest request) {
        if (users.getUser(request.getUsername()) == null) {
            return ResponseEntity.status(HttpStatus.UNAUTHORIZED).body(new ErrorResponse("User not found."));
        } else if (!orders.containsKey(request.getOrder())) {
            return ResponseEntity.status(HttpStatus.NOT_FOUND).body(new ErrorResponse("Order not found"));
        } else if (orders.get(request.getOrder()).getFromUser().equals(request.getUsername())) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body(new ErrorResponse("You cannot claim your own order"));
        } else if (orders.get(request.getOrder()).getStatus().isClaimed()) {
            return ResponseEntity.status(HttpStatus.CONFLICT).body(new ErrorResponse("Order is not open"));
        } else {
            orders.get(request.getOrder()).setAssignee(request.getUsername());
            return ResponseEntity.ok().build();
        }
    }

    @PostMapping("finish")
    public ResponseEntity<?> finishOrder(@RequestBody ChangeOrderRequest request) {
        if (users.getUser(request.getUsername()) == null) {
            return ResponseEntity.status(HttpStatus.UNAUTHORIZED).body(new ErrorResponse("User not found."));
        } else if (!orders.containsKey(request.getOrder())) {
            return ResponseEntity.status(HttpStatus.NOT_FOUND).body(new ErrorResponse("Order not found"));
        } else if (!orders.get(request.getOrder()).getStatus().isClaimed() || orders.get(request.getOrder()).getStatus().isFinished()) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body(new ErrorResponse("Order is not claimed or already finished."));
        } else if (!request.getUsername().equals(orders.get(request.getOrder()).getAssignee())) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body(new ErrorResponse("You cannot finish another users's orders."));
        } else {
            orders.get(request.getOrder()).finishOrder();
            fulfilOrder(orders.get(request.getOrder()));
            return ResponseEntity.ok().build();
        }
    }

    @PostMapping("approve")
    public ResponseEntity<?> approveOrder(@RequestBody ChangeOrderRequest request) {
        if (users.getUser(request.getUsername()) == null) {
            return ResponseEntity.status(HttpStatus.UNAUTHORIZED).body(new ErrorResponse("User not found."));
        } else if (!orders.containsKey(request.getOrder())) {
            return ResponseEntity.status(HttpStatus.NOT_FOUND).body(new ErrorResponse("Order not found"));
        } else if (!orders.get(request.getOrder()).getStatus().isClaimed() || orders.get(request.getOrder()).getStatus().isApproved()) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body(new ErrorResponse("Order is not claimed or already approved."));
        } else if (!request.getUsername().equals(orders.get(request.getOrder()).getFromUser())) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body(new ErrorResponse("You cannot approve another users's orders."));
        } else {
            orders.get(request.getOrder()).approveOrder();
            fulfilOrder(orders.get(request.getOrder()));
            return ResponseEntity.ok().build();
        }
    }

    @GetMapping
    public ResponseEntity<?> getOrders(@RequestParam("filter") String filter, @RequestParam("username") String username) {
        if (users.getUser(username) == null) {
            return ResponseEntity.status(HttpStatus.UNAUTHORIZED).body(new ErrorResponse("User not found."));
        }
        switch (filter) {
            case "own":
                return respondWithFilteredOrders(o -> o.getFromUser().equals(username));
            case "open":
                return respondWithFilteredOrders(o -> !o.getStatus().isClaimed() && !o.getFromUser().equals(username));
            case "claimed":
                return respondWithFilteredOrders(o -> o.getStatus().isClaimed() && o.getAssignee().equals(username));
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

    private void fulfilOrder(Order order) {
        if (!order.getStatus().isApproved() || !order.getStatus().isFinished()) return;
        users.getUser(order.getFromUser()).addToXp(order.getItem().getXpGain());
        users.getUser(order.getAssignee()).addToXp(150);
        orders.remove(order.getId());
    }
}
