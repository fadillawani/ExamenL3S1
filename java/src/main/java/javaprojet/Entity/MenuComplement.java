package javaprojet.Entity;

public class MenuComplement {
    private int id;
    private int menuId;
    private int complementId;
    private int quantite;

    public MenuComplement() {
    }

    public MenuComplement(int id, int menuId, int complementId, int quantite) {
        this.id = id;
        this.menuId = menuId;
        this.complementId = complementId;
        this.quantite = quantite;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public int getMenuId() {
        return menuId;
    }

    public void setMenuId(int menuId) {
        this.menuId = menuId;
    }

    public int getComplementId() {
        return complementId;
    }

    public void setComplementId(int complementId) {
        this.complementId = complementId;
    }

    public int getQuantite() {
        return quantite;
    }

    public void setQuantite(int quantite) {
        this.quantite = quantite;
    }

    @Override
    public String toString() {
        return "MenuComplement{" +
                "id=" + id +
                ", menuId=" + menuId +
                ", complementId=" + complementId +
                ", quantite=" + quantite +
                '}';
    }
}