package javaprojet.View;

import javaprojet.Entity.Menu;
import javaprojet.Entity.MenuBurger;
import javaprojet.Entity.MenuComplement;
import javaprojet.Services.BurgerService;
import javaprojet.Services.ComplementService;
import javaprojet.Services.MenuBurgerService;
import javaprojet.Services.MenuComplementService;
import javaprojet.Services.MenuService;
import javaprojet.Services.SaveImage;

import java.util.List;
import java.util.Scanner;

public class MenuView extends Vue {

    private final MenuService menuService;
    private final BurgerService burgerService;
    private final ComplementService complementService;
    private final MenuBurgerService menuBurgerService;
    private final MenuComplementService menuComplementService;
    private final SaveImage saveImage;

    private final BurgerVue burgerVue;
    private final ComplementVue complementVue;

    public MenuView(MenuService menuService,
                    BurgerService burgerService,
                    ComplementService complementService,
                    MenuBurgerService menuBurgerService,
                    MenuComplementService menuComplementService,
                    BurgerVue burgerVue,
                    ComplementVue complementVue,
                    SaveImage saveImage) {

        this.menuService = menuService;
        this.burgerService = burgerService;
        this.complementService = complementService;
        this.menuBurgerService = menuBurgerService;
        this.menuComplementService = menuComplementService;
        this.burgerVue = burgerVue;
        this.complementVue = complementVue;
        this.saveImage = saveImage;
    }

    public Menu saisieMenu(Scanner scanner) {
        Menu menu = new Menu();
        menu.setId(menuService.numberOfRows() + 1);
        menu.setLibelle(saisieChaine(scanner, "Saisir le libellé : "));

        String imageUrl = saveImage.uploadImage();
        if (imageUrl != null) {
            System.out.println("Image disponible à : " + imageUrl);
        }
        menu.setImageUrl(imageUrl);

        menu.setArchived(false);
        menu.setPrix(0.0);

        return menu;
    }

    public Menu saisieMenuComplet(Scanner scanner) {

        Menu menu = saisieMenu(scanner);
        menuService.createMenu(menu);

        double total = 0;

        // Ajout des burgers
        System.out.println("\n=== AJOUT DES BURGERS AU MENU ===");
        while (true) {
            String rep = saisieChaine(scanner, "Ajouter un burger ? (o/n) : ");
            if (rep.equalsIgnoreCase("n")) break;

            burgerVue.afficheBurgers();

            int burgerId;
            do {
                burgerId = Integer.parseInt(saisieChaine(scanner, "ID du burger : "));
            } while (burgerService.selectById(burgerId).isEmpty());

            int quantite = Vue.saisieIntPositive(scanner, "Quantité: ");

            MenuBurger mb = new MenuBurger(
                    menuBurgerService.numberOfRows() + 1,
                    menu.getId(),
                    burgerId,
                    quantite
            );
            menuBurgerService.createMenuBurger(mb);

            double prixBurger = burgerService.selectById(burgerId).get().getPrix();
            total += prixBurger * quantite;
        }

        // Ajout des compléments
        System.out.println("\n=== AJOUT DES COMPLÉMENTS AU MENU ===");
        while (true) {
            String rep = saisieChaine(scanner, "Ajouter un complément ? (o/n) : ");
            if (rep.equalsIgnoreCase("n")) break;

            complementVue.afficheComplements();

            int complementId;
            do {
                complementId = Integer.parseInt(saisieChaine(scanner, "ID du complément : "));
            } while (complementService.selectById(complementId).isEmpty());

            int quantite = Vue.saisieIntPositive(scanner, "Quantité: ");

            MenuComplement mc = new MenuComplement(
                    menuComplementService.numberOfRows() + 1,
                    menu.getId(),
                    complementId,
                    quantite
            );
            menuComplementService.createMenuComplement(mc);

            double prixComplement = complementService.selectById(complementId).get().getPrix();
            total += prixComplement * quantite;
        }

        menu.setPrix(total);
        menuService.update(menu);

        System.out.println("\n=== MENU CRÉÉ ===");
        System.out.println("Libellé : " + menu.getLibelle());
        System.out.println("Prix final : " + total + " FCFA");

        return menu;
    }

    public void afficheMenus() {
        List<Menu> menus = menuService.selectAll();

        if (menus.isEmpty()) {
            System.out.println("Aucun menu disponible.");
            return;
        }

        for (Menu menu : menus) {
            System.out.println(menu);

            System.out.println("Burgers :");
            List<MenuBurger> menuBurgers = menuBurgerService.findByMenuId(menu.getId());
            if (menuBurgers.isEmpty()) {
                System.out.println("   Aucun burger.");
            } else {
                for (MenuBurger mb : menuBurgers) {
                    System.out.println(burgerService.selectById(mb.getBurgerId()).get());
                }
            }

            System.out.println("Compléments :");
            List<MenuComplement> menuComplements = menuComplementService.findByMenuId(menu.getId());
            if (menuComplements.isEmpty()) {
                System.out.println("   Aucun complément.");
            } else {
                for (MenuComplement mc : menuComplements) {
                    System.out.println(complementService.selectById(mc.getComplementId()).get());
                }
            }

            System.out.println("==============================\n");
        }
    }
}
