package javaprojet.View;

import java.util.Scanner;

import javaprojet.Entity.Burger;
import javaprojet.Entity.BurgerCategorie;
import javaprojet.Services.BurgerCategorieService;
import javaprojet.Services.BurgerService;


public class MenuPrincipal {

    private final BurgerCategorieVue burgerCategorieVue;
    private final BurgerCategorieService burgerCategorieService;
    private final BurgerVue burgerVue;
    private final BurgerService burgerService;

    public MenuPrincipal(
            BurgerCategorieVue burgerCategorieVue,
            BurgerCategorieService burgerCategorieService,
            BurgerVue burgerVue,
            BurgerService burgerService
    ) {
        this.burgerCategorieVue = burgerCategorieVue;
        this.burgerCategorieService = burgerCategorieService;
        this.burgerVue = burgerVue;
        this.burgerService = burgerService;
    }

    public void affichermenuprincipal(Scanner scanner) {
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
                    Burger burger = burgerVue.saisieBurger(scanner);
                    burgerService.createBurger(burger);
                    System.out.println("Burger ajouté !");
                }
                case 2 -> {
                    BurgerCategorie cat = burgerCategorieVue.saisieBurgerCategorie(scanner);
                    burgerCategorieService.createBurgerCategorie(cat);
                    System.out.println("BurgerCategorie ajoutée !");
                }
               
                case 3 -> {
                    burgerVue.Archivedburger(scanner);
                    
                }
                case 4 -> {
                    burgerVue.afficheBurgers();
                    
                }

                case 5 -> {
                    System.out.println("Retour au menu principal.");
                }
                default -> System.out.println("Choix invalide !");
            }

        } while (choix != 5);
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
