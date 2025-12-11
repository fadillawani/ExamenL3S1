package javaprojet.View;
import javaprojet.Entity.Complement;
import javaprojet.Entity.Enum.TypeComplement;
import javaprojet.Services.ComplementService;
import java.util.List;
import java.util.Scanner;

public class ComplementVue extends Vue {
    private ComplementService service;

    public ComplementVue(ComplementService service) {
        this.service = service;
    }

    public Complement saisieComplement(Scanner scanner) {
        Complement c = new Complement();
        c.setId(service.numberOfRows() + 1);

        c.setLibelle(saisieChaine(scanner, "Libellé : "));
        c.setPrix(Double.parseDouble(saisieChaine(scanner, "Prix : ")));
        c.setImageUrl(saisieChaine(scanner, "URL image : "));
        c.setArchived(false);

        System.out.println("Type de complément (BOISSON / FRITE)");
        c.setTypeComplement(TypeComplement.valueOf(
                saisieChaine(scanner, "Type : ").toUpperCase()
        ));

        return c;
    }

    public void afficheComplements() {
        List<Complement> liste = service.selectAll();
        if (liste.isEmpty()) {
            System.out.println("Aucun complément.");
        } else {
            liste.forEach(System.out::println);
        }
    }
}
