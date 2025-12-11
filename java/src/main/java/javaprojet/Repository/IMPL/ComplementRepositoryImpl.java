package javaprojet.Repository.IMPL;
import javaprojet.Config.Database.Database;
import javaprojet.Entity.Complement;
import javaprojet.Entity.Enum.TypeComplement;
import javaprojet.Repository.ComplementRepository;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.Collections;
import java.util.List;
import java.util.Optional;



public class ComplementRepositoryImpl implements ComplementRepository {
    private Database database;

    public ComplementRepositoryImpl(Database database) {
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
            PreparedStatement ps = conn.prepareStatement("SELECT COUNT(*) FROM complement");
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
    public int insert(Complement c) {
        try {
            if (!database.isConnected()) {
                throw new SQLException("Erreur de connexion à la BD");
            }
            Connection conn = database.getConnection();
            PreparedStatement ps = conn.prepareStatement(
                    "INSERT INTO complement (id, libelle, prix, image_url, is_archived, type_complement) " +
                            "VALUES (?, ?, ?, ?, ?, ?::type_complement)"
            );

            ps.setInt(1, c.getId());
            ps.setString(2, c.getLibelle());
            ps.setDouble(3, c.getPrix());
            ps.setString(4, c.getImageUrl());
            ps.setBoolean(5, c.getArchived());
            ps.setString(6, c.getTypeComplement().name());

            return ps.executeUpdate();

        } catch (SQLException e) {
            e.printStackTrace();
            return 0;
        }
    }

    @Override
    public Optional<Complement> selectById(int id) {
        Connection conn = database.getConnection();
        PreparedStatement ps;
        try {
            ps = conn.prepareStatement("select * from complement where id = ?");
            ps.setInt(1, id);
            return database.<Complement>fetch(ps, this::toEntity);
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return Optional.empty();
    }


    @Override
    public List<Complement> selectAll() {
        try {
            Connection conn = database.getConnection();
            PreparedStatement ps = conn.prepareStatement("select * from complement");
            return database.<Complement>fetchAll(ps, this::toEntity);
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return Collections.emptyList();
    }

    private Complement toEntity(ResultSet rs) throws SQLException {
        Complement c = new Complement();
        c.setId(rs.getInt("id"));
        c.setLibelle(rs.getString("libelle"));
        c.setPrix(rs.getDouble("prix"));
        c.setImageUrl(rs.getString("image_url"));
        c.setArchived(rs.getBoolean("is_archived"));
        c.setTypeComplement(TypeComplement.valueOf(rs.getString("type_complement")));
        return c;
    }

}