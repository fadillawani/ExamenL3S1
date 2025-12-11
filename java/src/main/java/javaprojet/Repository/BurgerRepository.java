package javaprojet.Repository;
import javaprojet.Entity.Burger;

import java.util.List;
import java.util.Optional;

public interface BurgerRepository {
    int numberOfRows();
    int insert(Burger burger);
    Optional<Burger> selectById(int id);

    List<Burger> selectAll();


}