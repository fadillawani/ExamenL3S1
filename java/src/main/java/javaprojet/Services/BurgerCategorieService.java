package javaprojet.Services;
import javaprojet.Entity.BurgerCategorie;
import java.util.List;
import java.util.Optional;

public interface BurgerCategorieService {
    void createBurgerCategorie(BurgerCategorie burgerCategorie);

    Optional<BurgerCategorie> selectById(int id);

    List<BurgerCategorie> selectAll();

    int numberOfRows();
}