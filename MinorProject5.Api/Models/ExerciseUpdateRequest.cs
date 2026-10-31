namespace MinorProject5.Api.Models;

public class ExerciseUpdateRequest
{
    public string ExerciseName { get; set; } = "";
    public string MuscleGroup { get; set; } = "";
    public string Equipment { get; set; } = "";
    public string Difficulty { get; set; } = "";
}