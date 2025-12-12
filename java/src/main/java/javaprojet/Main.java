package javaprojet;

import java.util.Scanner;
import javaprojet.Repository.*;
import javaprojet.Repository.IMPL.*;
import javaprojet.Services.*;
import javaprojet.Services.impl.*;
import javaprojet.View.*;
import javaprojet.Config.Database.Database;
import javaprojet.Config.Database.DatabaseImpl;


public class Main {
    public static void main(String[] args) {
            Database database = DatabaseImpl.getInstance(
    "org.postgresql.Driver",
    "jdbc:postgresql://ep-steep-leaf-ah7j93si-pooler.c-3.us-east-1.aws.neon.tech:5432/neondb?sslmode=require&ssl=true&sslfactory=org.postgresql.ssl.NonValidatingFactory",
    "neondb_owner",
    "npg_Vg5ADhlt3yzL"
   );
    

        Scanner scanner = new Scanner(System.in);
        BurgerCategorieRepository burgerCategorieRepository = new BurgerCategorieRepositoryImpl(database);
        BurgerRepository burgerRepository = new BurgerRepositoryImpl(database);
        ComplementRepository complementRepository = new ComplementRepositoryImpl(database);
        MenuRepository menuRepository = new MenuRepositoryImpl(database);
        MenuBurgerRepository menuBurgerRepository = new MenuBurgerRepositoryImpl(database);
        MenuComplementRepository menuComplementRepository = new MenuComplementRepositoryImpl(database);
        SaveImage saveImage = new SaveImage();



        BurgerCategorieService burgerCategorieService = new BurgerCategorieServiceImpl(burgerCategorieRepository);
        BurgerService burgerService = new BurgerServiceImpl(burgerRepository);
        ComplementService complementService = new ComplementServiceImpl(complementRepository);
        MenuService menuService = new MenuServiceImpl(menuRepository);
        MenuBurgerService menuBurgerService = new MenuBurgerServiceImpl(menuBurgerRepository);
        MenuComplementService menuComplementService = new MenuComplementServiceImpl(menuComplementRepository);

        BurgerCategorieVue burgerCategorieVue = new BurgerCategorieVue(burgerCategorieService);
        BurgerVue burgerVue = new BurgerVue(burgerService, burgerCategorieService, burgerCategorieVue, saveImage);
        ComplementVue complementVue = new ComplementVue(complementService, saveImage);
        MenuView menuVue = new MenuView(menuService, burgerService, complementService, menuBurgerService, menuComplementService, burgerVue, complementVue, saveImage);


 MenuPrincipal menuPrincipal = new MenuPrincipal(burgerCategorieVue, burgerCategorieService, burgerVue, burgerService, complementVue, complementService, menuVue, menuService, menuBurgerService, menuComplementService);

        menuPrincipal.affichermenuprincipal(scanner);
    }    
}