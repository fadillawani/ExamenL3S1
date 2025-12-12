package javaprojet.View;
import javaprojet.Entity.Complement;
import javaprojet.Entity.Enum.TypeComplement;
import javaprojet.Services.ComplementService;
import javaprojet.Services.SaveImage;

import java.util.List;
import java.util.Scanner;

public class ComplementVue extends Vue {
    private ComplementService service;
     private SaveImage saveImage ;

    public ComplementVue(ComplementService service, SaveImage saveImage) {
        this.service = service;
        this.saveImage = saveImage;
    }

    public Complement saisieComplement(Scanner scanner) {
        Complement c = new Complement();
        c.setId(service.numberOfRows() + 1);

        c.setLibelle(saisieChaine(scanner, "Libellé : "));
        c.setPrix(Double.parseDouble(saisieChaine(scanner, "Prix : ")));
       String imageUrl = saveImage.uploadImage();

        if (imageUrl != null) {
            System.out.println("Image disponible à : " + imageUrl);
        }
        c.setImageUrl(imageUrl);
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
    public void Archivedcomplement(Scanner scanner) {
        afficheComplements();
        int id = Integer.parseInt(saisieChaine(scanner, "Entrez l'ID du complément à archiver : "));
        var complementOpt = service.selectById(id);
        if (complementOpt.isPresent()) {
            Complement complement = complementOpt.get();
            complement.setArchived(true);
            System.out.println("Complément archivé : " + complement);
        } else {
            System.out.println("Complément avec l'ID " + id + " non trouvé.");
        }
    }
}
