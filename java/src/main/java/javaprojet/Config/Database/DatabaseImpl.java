package javaprojet.Config.Database;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import java.util.Optional;

public class DatabaseImpl implements Database {
    private Connection connection;
    public static DatabaseImpl instance;

    public static DatabaseImpl getInstance(Map<String, String> config) {
        if (instance == null)
            instance = new DatabaseImpl(config);
        return instance;
    }

    public static DatabaseImpl getInstance(String driver, String url, String user, String pwd) {
        if (instance == null)
            instance = new DatabaseImpl(driver, url, user, pwd);
        return instance;
    }

    private DatabaseImpl(Map<String, String> config) {
        String driver = config.get("driver");
        String url = config.get("url");
        String user = config.get("user");
        String password = config.get("password");
        connection = openConnection(driver, url, user, password);
    }

    private DatabaseImpl(String driver, String url, String user, String pwd) {
        connection = openConnection(driver, url, user, pwd);
    }

    public Connection openConnection(String driver, String url, String user, String pwd) {
        try {
            Class.forName(driver);
            return DriverManager.getConnection(url, user, pwd);
        } catch (ClassNotFoundException | SQLException e) {
            e.printStackTrace();
        }
        return null;
    }

    @Override
    public Connection getConnection() {
        return connection;
    }

    @Override
    public boolean isConnected() {
        return connection != null;
    }

    @Override
    public void closeConnection() {
        if (connection != null) {
            try {
                connection.close();
            } catch (SQLException e) {
                e.printStackTrace();
            }
        }
    }

    @Override
    public <T> Optional<T> fetch(PreparedStatement ps, Convert<T> convert) throws SQLException {
        ResultSet rs = ps.executeQuery();
        T data = null;
        if (rs.next()) {
            data = convert.toEntity(rs);
        }
        return Optional.ofNullable(data);
    }

    @Override
    public <T> List<T> fetchAll(PreparedStatement ps, Convert<T> convert) throws SQLException {
        ResultSet rs = ps.executeQuery();
        List<T> datas = new ArrayList<>();
        while (rs.next()) {
            datas.add(convert.toEntity(rs));
        }
        return datas;
    }
}
