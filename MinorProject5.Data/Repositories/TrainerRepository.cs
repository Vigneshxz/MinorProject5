using MySqlConnector;
using MinorProject5.Data.Database;
using MinorProject5.Data.Models;

namespace MinorProject5.Data.Repositories;

public class TrainerRepository
{
    private readonly DbConnectionFactory _connectionFactory;

    public TrainerRepository(DbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<List<Trainer>> GetAllAsync()
    {
        var trainers = new List<Trainer>();

        await using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync();

        const string sql = """
            SELECT TrainerId, FirstName, LastName, Email, Specialty
            FROM Trainers
            ORDER BY TrainerId
            """;

        await using var command = new MySqlCommand(sql, connection);
        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            trainers.Add(MapTrainer(reader));
        }

        return trainers;
    }

    public async Task<Trainer?> GetByIdAsync(int id)
    {
        await using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync();

        const string sql = """
            SELECT TrainerId, FirstName, LastName, Email, Specialty
            FROM Trainers
            WHERE TrainerId = @Id
            """;

        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@Id", id);

        await using var reader = await command.ExecuteReaderAsync();

        return await reader.ReadAsync() ? MapTrainer(reader) : null;
    }

    public async Task<int> InsertAsync(Trainer trainer)
    {
        await using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync();

        const string sql = """
            INSERT INTO Trainers (FirstName, LastName, Email, Specialty)
            VALUES (@FirstName, @LastName, @Email, @Specialty)
            """;

        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@FirstName", trainer.FirstName);
        command.Parameters.AddWithValue("@LastName", trainer.LastName);
        command.Parameters.AddWithValue("@Email", trainer.Email);
        command.Parameters.AddWithValue("@Specialty", trainer.Specialty);

        await command.ExecuteNonQueryAsync();

        return checked((int)command.LastInsertedId);
    }

    public async Task<bool> UpdateAsync(Trainer trainer)
    {
        await using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync();

        const string sql = """
            UPDATE Trainers
            SET FirstName = @FirstName,
                LastName = @LastName,
                Email = @Email,
                Specialty = @Specialty
            WHERE TrainerId = @TrainerId
            """;

        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@TrainerId", trainer.TrainerId);
        command.Parameters.AddWithValue("@FirstName", trainer.FirstName);
        command.Parameters.AddWithValue("@LastName", trainer.LastName);
        command.Parameters.AddWithValue("@Email", trainer.Email);
        command.Parameters.AddWithValue("@Specialty", trainer.Specialty);

        return await command.ExecuteNonQueryAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        await using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync();

        const string sql = "DELETE FROM Trainers WHERE TrainerId = @Id";

        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@Id", id);

        return await command.ExecuteNonQueryAsync() > 0;
    }

    private static Trainer MapTrainer(MySqlDataReader reader)
    {
        return new Trainer
        {
            TrainerId = reader.GetInt32("TrainerId"),
            FirstName = reader.GetString("FirstName"),
            LastName = reader.GetString("LastName"),
            Email = reader.GetString("Email"),
            Specialty = reader.GetString("Specialty")
        };
    }
}