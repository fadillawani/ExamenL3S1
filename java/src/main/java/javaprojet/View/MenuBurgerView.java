package javaprojet.View;
import javaprojet.Entity.MenuBurger;
import javaprojet.Services.MenuBurgerService;
import java.util.List;

public class MenuBurgerView {
    private MenuBurgerService service;

    public MenuBurgerView(MenuBurgerService service) {
        this.service = service;
    }

    public void afficheMenuBurgers() {
        List<MenuBurger> liste = service.selectAll();
        if (liste.isEmpty()) {
            System.out.println("Aucun menu-burger.");
        } else {
            liste.forEach(System.out::println);
        }

    }
}
