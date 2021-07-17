using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewnetCore.Models
{
    public class SQLpersonRepository : IPerson
    {
        private readonly NetCoreDB coreDB;

        public SQLpersonRepository(NetCoreDB coreDB)
        {
            this.coreDB = coreDB;
        }
        public PersonsP Add(PersonsP person)
        {
             
            coreDB.Persons.Add(person);
            coreDB.SaveChanges();
            return person;
        }

        public PersonsP Delete(int id)
        {
           PersonsP persons = coreDB.Persons.Find(id);
            if(persons != null)
            {
                coreDB.Persons.Remove(persons);
                coreDB.SaveChanges();
            } 
            return persons;
        }


        public IEnumerable<PersonsP> GetAllPersonsP()
        {
            return coreDB.Persons;
        }

        public PersonsP GetAllPersonsP(int id)
        {
            return coreDB.Persons.Find(id);
        }

        public PersonsP GetPersonsP(int Id)
        {
            return coreDB.Persons.Find(Id);
        }

        public PersonsP Update(PersonsP personChanges)
        {
            var persons = coreDB.Persons.Attach(personChanges);
            persons.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            coreDB.SaveChanges();
            return personChanges;
        }

        //PersonsP IPerson.GetAllPersonsP()
        //{
           
        //}
    }
}
