namespace MinorProject5.Api.Models;

public class TrainerUpdateRequest
{
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Email { get; set; } = "";
    public string? Specialty { get; set; }
}