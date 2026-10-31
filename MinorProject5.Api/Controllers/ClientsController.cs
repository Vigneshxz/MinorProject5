using Microsoft.AspNetCore.Mvc;
using MinorProject5.Api.Models;
using MinorProject5.Business.Services;
using MinorProject5.Data.Models;
using MySqlConnector;

namespace MinorProject5.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly ClientService _service;

    public ClientsController(ClientService service)
    {
        _service = service;
    }

    // GET: api/clients
    [HttpGet]
    public async Task<ActionResult<List<ClientResponse>>> GetAll()
    {
        var clients = await _service.GetAllAsync();

        var response = clients.Select(client => new ClientResponse
        {
            ClientId = client.ClientId,
            FirstName = client.FirstName,
            LastName = client.LastName,
            Email = client.Email,
            Phone = client.Phone
        }).ToList();

        return Ok(response);
    }

    // GET: api/clients/1
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ClientResponse>> GetById(int id)
    {
        var client = await _service.GetByIdAsync(id);

        if (client == null)
        {
            return NotFound();
        }

        var response = new ClientResponse
        {
            ClientId = client.ClientId,
            FirstName = client.FirstName,
            LastName = client.LastName,
            Email = client.Email,
            Phone = client.Phone
        };

        return Ok(response);
    }

    // POST: api/clients

[HttpPost]
public async Task<ActionResult> Create(ClientCreateRequest request)
{
    try
    {
        var client = new Client
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Phone = request.Phone
        };

        int newId = await _service.CreateAsync(client);

        return CreatedAtAction(
            nameof(GetById),
            new { id = newId },
            new { clientId = newId }
        );
    }
    catch (ArgumentException ex)
    {
        return BadRequest(ex.Message);
    }
    catch (MySqlException ex) when (ex.Number == 1062)
    {
        return Conflict("A client with this email already exists.");
    }
}

    // POST: api/clients/1
    // POST: api/clients/1
[HttpPost("{id:int}")]
public async Task<ActionResult> Update(
    int id,
    ClientUpdateRequest request)
{
    try
    {
        var client = new Client
        {
            ClientId = id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Phone = request.Phone
        };

        bool updated = await _service.UpdateAsync(client);

        if (!updated)
        {
            return NotFound();
        }

        return Ok("Client updated successfully.");
    }
    catch (ArgumentException ex)
    {
        return BadRequest(ex.Message);
    }
    catch (MySqlException ex) when (ex.Number == 1062)
    {
        return Conflict("A client with this email already exists.");
    }
}

    // DELETE: api/clients/1
    [HttpDelete("{id:int}")]
public async Task<ActionResult> Delete(int id)
{
    try
    {
        bool deleted = await _service.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return Ok("Client deleted successfully.");
    }
    catch (ArgumentException ex)
    {
        return BadRequest(ex.Message);
    }
    catch (MySqlException ex) when (ex.Number == 1451)
    {
        return Conflict(
            "Cannot delete this client because they have associated training plans."
        );
    }
}
}