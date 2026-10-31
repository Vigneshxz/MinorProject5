using Microsoft.AspNetCore.Mvc;
using MinorProject5.Api.Models;
using MinorProject5.Business.Services;
using MinorProject5.Data.Models;

namespace MinorProject5.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TrainersController : ControllerBase
{
    private readonly TrainerService _service;

    public TrainersController(TrainerService service)
    {
        _service = service;
    }

    // GET: api/trainers
    [HttpGet]
    public async Task<ActionResult<List<TrainerResponse>>> GetAll()
    {
        var trainers = await _service.GetAllAsync();

        var response = trainers.Select(trainer => new TrainerResponse
        {
            TrainerId = trainer.TrainerId,
            FirstName = trainer.FirstName,
            LastName = trainer.LastName,
            Email = trainer.Email,
            Specialty = trainer.Specialty
        }).ToList();

        return Ok(response);
    }

    // GET: api/trainers/1
    [HttpGet("{id:int}")]
    public async Task<ActionResult<TrainerResponse>> GetById(int id)
    {
        var trainer = await _service.GetByIdAsync(id);

        if (trainer == null)
        {
            return NotFound();
        }

        var response = new TrainerResponse
        {
            TrainerId = trainer.TrainerId,
            FirstName = trainer.FirstName,
            LastName = trainer.LastName,
            Email = trainer.Email,
            Specialty = trainer.Specialty
        };

        return Ok(response);
    }

    // POST: api/trainers
    [HttpPost]
    public async Task<ActionResult> Create(TrainerCreateRequest request)
    {
        try
        {
            var trainer = new Trainer
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Specialty = request.Specialty
            };

            int newId = await _service.CreateAsync(trainer);

            return CreatedAtAction(
                nameof(GetById),
                new { id = newId },
                new { trainerId = newId }
            );
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // POST: api/trainers/1
    [HttpPost("{id:int}")]
    public async Task<ActionResult> Update(int id, TrainerUpdateRequest request)
    {
        try
        {
            var trainer = new Trainer
            {
                TrainerId = id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Specialty = request.Specialty
            };

            bool updated = await _service.UpdateAsync(trainer);

            if (!updated)
            {
                return NotFound();
            }

            return Ok("Trainer updated successfully.");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // DELETE: api/trainers/1
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

            return Ok("Trainer deleted successfully.");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}