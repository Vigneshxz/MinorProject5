using MinorProject5.Data.Models;
using MinorProject5.Data.Repositories;

namespace MinorProject5.Business.Services;

public class TrainerService
{
    private readonly TrainerRepository _repository;

    public TrainerService(TrainerRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Trainer>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Trainer?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    private static void ValidateTrainer(Trainer trainer)
{
    if (string.IsNullOrWhiteSpace(trainer.Specialty))
    {
        throw new ArgumentException(
            "Trainer specialty is required.");
    }
}

public async Task<int> CreateAsync(Trainer trainer)
{
    ValidateTrainer(trainer);

    return await _repository.InsertAsync(trainer);
}

public async Task<bool> UpdateAsync(Trainer trainer)
{
    ValidateTrainer(trainer);

    return await _repository.UpdateAsync(trainer);
}

public async Task<bool> DeleteAsync(int id)
{
    if (id <= 0)
    {
        throw new ArgumentException("Trainer ID must be positive.");
    }

    return await _repository.DeleteAsync(id);
}
}