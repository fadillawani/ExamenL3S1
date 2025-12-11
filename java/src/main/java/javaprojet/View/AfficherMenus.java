package javaprojet.View;

public class AfficherMenus {


     public static void afficherMenuPrincipal() {
        System.out.println("\n******************************");
        System.out.println("         MENU PRINCIPAL      ");
        System.out.println("******************************");
        System.out.println("1 - Gestion des Burgers");
        System.out.println("2 - Gestion des Menus");
        System.out.println("3 - Gestion des Compléments");
        System.out.println("4 - Quitter");
        System.out.println("******************************");
        System.out.print("Votre choix : ");
    }

      public static void afficherMenuBurger() {
        System.out.println("\n--- Gestion des Burgers ---");
        System.out.println("1 - Ajouter un Burger");
        System.out.println("2 - Ajouter un Catégorie");
        System.out.println("3 - Archiver un Burger");
        System.out.println("4 - Liste des Burgers");
        System.out.println("5 - Retour");
        System.out.print("Votre choix : ");
    }
}
