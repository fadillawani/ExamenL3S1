package javaprojet.Repository.IMPL;
import javaprojet.Config.Database.*;
import javaprojet.Entity.Menu;
import javaprojet.Repository.MenuRepository;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.Collections;
import java.util.List;
import java.util.Optional;

public class MenuRepositoryImpl implements MenuRepository {
    private Database database;

    public MenuRepositoryImpl(Database database) {
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
            PreparedStatement ps = conn.prepareStatement("SELECT COUNT(*) FROM menu");
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
    public int insert(Menu menu) {
        try {
            if (!database.isConnected()) {
                throw new SQLException("Erreur de connexion à la BD");
            }
            Connection conn = database.getConnection();
            PreparedStatement ps = conn.prepareStatement(
                    "INSERT INTO menu (id, libelle, image_url, is_archived, prix) VALUES (?, ?, ?, ?, ?)"
            );

            ps.setInt(1, menu.getId());
            ps.setString(2, menu.getLibelle());
            ps.setString(3, menu.getImageUrl());
            ps.setBoolean(4, menu.getArchived());
            ps.setDouble(5, menu.getPrix());

            return ps.executeUpdate();

        } catch (SQLException e) {
            e.printStackTrace();
            return 0;
        }
    }

    @Override
    public int update(Menu menu) {
        try {
            if (!database.isConnected()) {
                throw new SQLException("Erreur de connexion à la BD");
            }

            Connection conn = database.getConnection();
            PreparedStatement ps = conn.prepareStatement(
                    "UPDATE menu SET libelle = ?, image_url = ?, is_archived = ?, prix = ? WHERE id = ?"
            );

            ps.setString(1, menu.getLibelle());
            ps.setString(2, menu.getImageUrl());
            ps.setBoolean(3, menu.getArchived());
            ps.setDouble(4, menu.getPrix());
            ps.setInt(5, menu.getId());

            return ps.executeUpdate();

        } catch (SQLException e) {
            e.printStackTrace();
            return 0;
        }
    }

    @Override
    public Optional<Menu> selectById(int id) {
        Connection conn = database.getConnection();
        PreparedStatement ps;
        try {
            ps = conn.prepareStatement("select * from menu where id = ?");
            ps.setInt(1, id);
            return database.<Menu>fetch(ps, this::toEntity);
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return Optional.empty();
    }

    @Override
    public List<Menu> selectAll() {
        try {
            Connection conn = database.getConnection();
            PreparedStatement ps = conn.prepareStatement("select * from menu");
            return database.<Menu>fetchAll(ps, this::toEntity);
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return Collections.emptyList();
    }

    private Menu toEntity(ResultSet rs) throws SQLException {
        Menu menu = new Menu();
        menu.setId(rs.getInt("id"));
        menu.setLibelle(rs.getString("libelle"));
        menu.setImageUrl(rs.getString("image_url"));
        menu.setArchived(rs.getBoolean("is_archived"));
        menu.setPrix(rs.getDouble("prix"));
        return menu;
    }


}