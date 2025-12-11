package javaprojet.View;

import java.util.Scanner;

import javaprojet.Entity.BurgerCategorie;
import javaprojet.Services.BurgerCategorieService;

public class MenuPrincipal {

    private final BurgerCategorieVue burgerCategorieVue;
    private final BurgerCategorieService burgerCategorieService;

    public MenuPrincipal(
            BurgerCategorieVue burgerCategorieVue,
            BurgerCategorieService burgerCategorieService
    ) {
        this.burgerCategorieVue = burgerCategorieVue;
        this.burgerCategorieService = burgerCategorieService;
    }

    public void afficher(Scanner scanner) {
        int choix;

        do {
            AfficherMenus.afficherMenuPrincipal();
            choix = lireEntier(scanner);

            switch (choix) {
                case 1:
                    afficherMenuBurger(scanner);
                    break;
                case 4:
                    System.out.println("Au revoir !");
                    break;
                default:
                    System.out.println("Choix invalide, réessayez.");
                    break;
            }

        } while (choix != 4);
    }

    private void afficherMenuBurger(Scanner scanner) {
        int choix;

        do {
            AfficherMenus.afficherMenuBurger();
            choix = lireEntier(scanner);

            switch (choix) {
                case 1 -> {
                    
                    System.out.println("Burger ajouté !");
                }
                case 2 -> {
                    BurgerCategorie cat = burgerCategorieVue.saisieBurgerCategorie(scanner);
                    burgerCategorieService.createBurgerCategorie(cat);
                    System.out.println("BurgerCategorie ajoutée !");
                }
                case 5 -> {
                  System.out.println("Burger ajouté !");
                }
                case 3 -> {
                    // retour menu précédent
                }
                default -> System.out.println("Choix invalide !");
            }

        } while (choix != 3);
    }

    private int lireEntier(Scanner scanner) {
        String line = scanner.nextLine();
        try {
            return Integer.parseInt(line.trim());
        } catch (NumberFormatException e) {
            return -1;
        }
    }
}
