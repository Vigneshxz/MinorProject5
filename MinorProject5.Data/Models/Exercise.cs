namespace MinorProject5.Data.Models;

public class Exercise
{
    public int ExerciseId { get; set; }

    public string ExerciseName { get; set; } = string.Empty;

    public string MuscleGroup { get; set; } = string.Empty;

    public string Equipment { get; set; } = string.Empty;

    public string Difficulty { get; set; } = string.Empty;
}