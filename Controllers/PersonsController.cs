using HallOfFame.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HallOfFame.Controllers;

/// <summary>
/// Контроллер сотрудников
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class PersonsController : ControllerBase
{
    private readonly ApplicationDbContext _applicationDbContext;

    public PersonsController(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    /// <summary>
    /// Получение списка всех сотрудников
    /// </summary>
    /// <returns>Список всех сотрудников</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Person>), 200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IEnumerable<Person>>> GetAllPersons()
    {
        var persons = await _applicationDbContext
            .Persons
            .Include(person => person.Skills)
            .ToListAsync();

        return Ok(persons);
    }

    /// <summary>
    /// Получение сотрудника по его идентификатору
    /// </summary>
    /// <param name="id">Идентификатор сотрудника</param>
    /// <returns>модель сотрудника</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Person), 200)]
    [ProducesResponseType(typeof(string), 404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<Person>> GetPersonById(long id)
    {
        var person = await _applicationDbContext
            .Persons
            .Include(person => person.Skills)
            .FirstOrDefaultAsync(person => person.Id == id);

        if (person == null)
        {
            return NotFound("Сотрудник не найден");
        }

        return Ok(person);
    }


    /// <summary>
    /// Добавление нового сотрудника
    /// </summary>
    /// <param name="person">Модель сотрудника</param>
    /// <returns>Идентификатор созданного сотрудника</returns>
    [HttpPost]
    [ProducesResponseType(typeof(long), 200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<long>> CreatePerson(Person person)
    {
        if (person.Id != 0)
        {
            return BadRequest("Person.Id не должен быть заполнен");
        }

        await _applicationDbContext.Persons.AddAsync(person);
        await _applicationDbContext.SaveChangesAsync();

        return Ok(person.Id);
    }

    /// <summary>
    /// Обновление информации о сотруднике по его идентификатору
    /// </summary>
    /// <param name="id">идентификатор сотрудника</param>
    /// <param name="person"> данные о сотруднике</param>
    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(string), 400)]
    [ProducesResponseType(typeof(string), 404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> UpdatePerson(long id, Person person)
    {
        if (person.Id != 0)
        {
            return BadRequest("Person.Id не должен быть заполнен");
        }

        var personDb = await _applicationDbContext.Persons
            .Include(x => x.Skills)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (personDb == null)
        {
            return NotFound("Сотрудник не найден");
        }

        personDb.Name = person.Name;
        personDb.DisplayName = person.DisplayName;

        _applicationDbContext.Skills.RemoveRange(personDb.Skills);
        personDb.Skills = person.Skills;
        await _applicationDbContext.SaveChangesAsync();

        return Ok();
    }


    /// <summary>
    /// Удаление сотрудника по его идентификатору
    /// </summary>
    /// <param name="id">идентификатор сотрудника</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(string), 404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> DeletePerson(long id)
    {
        var personDb = await _applicationDbContext.Persons
            .Include(x => x.Skills)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (personDb == null)
        {
            return NotFound("Сотрудник не найден");
        }

        _applicationDbContext.Persons.Remove(personDb);
        await _applicationDbContext.SaveChangesAsync();

        return Ok();
    }
}
