using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using NetAPI.Application.Dtos;
using NetAPI.Application.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NetAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController(IPersonService personService, IValidator<PersonDto> personDtoValidator)
        : ControllerBase
    {
        // GET: api/<PersonController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            return Ok(personService.GetPersons());
        }

        // GET api/<PersonController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get([FromRoute]int id)
        {
            var person = personService.GetPerson(id);
            if (person == null) return NotFound();
            return Ok(person);
        }

        // POST api/<PersonController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] PersonDto person)
        {
            var result = await personDtoValidator.ValidateAsync(person);
            if (!result.IsValid)
            {
                var errors = result.Errors.Select(e => new { e.PropertyName, e.ErrorMessage, e.ErrorCode });
                return BadRequest(new
                {
                    Message = "Validation failed for speaker",
                    errors
                });
            }
            var personId = personService.AddPerson(person);
            return CreatedAtRoute("", new { id = personId });
        }

        // PUT api/<PersonController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PersonController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
