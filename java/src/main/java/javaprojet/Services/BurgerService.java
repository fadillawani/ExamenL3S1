package javaprojet.Services;
import javaprojet.Entity.Burger;

import java.util.List;
import java.util.Optional;

public interface BurgerService {
    void createBurger(Burger burger);

    int numberOfRows();
    Optional<Burger> selectById(int id);

    List<Burger> selectAll();
}