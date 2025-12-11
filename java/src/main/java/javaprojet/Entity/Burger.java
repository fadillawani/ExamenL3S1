package javaprojet.Entity;

public class Burger {
    private int id;
    private String libelle;
    private String desc;
    private Double prix;
    private String imageUrl;
    private Boolean isArchived;
    private int burgerCategorieId;

    public Burger() {
    }

    public Burger(int id, String libelle, String desc, Double prix, String imageUrl, Boolean isArchived, int burgerCategorieId) {
        this.id = id;
        this.libelle = libelle;
        this.desc = desc;
        this.prix = prix;
        this.imageUrl = imageUrl;
        this.isArchived = isArchived;
        this.burgerCategorieId = burgerCategorieId;
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

    public String getDesc() {
        return desc;
    }

    public void setDesc(String desc) {
        this.desc = desc;
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

    public int getBurgerCategorieId() {
        return burgerCategorieId;
    }

    public void setBurgerCategorieId(int burgerCategorieId) {
        this.burgerCategorieId = burgerCategorieId;
    }

    @Override
    public String toString() {
        return "Burger{" +
                "id=" + id +
                ", libelle='" + libelle + '\'' +
                ", desc='" + desc + '\'' +
                ", prix=" + prix +
                ", imageUrl='" + imageUrl + '\'' +
                ", isArchived=" + isArchived +
                ", burgerCategorieId=" + burgerCategorieId +
                '}';
    }
}
