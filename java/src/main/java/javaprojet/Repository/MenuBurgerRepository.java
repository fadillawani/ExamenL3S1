package javaprojet.Repository;
import javaprojet.Entity.MenuBurger;

import java.util.List;
import java.util.Optional;

public interface MenuBurgerRepository {
    int numberOfRows();
    int insert(MenuBurger menuBurger);
    Optional<MenuBurger> selectById(int id);

    List<MenuBurger> selectAll();
    List<MenuBurger> findByMenuId(int menuId);

}