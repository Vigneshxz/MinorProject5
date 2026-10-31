using MySqlConnector;
using MinorProject5.Data.Database;
using MinorProject5.Data.Models;

namespace MinorProject5.Data.Repositories;

public class ExerciseRepository
{
    private readonly DbConnectionFactory _connectionFactory;

    public ExerciseRepository(DbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<List<Exercise>> GetAllAsync()
    {
        var exercises = new List<Exercise>();

        await using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync();

        const string sql = """
            SELECT ExerciseId, ExerciseName, MuscleGroup, Equipment, Difficulty
            FROM Exercises
            ORDER BY ExerciseId
            """;

        await using var command = new MySqlCommand(sql, connection);
        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            exercises.Add(MapExercise(reader));
        }

        return exercises;
    }

    public async Task<Exercise?> GetByIdAsync(int id)
    {
        await using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync();

        const string sql = """
            SELECT ExerciseId, ExerciseName, MuscleGroup, Equipment, Difficulty
            FROM Exercises
            WHERE ExerciseId = @Id
            """;

        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@Id", id);

        await using var reader = await command.ExecuteReaderAsync();

        return await reader.ReadAsync() ? MapExercise(reader) : null;
    }

    public async Task<int> InsertAsync(Exercise exercise)
    {
        await using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync();

        const string sql = """
            INSERT INTO Exercises
                (ExerciseName, MuscleGroup, Equipment, Difficulty)
            VALUES
                (@ExerciseName, @MuscleGroup, @Equipment, @Difficulty)
            """;

        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@ExerciseName", exercise.ExerciseName);
        command.Parameters.AddWithValue("@MuscleGroup", exercise.MuscleGroup);
        command.Parameters.AddWithValue("@Equipment", exercise.Equipment);
        command.Parameters.AddWithValue("@Difficulty", exercise.Difficulty);

        await command.ExecuteNonQueryAsync();

        return checked((int)command.LastInsertedId);
    }

    public async Task<bool> UpdateAsync(Exercise exercise)
    {
        await using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync();

        const string sql = """
            UPDATE Exercises
            SET ExerciseName = @ExerciseName,
                MuscleGroup = @MuscleGroup,
                Equipment = @Equipment,
                Difficulty = @Difficulty
            WHERE ExerciseId = @ExerciseId
            """;

        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@ExerciseId", exercise.ExerciseId);
        command.Parameters.AddWithValue("@ExerciseName", exercise.ExerciseName);
        command.Parameters.AddWithValue("@MuscleGroup", exercise.MuscleGroup);
        command.Parameters.AddWithValue("@Equipment", exercise.Equipment);
        command.Parameters.AddWithValue("@Difficulty", exercise.Difficulty);

        return await command.ExecuteNonQueryAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        await using var connection = _connectionFactory.CreateConnection();
        await connection.OpenAsync();

        const string sql = "DELETE FROM Exercises WHERE ExerciseId = @Id";

        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@Id", id);

        return await command.ExecuteNonQueryAsync() > 0;
    }

    private static Exercise MapExercise(MySqlDataReader reader)
    {
        return new Exercise
        {
            ExerciseId = reader.GetInt32("ExerciseId"),
            ExerciseName = reader.GetString("ExerciseName"),
            MuscleGroup = reader.GetString("MuscleGroup"),
            Equipment = reader.GetString("Equipment"),
            Difficulty = reader.GetString("Difficulty")
        };
    }
}