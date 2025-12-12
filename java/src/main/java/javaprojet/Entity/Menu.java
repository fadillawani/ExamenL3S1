package javaprojet.Entity;

public class Menu {
    private int id;
    private String libelle;
    private String imageUrl;
    private Boolean isArchived;
    private Double prix;

    public Menu() {
    }

    public Menu(int id, String libelle, String imageUrl, Boolean isArchived, Double prix) {
        this.id = id;
        this.libelle = libelle;
        this.imageUrl = imageUrl;
        this.isArchived = isArchived;
        this.prix = prix;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getLibelle() {
        return libelle;
    }

    public void setLibelle(String libelle) {
        this.libelle = libelle;
    }

    public String getImageUrl() {
        return imageUrl;
    }

    public void setImageUrl(String imageUrl) {
        this.imageUrl = imageUrl;
    }

    public Boolean getArchived() {
        return isArchived;
    }

    public void setArchived(Boolean archived) {
        isArchived = archived;
    }

    public Double getPrix() {
        return prix;
    }

    public void setPrix(Double prix) {
        this.prix = prix;
    }

    @Override
    public String toString() {
        return "Menu{" +
                "id=" + id +
                ", libelle='" + libelle + '\'' +
                ", imageUrl='" + imageUrl + '\'' +
                ", isArchived=" + isArchived +
                ", prix=" + prix +
                '}';
    }
}