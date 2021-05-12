using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hall_of_Fame.Models;
using Hall_of_Fame.Models.Data;
using Hall_of_Fame.Controllers;

namespace Hall_of_Fame
{
    public class PersonDBEntity : ControllerBase
    {
        PersonsContext db;
        public PersonsController(PersonsContext context)
            {
                db = context;
                if (!db.Persons.Any())
                {
                    db.Persons.Add(new Person
                    {
                        Name = "Валерий Жмышенко",
                        DisplayName = "Валерий Жмышенко",
                        Skills = new List<Skill>()
                        {
                            new Skill() { Name = "Уборка", Level = 5 }
                        }
                    });
                    db.Persons.Add(new Person
                    {
                        Name = "Денис Иванов",
                        DisplayName = "Денис Иванов",
                        Skills = new List<Skill>()
                        {
                            new Skill() { Name = "Лидерство", Level = 3 },
                            new Skill() { Name = "Пасьянсы", Level = 9 }
                        }
                    });
                    db.SaveChanges();
                }
            }

    }
    }
