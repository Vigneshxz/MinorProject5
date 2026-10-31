using MySqlConnector;
using MinorProject5.Data.Database;
using MinorProject5.Data.Models;

namespace MinorProject5.Data.Repositories;

public class ClientRepository
{
    private readonly DbConnectionFactory _connectionFactory;

    public ClientRepository(DbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    // GET ALL CLIENTS
    public async Task<List<Client>> GetAllAsync()
    {
        var clients = new List<Client>();

        await using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync();

        const string sql = """
            SELECT ClientId, FirstName, LastName, Email, Phone, CreatedAt
            FROM Clients
            ORDER BY ClientId
            """;

        await using var command = new MySqlCommand(sql, connection);
        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            clients.Add(MapClient(reader));
        }

        return clients;
    }

    // GET CLIENT BY ID
    public async Task<Client?> GetByIdAsync(int id)
    {
        await using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync();

        const string sql = """
            SELECT ClientId, FirstName, LastName, Email, Phone, CreatedAt
            FROM Clients
            WHERE ClientId = @Id
            """;

        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@Id", id);

        await using var reader = await command.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            return MapClient(reader);
        }

        return null;
    }

    // INSERT CLIENT
    public async Task<int> InsertAsync(Client client)
    {
        await using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync();

        const string sql = """
            INSERT INTO Clients (FirstName, LastName, Email, Phone)
            VALUES (@FirstName, @LastName, @Email, @Phone)
            """;

        await using var command = new MySqlCommand(sql, connection);

        command.Parameters.AddWithValue("@FirstName", client.FirstName);
        command.Parameters.AddWithValue("@LastName", client.LastName);
        command.Parameters.AddWithValue("@Email", client.Email);
        command.Parameters.AddWithValue(
            "@Phone",
            (object?)client.Phone ?? DBNull.Value
        );

        await command.ExecuteNonQueryAsync();

        return checked((int)command.LastInsertedId);
    }

    // UPDATE CLIENT
    public async Task<bool> UpdateAsync(Client client)
    {
        await using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync();

        const string sql = """
            UPDATE Clients
            SET FirstName = @FirstName,
                LastName = @LastName,
                Email = @Email,
                Phone = @Phone
            WHERE ClientId = @ClientId
            """;

        await using var command = new MySqlCommand(sql, connection);

        command.Parameters.AddWithValue("@ClientId", client.ClientId);
        command.Parameters.AddWithValue("@FirstName", client.FirstName);
        command.Parameters.AddWithValue("@LastName", client.LastName);
        command.Parameters.AddWithValue("@Email", client.Email);
        command.Parameters.AddWithValue(
            "@Phone",
            (object?)client.Phone ?? DBNull.Value
        );

        int rowsAffected = await command.ExecuteNonQueryAsync();

        return rowsAffected > 0;
    }

    // DELETE CLIENT
    public async Task<bool> DeleteAsync(int id)
    {
        await using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync();

        const string sql = """
            DELETE FROM Clients
            WHERE ClientId = @Id
            """;

        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@Id", id);

        int rowsAffected = await command.ExecuteNonQueryAsync();

        return rowsAffected > 0;
    }

    // CONVERT DATABASE ROW INTO CLIENT OBJECT
    private static Client MapClient(MySqlDataReader reader)
    {
        return new Client
        {
            ClientId = reader.GetInt32("ClientId"),
            FirstName = reader.GetString("FirstName"),
            LastName = reader.GetString("LastName"),
            Email = reader.GetString("Email"),
            Phone = reader.IsDBNull(reader.GetOrdinal("Phone"))
                ? null
                : reader.GetString("Phone"),
            CreatedAt = reader.GetDateTime("CreatedAt")
        };
    }
}