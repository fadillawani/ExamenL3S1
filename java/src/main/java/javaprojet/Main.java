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
                "jdbc:postgresql://localhost:5432/brazilburger",
                "postgres",
                "fadil2006"
        );
        Scanner scanner = new Scanner(System.in);
        BurgerCategorieRepository burgerCategorieRepository = new BurgerCategorieRepositoryImpl(database);
        BurgerRepository burgerRepository = new BurgerRepositoryImpl(database);
        ComplementRepository complementRepository = new ComplementRepositoryImpl(database);


        BurgerCategorieService burgerCategorieService = new BurgerCategorieServiceImpl(burgerCategorieRepository);
        BurgerService burgerService = new BurgerServiceImpl(burgerRepository);
        ComplementService complementService = new ComplementServiceImpl(complementRepository);

        BurgerCategorieVue burgerCategorieVue = new BurgerCategorieVue(burgerCategorieService);
        BurgerVue burgerVue = new BurgerVue(burgerService, burgerCategorieService, burgerCategorieVue);
        ComplementVue complementVue = new ComplementVue(complementService);


 MenuPrincipal menuPrincipal = new MenuPrincipal(burgerCategorieVue, burgerCategorieService, burgerVue, burgerService, complementVue, complementService);

        menuPrincipal.affichermenuprincipal(scanner);
    }    
}