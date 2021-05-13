using Hall_of_Fame.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hall_of_Fame.Models.Data;

namespace Hall_of_Fame.Controllers
{

    [ApiController]
    [Route("api/v1")]
    public class PersonsController : ControllerBase
    {

           private readonly PersonsContext _db;
           public PersonsController(PersonsContext context)
           {
               _db = context;
           }

        //GET api/v1/persons
        //Возвращает массив объектов типа Person
        [HttpGet("persons")]
        public async Task<ActionResult<IEnumerable<Person>>> Get()
        {
            if (!_db.Persons.Any())
                return NotFound();
            else
                return await _db.Persons.Include(item => item.Skills).ToListAsync();
        }

        //GET api/v1/person/[id]
        //Возвращает объект типа Person.
        [HttpGet("person/{id}")]
        public async Task<ActionResult<Person>> Get(int id)
        {
            Person person = await _db.Persons.Include(item => item.Skills).FirstOrDefaultAsync(x => x.Id == id);
            if (person == null)
                return NotFound();
            return new ObjectResult(person);
        }


        //PUT api/v1/person/[id]
        //Обновляет данные сотрудника
        [HttpPut("person/{id}")]
        public async Task<ActionResult<Person>> EditPerson(Person person, int id)
        {
            if (person == null)
            {
                return BadRequest();
            }

            if (!_db.Persons.Any(x => x.Id == id))
            {
                return NotFound();
            }

            _db.Persons.Attach(person);
            _db.Entry(person).Property(x => x.Name).IsModified = true;
            _db.Entry(person).Property(x => x.DisplayName).IsModified = true;
            await _db.SaveChangesAsync();
            return Ok(person);
        }

        //POST api/v1/person
        //Создаёт нового сотрудника
        [HttpPost("person")]
        public async Task<ActionResult<Person>> AddPerson(Person person)
        {
            if (person == null)
            {
                return BadRequest();
            }

            _db.Persons.Attach((person));
            _db.Entry(person).Property(x => x.Name).IsModified = true;
            _db.Entry(person).Property(x => x.DisplayName).IsModified = true;
            await _db.SaveChangesAsync();
            return Ok(person);
        }


        //DELETE api/v1/person/[id]
        //Удаляет с указанным id сотрудника из системы.
        [HttpDelete("person/{id}")]
        public async Task<ActionResult<Person>> DeletePerson(int id)
        {
            Person person = _db.Persons.Include(item => item.Skills).FirstOrDefault(x => x.Id == id);
            if (person == null)
            {
                return NotFound();
            }
            _db.Persons.Remove(person);
            await _db.SaveChangesAsync();
            return Ok(person);
        }

    }
}

