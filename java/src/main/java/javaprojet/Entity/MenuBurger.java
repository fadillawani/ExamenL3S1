package javaprojet.Entity;

public class MenuBurger {
    private int id;
    private int menuId;
    private int burgerId;
    private int quantite;

    public MenuBurger() {
    }

    public MenuBurger(int id, int menuId, int burgerId, int quantite) {
        this.id = id;
        this.menuId = menuId;
        this.burgerId = burgerId;
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

    public int getBurgerId() {
        return burgerId;
    }

    public void setBurgerId(int burgerId) {
        this.burgerId = burgerId;
    }

    public int getQuantite() {
        return quantite;
    }

    public void setQuantite(int quantite) {
        this.quantite = quantite;
    }

    @Override
    public String toString() {
        return "MenuBurger{" +
                "id=" + id +
                ", menuId=" + menuId +
                ", burgerId=" + burgerId +
                ", quantite=" + quantite +
                '}';
    }
}
