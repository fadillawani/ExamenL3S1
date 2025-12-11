package javaprojet.Repository.IMPL;
import javaprojet.Config.Database.Database;
import javaprojet.Entity.BurgerCategorie;
import javaprojet.Repository.BurgerCategorieRepository;


import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.Collections;
import java.util.List;
import java.util.Optional;

public class BurgerCategorieRepositoryImpl implements BurgerCategorieRepository {
    private Database database;

    public BurgerCategorieRepositoryImpl(Database database) {
        this.database = database;
    }

    @Override
    public int numberOfRows() {
        int count = 0;
        try {
            if (!database.isConnected()) {
                throw new SQLException("Erreur de connexion à la BD");
            }
            Connection conn = database.getConnection();
            PreparedStatement ps = conn.prepareStatement("SELECT COUNT(*) FROM burger_categorie");
            ResultSet rs = ps.executeQuery();

            if (rs.next()) {
                count = rs.getInt(1);
            }
            rs.close();
            ps.close();
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return count;
    }


    @Override
    public int insert(BurgerCategorie bc) {
        try {
            if (!database.isConnected()) {
                throw new SQLException("Erreur de connexion à la BD");
            }
            Connection conn = database.getConnection();
            PreparedStatement ps = conn.prepareStatement(
                    "INSERT INTO burger_categorie (id, nom) VALUES (?, ?)"
            );

            ps.setInt(1, bc.getId());
            ps.setString(2, bc.getNom());

            return ps.executeUpdate();

        } catch (SQLException e) {
            e.printStackTrace();
            return 0;
        }
    }


    @Override
    public Optional<BurgerCategorie> selectById(int id) {
        Connection conn = database.getConnection();
        PreparedStatement ps;
        try {
            ps = conn.prepareStatement("select * from burger_categorie where id = ?");
            ps.setInt(1, id);
            return database.<BurgerCategorie>fetch(ps, this::toEntity);
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return Optional.empty();
    }


    @Override
    public List<BurgerCategorie> selectAll() {
        try {
            Connection conn = database.getConnection();
            PreparedStatement ps = conn.prepareStatement("select * from burger_categorie");
            return database.<BurgerCategorie>fetchAll(ps, this::toEntity);
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return Collections.emptyList();
    }

    private BurgerCategorie toEntity(ResultSet rs) throws SQLException {
        BurgerCategorie bc = new BurgerCategorie();
        bc.setId(rs.getInt("id"));
        bc.setNom(rs.getString("nom"));
        return bc;
    }

}
