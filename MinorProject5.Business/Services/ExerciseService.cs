using MinorProject5.Data.Models;
using MinorProject5.Data.Repositories;

namespace MinorProject5.Business.Services;

public class ExerciseService
{
    private readonly ExerciseRepository _repository;

    public ExerciseService(ExerciseRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Exercise>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Exercise?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    private static void ValidateDifficulty(Exercise exercise)
{
    string[] allowedDifficulties =
    {
        "Beginner",
        "Intermediate",
        "Advanced"
    };

    if (!allowedDifficulties.Contains(
        exercise.Difficulty,
        StringComparer.OrdinalIgnoreCase))
    {
        throw new ArgumentException(
            "Difficulty must be Beginner, Intermediate, or Advanced.");
    }
}
private static void ValidateExerciseName(Exercise exercise)
{
    if (string.IsNullOrWhiteSpace(exercise.ExerciseName) ||
        exercise.ExerciseName.Trim().Length < 3)
    {
        throw new ArgumentException(
            "Exercise name must contain at least 3 characters.");
    }
}

public async Task<int> CreateAsync(Exercise exercise)
{
    ValidateDifficulty(exercise);
    ValidateExerciseName(exercise);

    return await _repository.InsertAsync(exercise);
}

public async Task<bool> UpdateAsync(Exercise exercise)
{
    ValidateDifficulty(exercise);
    ValidateExerciseName(exercise);

    return await _repository.UpdateAsync(exercise);
}

public async Task<bool> DeleteAsync(int id)
{
    if (id <= 0)
    {
        throw new ArgumentException("Exercise ID must be positive.");
    }

    return await _repository.DeleteAsync(id);
}
}