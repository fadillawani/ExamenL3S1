package javaprojet.Services;
import javaprojet.Entity.Menu;


import java.util.List;
import java.util.Optional;

public interface MenuService {
    void createMenu(Menu menu);


    int numberOfRows();

    void update(Menu menu);

    Optional<Menu> selectById(int id);

    List<Menu> selectAll();

}
