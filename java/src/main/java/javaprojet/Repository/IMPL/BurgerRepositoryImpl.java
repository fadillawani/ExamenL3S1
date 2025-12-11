package javaprojet.Repository.IMPL;
import javaprojet.Config.Database.Database;
import javaprojet.Entity.Burger;
import javaprojet.Repository.BurgerRepository;


import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.Collections;
import java.util.List;
import java.util.Optional;

public class BurgerRepositoryImpl implements BurgerRepository {
    private Database database;

    public BurgerRepositoryImpl(Database database) {
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
            PreparedStatement ps = conn.prepareStatement("SELECT COUNT(*) FROM burger");
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
    public int insert(Burger burger) {
        try {
            if (!database.isConnected()) {
                throw new SQLException("Erreur de connexion à la BD");
            }
            Connection conn = database.getConnection();
            PreparedStatement ps = conn.prepareStatement(
                    "INSERT INTO burger (id, libelle, description, prix, image_url, is_archived, burger_categorie_id) " +
                            "VALUES (?, ?, ?, ?, ?, ?, ?)"
            );

            ps.setInt(1, burger.getId());
            ps.setString(2, burger.getLibelle());
            ps.setString(3, burger.getDesc());
            ps.setDouble(4, burger.getPrix());
            ps.setString(5, burger.getImageUrl());
            ps.setBoolean(6, burger.getArchived());
            ps.setInt(7, burger.getBurgerCategorieId());

            return ps.executeUpdate();

        } catch (SQLException e) {
            e.printStackTrace();
            return 0;
        }
    }

    @Override
    public Optional<Burger> selectById(int id) {
        Connection conn = database.getConnection();
        PreparedStatement ps;
        try {
            ps = conn.prepareStatement("select * from burger where id = ?");
            ps.setInt(1, id);
            return database.<Burger>fetch(ps, this::toEntity);
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return Optional.empty();
    }

    @Override
    public List<Burger> selectAll() {
        try {
            Connection conn = database.getConnection();
            PreparedStatement ps = conn.prepareStatement("select * from burger");
            return database.<Burger>fetchAll(ps, this::toEntity);
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return Collections.emptyList();
    }

    private Burger toEntity(ResultSet rs) throws SQLException {
        Burger burger = new Burger();
        burger.setId(rs.getInt("id"));
        burger.setLibelle(rs.getString("libelle"));
        burger.setDesc(rs.getString("description"));
        burger.setPrix(rs.getDouble("prix"));
        burger.setImageUrl(rs.getString("image_url"));
        burger.setArchived(rs.getBoolean("is_archived"));
        burger.setBurgerCategorieId(rs.getInt("burger_categorie_id"));
        return burger;
    }

}
