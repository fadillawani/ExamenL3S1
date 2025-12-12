package javaprojet.Repository.IMPL;

import javaprojet.Config.Database.Database;
import javaprojet.Entity.MenuBurger;
import javaprojet.Repository.MenuBurgerRepository;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.Collections;
import java.util.List;
import java.util.Optional;

public class MenuBurgerRepositoryImpl implements MenuBurgerRepository {
    private Database database;

    public MenuBurgerRepositoryImpl(Database database) {
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
            PreparedStatement ps = conn.prepareStatement("SELECT COUNT(*) FROM menu_burger");
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
    public int insert(MenuBurger mb) {
        try {
            if (!database.isConnected()) {
                throw new SQLException("Erreur de connexion à la BD");
            }
            Connection conn = database.getConnection();
            PreparedStatement ps = conn.prepareStatement(
                    "INSERT INTO menu_burger (id, menu_id, burger_id, quantite) VALUES (?, ?, ?, ?)"
            );

            ps.setInt(1, mb.getId());
            ps.setInt(2, mb.getMenuId());
            ps.setInt(3, mb.getBurgerId());
            ps.setInt(4, mb.getQuantite());

            return ps.executeUpdate();

        } catch (SQLException e) {
            e.printStackTrace();
            return 0;
        }
    }


    @Override
    public Optional<MenuBurger> selectById(int id) {
        Connection conn = database.getConnection();
        PreparedStatement ps;
        try {
            ps = conn.prepareStatement("select * from menu_burger where id = ?");
            ps.setInt(1, id);
            return database.<MenuBurger>fetch(ps, this::toEntity);
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return Optional.empty();
    }

    @Override
    public List<MenuBurger> selectAll() {
        try {
            Connection conn = database.getConnection();
            PreparedStatement ps = conn.prepareStatement("select * from menu_burger");
            return database.<MenuBurger>fetchAll(ps, this::toEntity);
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return Collections.emptyList();
    }

    @Override
    public List<MenuBurger> findByMenuId(int menuId) {
        List<MenuBurger> list = new ArrayList<>();
        try {
            Connection conn = database.getConnection();
            PreparedStatement ps = conn.prepareStatement(
                    "SELECT * FROM menu_burger WHERE menu_id = ?"
            );
            ps.setInt(1, menuId);

            ResultSet rs = ps.executeQuery();
            while (rs.next()) {
                list.add(new MenuBurger(
                        rs.getInt("id"),
                        rs.getInt("menu_id"),
                        rs.getInt("burger_id"),
                        rs.getInt("quantite")
                ));
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
        return list;
    }


    private MenuBurger toEntity(ResultSet rs) throws SQLException {
        MenuBurger mb = new MenuBurger();
        mb.setId(rs.getInt("id"));
        mb.setMenuId(rs.getInt("menu_id"));
        mb.setBurgerId(rs.getInt("burger_id"));
        mb.setQuantite(rs.getInt("quantite"));
        return mb;
    }

}