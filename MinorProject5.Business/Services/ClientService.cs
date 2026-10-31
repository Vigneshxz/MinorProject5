using System.Net.Mail;
using MinorProject5.Data.Models;
using MinorProject5.Data.Repositories;

namespace MinorProject5.Business.Services;

public class ClientService
{
    private readonly ClientRepository _repository;

    public ClientService(ClientRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Client>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Client?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    private static void ValidateClientName(Client client)
{
    if (string.IsNullOrWhiteSpace(client.FirstName) ||
        string.IsNullOrWhiteSpace(client.LastName))
    {
        throw new ArgumentException(
            "Client first name and last name are required.");
    }
}

private static void ValidateClientEmail(Client client)
{
    if (string.IsNullOrWhiteSpace(client.Email) ||
        !MailAddress.TryCreate(client.Email, out var address) ||
        address.Address != client.Email ||
        !client.Email.Contains('@'))
    {
        throw new ArgumentException(
            "A valid client email address is required.");
    }
}

public async Task<int> CreateAsync(Client client)
{
    ValidateClientName(client);
    ValidateClientEmail(client);

    return await _repository.InsertAsync(client);
}

public async Task<bool> UpdateAsync(Client client)
{
    ValidateClientName(client);
    ValidateClientEmail(client);

    return await _repository.UpdateAsync(client);
}

public async Task<bool> DeleteAsync(int id)
{
    if (id <= 0)
    {
        throw new ArgumentException("Client ID must be positive.");
    }

    return await _repository.DeleteAsync(id);
}
}