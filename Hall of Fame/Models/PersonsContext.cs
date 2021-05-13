using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Hall_of_Fame.Models.Data;

namespace Hall_of_Fame.Models
{
    public class PersonsContext : DbContext
    {
       public DbSet<Person> Persons { get; set; }

            public PersonsContext(DbContextOptions<PersonsContext> options)
                : base(options)
            {
                Database.Migrate();
            }
    }
}
