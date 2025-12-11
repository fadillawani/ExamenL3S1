package javaprojet.Repository;
import javaprojet.Entity.Complement;
import java.util.List;
import java.util.Optional;

public interface ComplementRepository {
    int numberOfRows();
    int insert(Complement complement);
    Optional<Complement> selectById(int id);

    List<Complement> selectAll();


}