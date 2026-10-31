namespace MinorProject5.Api.Models;

public class ExerciseResponse
{
    public int ExerciseId { get; set; }
    public string ExerciseName { get; set; } = "";
    public string MuscleGroup { get; set; } = "";
    public string Equipment { get; set; } = "";
    public string Difficulty { get; set; } = "";
}