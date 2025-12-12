package javaprojet.Services;
import javaprojet.Entity.MenuBurger;


import java.util.List;
import java.util.Optional;

public interface MenuBurgerService {
    void createMenuBurger(MenuBurger menuBurger);

    Optional<MenuBurger> selectById(int id);

    List<MenuBurger> selectAll();

    int numberOfRows();

    List<MenuBurger> findByMenuId(int menuId);

}
