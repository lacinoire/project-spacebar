package com.space.bar.spacebar;

import com.space.bar.spacebar.orders.ItemProvider;
import com.space.bar.spacebar.orders.MenuItem;
import com.space.bar.spacebar.skills.Skill;
import com.space.bar.spacebar.skills.SkillsProvider;
import com.space.bar.spacebar.users.User;
import com.space.bar.spacebar.users.UserService;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

import java.io.*;
import java.util.HashMap;
import java.util.HashSet;

@Configuration
public class StorageWriter {
    public static <T> T loadFromFile(String file) {
        T o = null;
        try (FileInputStream fileIn = new FileInputStream(getUserDataDirectory() + file);
             ObjectInputStream in = new ObjectInputStream(fileIn)){
            o = (T) in.readObject();
        } catch (IOException | ClassNotFoundException i) {
            i.printStackTrace();
        }
        System.out.println("Deserialized " + file);
        return o;
    }

    public static boolean exists(String file) {
        return new File(getUserDataDirectory() + file).exists()
                && new File(getUserDataDirectory() + file).isFile();
    }

    public static void saveToFile(Serializable obj, String file) {
        createFile(getUserDataDirectory() + file);
        try (
             FileOutputStream fileOut = new FileOutputStream(getUserDataDirectory() + file);
             ObjectOutputStream out = new ObjectOutputStream(fileOut)) {
            out.writeObject(obj);
            out.close();
            System.out.printf("Serialized to " + file);
        } catch (IOException i) {
            i.printStackTrace();
        }
    }

    private static String getUserDataDirectory() {
        return System.getProperty("user.home") + File.separator + ".project-space.bar" + File.separator;
    }

    private static void createFile(String file) {
        try {
            File f = new File(file);
            f.getParentFile().mkdirs();
            f.createNewFile();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    @Bean
    public static ItemProvider getItemProvider() {
        if (exists("items.ser")) {
            HashSet<MenuItem> items = loadFromFile("items.ser");
            if (items != null) {
                return new ItemProvider(items);
            }
        }
        return new ItemProvider();
    }

    @Bean
    public static SkillsProvider getProvder() {
        if (exists("skills.ser")) {
            HashMap<Skill, Boolean> map = loadFromFile("skills.ser");
            if (map != null) return new SkillsProvider();
        }
        return new SkillsProvider();
    }

    @Bean
    public static UserService getUserService() {
        if (exists("users.ser")) {
            HashMap<String, User> users = loadFromFile("users.ser");
            if (users != null) {
                return new UserService(users);
            }
        }
        return new UserService(new HashMap<>());
    }
}
