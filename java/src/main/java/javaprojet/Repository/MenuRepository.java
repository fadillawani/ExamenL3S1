package javaprojet.Repository;
import javaprojet.Entity.Menu;


import java.util.List;
import java.util.Optional;

public interface MenuRepository {
    int numberOfRows();
    int insert(Menu menu);

    int update(Menu menu);
    Optional<Menu> selectById(int id);

    List<Menu> selectAll();

}