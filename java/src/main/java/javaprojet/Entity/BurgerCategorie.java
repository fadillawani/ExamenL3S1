package javaprojet.Entity;

public class BurgerCategorie {
    private int id;
    private String nom;

    public BurgerCategorie(int id, String nom) {
        this.id = id;
        this.nom = nom;
    }

    public BurgerCategorie() {
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getNom() {
        return nom;
    }

    public void setNom(String nom) {
        this.nom = nom;
    }

    @Override
    public String toString() {
        return "BurgerCategorie{" +
                "id=" + id +
                ", nom='" + nom + '\'' +
                '}';
    }
}