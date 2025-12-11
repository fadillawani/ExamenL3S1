package javaprojet.View;
import javaprojet.Entity.Burger;
import javaprojet.Services.BurgerCategorieService;
import javaprojet.Services.BurgerService;
import java.util.List;
import java.util.Scanner;

public class BurgerVue  extends Vue{
  private BurgerService burgerService;
    private BurgerCategorieService burgerCategorieService;
    private BurgerCategorieVue burgerCategorieVue;

    public BurgerVue(BurgerService burgerService, BurgerCategorieService burgerCategorieService, BurgerCategorieVue burgerCategorieVue) {

        this.burgerService = burgerService;
        this.burgerCategorieService = burgerCategorieService;
        this.burgerCategorieVue = burgerCategorieVue;
    }

    public Burger saisieBurger(Scanner scanner) {
        Burger b = new Burger();
        b.setId(burgerService.numberOfRows() + 1);

        b.setLibelle(saisieChaine(scanner, "Libellé : "));
        b.setDesc(saisieChaine(scanner, "Description : "));
        b.setPrix(Double.parseDouble(saisieChaine(scanner, "Prix : ")));
        b.setImageUrl(saisieChaine(scanner, "URL Image : "));
        b.setArchived(false);

        do {
            burgerCategorieVue.afficheBurgerCategories();
            b.setBurgerCategorieId(Integer.parseInt(saisieChaine(scanner, "ID Catégorie : ")));
        }while (burgerCategorieService.selectById(b.getBurgerCategorieId()).isEmpty());

        return b;
    }

    public void afficheBurgers() {
        List<Burger> liste = burgerService.selectAll();
        if (liste.isEmpty()) {
            System.out.println("Aucun burger.");
        } else {
            liste.forEach(System.out::println);
        }
    }
}