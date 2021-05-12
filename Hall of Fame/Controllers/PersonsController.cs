using Hall_of_Fame.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Hall_of_Fame;
using Hall_of_Fame.Models.Data;
using System.Web.Http;

namespace Hall_of_Fame.Controllers
{
    public class PersonsController : ControllerBase
    {

{
    [ApiController]
    [Route("api/[controller]")]
    /*    public class UsersController : ControllerBase
       {

           PersonsContext db;
           public UsersController(PersonsContext context)
           {
               db = context;
               if (!db.Persons.Any())
               {
                   db.Persons.Add(new Person { Name = "Tom", DisplayName = "Cruise" });
                   db.Persons.Add(new Person { Name = "Alice", DisplayName = "ICM" });
                   db.SaveChanges();
               }
           } */

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> Get()
        {
            return await db.Persons.ToListAsync();
        }

        // GET api/v1/persons
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> Get(int id)
        {
            Person user = await db.Persons.FirstOrDefaultAsync(x => x.Id == id);
            if (person == null)
                return NotFound();
            return new ObjectResult(person);
        }

        public IHttpActionResult GetAllStudents()
        {
            IList<Person> persons = null;

            using (var ctx = new PersonDBEntity())
            {
                persons = ctx.Persons.Include("Skill")
                    .Select(s => new Person()
                    {
                        Id = s.PersonId
                        Name = s.Name,
                        DisplayName = s.DisplayName
                    }).ToList<Skill>();
            }

            if (persons.Count == 0)
            {
                return NotFoundResult();
            }

            return OkResult(persons);
        }


        // PUT api/v1/person/[id]
        [HttpPut("person/{Id}")]
        //Редактирует данные сотрудника с указанным id
        public async Task<ActionResult<Person>> EditPerson([FromBody] Person person)
        {
            if (person == null)
            {
                return BadRequest();
            }

            if (!db.Persons.Any(x => x.Id == person.Id))
            {
                return NotFound();
            }


            db.Persons.Update(person);
            await db.SaveChangesAsync();
            return Ok(person);
        }


    }
}

}
}