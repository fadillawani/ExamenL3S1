package javaprojet.View;
import java.util.Scanner;

public class Vue {
    protected static Scanner scanner = new Scanner(System.in);

    public static String saisieChaine(Scanner scanner, String message) {
        String chaine;
        System.out.print(message);
        do {
            chaine = scanner.nextLine();
        } while (chaine.isEmpty());
        return chaine;
    }

    public static int saisieIntPositive(Scanner scanner, String message) {
        int valeur;
        do {
            System.out.print(message);
            while (!scanner.hasNextInt()) {
                System.out.println("Veuillez saisir un nombre entier positif !");
                scanner.nextLine();
                System.out.print(message);
            }
            valeur = scanner.nextInt();
            scanner.nextLine();
        } while (valeur < 0);

        return valeur;
    }

}
