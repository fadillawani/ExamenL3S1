package javaprojet.View;
import javaprojet.Entity.MenuComplement;
import javaprojet.Services.MenuComplementService;
import java.util.List;

public class MenuComplementView {
 private MenuComplementService service;

    public MenuComplementView(MenuComplementService service) {
        this.service = service;
    }

    public void afficheMenuComplements() {
        List<MenuComplement> liste = service.selectAll();
        if (liste.isEmpty()) {
            System.out.println("Aucun menu-complement.");
        } else {
            liste.forEach(System.out::println);
        }
    }
}