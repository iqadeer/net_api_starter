using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using NetAPI.Application.Dtos;
using NetAPI.Application.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NetAPI.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SpeakersController(ISpeakerService speakerService, IValidator<SpeakerDto> speakerValidator) : ControllerBase
{
    // GET: api/<SpeakersController>
    [HttpGet(Name = "GetAll")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Get()
    {
        return Ok(speakerService.GetAll());
    }

    // GET api/<SpeakersController>/5
    [HttpGet("{speakerId:int}", Name = "Get")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Get([FromRoute]int speakerId)
    {
        if (speakerId == 0) throw new ArgumentOutOfRangeException(nameof(speakerId), "Speaker id is not in range");
        var result = speakerService.Get(speakerId);
        if (result is null) return NotFound();
        return Ok(speakerService.Get(speakerId));
    }

    // POST api/<SpeakersController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Post([FromBody] SpeakerDto speakerDto)
    {
        var result = await speakerValidator.ValidateAsync(speakerDto);
        if (!result.IsValid)
        {
            var errors = result.Errors.Select(e => new { e.PropertyName, e.ErrorMessage, e.ErrorCode });
            return BadRequest(new
            {
                Message = "Validation failed for speaker",
                errors
            });
        }

        var speakerId = speakerService.Create(speakerDto);
        return CreatedAtRoute("", new { speakerId });
    }

    // PUT api/<SpeakersController>/5
    [HttpPut("{speakerId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> Put(int speakerId, [FromBody] SpeakerDto speakerDto)
    {
        var result = await speakerValidator.ValidateAsync(speakerDto);
        if (!result.IsValid)
        {
            var errors = result.Errors.Select(e => new { e.PropertyName, e.ErrorMessage, e.ErrorCode });
            return BadRequest(new
            {
                Message = "Validation failed for speaker",
                errors
            });
        }
        speakerService.Update(speakerDto);
        return NoContent();
    }

    // DELETE api/<SpeakersController>/5
    [HttpDelete("{speakerId:int}")]
    public IActionResult Delete(int speakerId)
    {
        speakerService.Delete(speakerId);
        return NoContent();
    }
}