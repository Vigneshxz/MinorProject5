using Microsoft.AspNetCore.Mvc;
using MinorProject5.Api.Models;
using MinorProject5.Business.Services;
using MinorProject5.Data.Models;

namespace MinorProject5.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExercisesController : ControllerBase
{
    private readonly ExerciseService _service;

    public ExercisesController(ExerciseService service)
    {
        _service = service;
    }

    // GET: api/exercises
    [HttpGet]
    public async Task<ActionResult<List<ExerciseResponse>>> GetAll()
    {
        var exercises = await _service.GetAllAsync();

        var response = exercises.Select(exercise => new ExerciseResponse
        {
            ExerciseId = exercise.ExerciseId,
            ExerciseName = exercise.ExerciseName,
            MuscleGroup = exercise.MuscleGroup,
            Equipment = exercise.Equipment,
            Difficulty = exercise.Difficulty
        }).ToList();

        return Ok(response);
    }

    // GET: api/exercises/1
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ExerciseResponse>> GetById(int id)
    {
        var exercise = await _service.GetByIdAsync(id);

        if (exercise == null)
        {
            return NotFound();
        }

        var response = new ExerciseResponse
        {
            ExerciseId = exercise.ExerciseId,
            ExerciseName = exercise.ExerciseName,
            MuscleGroup = exercise.MuscleGroup,
            Equipment = exercise.Equipment,
            Difficulty = exercise.Difficulty
        };

        return Ok(response);
    }

    // POST: api/exercises
    [HttpPost]
    public async Task<ActionResult> Create(ExerciseCreateRequest request)
    {
        try
        {
            var exercise = new Exercise
            {
                ExerciseName = request.ExerciseName,
                MuscleGroup = request.MuscleGroup,
                Equipment = request.Equipment,
                Difficulty = request.Difficulty
            };

            int newId = await _service.CreateAsync(exercise);

            return CreatedAtAction(
                nameof(GetById),
                new { id = newId },
                new { exerciseId = newId }
            );
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // POST: api/exercises/1
    [HttpPost("{id:int}")]
    public async Task<ActionResult> Update(int id, ExerciseUpdateRequest request)
    {
        try
        {
            var exercise = new Exercise
            {
                ExerciseId = id,
                ExerciseName = request.ExerciseName,
                MuscleGroup = request.MuscleGroup,
                Equipment = request.Equipment,
                Difficulty = request.Difficulty
            };

            bool updated = await _service.UpdateAsync(exercise);

            if (!updated)
            {
                return NotFound();
            }

            return Ok("Exercise updated successfully.");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // DELETE: api/exercises/1
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

            return Ok("Exercise deleted successfully.");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}