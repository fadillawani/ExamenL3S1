package javaprojet.Entity;
import javaprojet.Entity.Enum.TypeComplement;

public class Complement {
    private int id;
    private String libelle;
    private Double prix;
    private String imageUrl;
    private Boolean isArchived;
    private TypeComplement typeComplement;

    public Complement() {
    }

    public Complement(int id, String libelle, Double prix, String imageUrl, Boolean isArchived, TypeComplement typeComplement) {
        this.id = id;
        this.libelle = libelle;
        this.prix = prix;
        this.imageUrl = imageUrl;
        this.isArchived = isArchived;
        this.typeComplement = typeComplement;
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

    public Double getPrix() {
        return prix;
    }

    public void setPrix(Double prix) {
        this.prix = prix;
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

    public TypeComplement getTypeComplement() {
        return typeComplement;
    }

    public void setTypeComplement(TypeComplement typeComplement) {
        this.typeComplement = typeComplement;
    }

    @Override
    public String toString() {
        return "Complement{" +
                "id=" + id +
                ", libelle='" + libelle + '\'' +
                ", prix=" + prix +
                ", imageUrl='" + imageUrl + '\'' +
                ", isArchived=" + isArchived +
                ", typeComplement=" + typeComplement +
                '}';
    }
}