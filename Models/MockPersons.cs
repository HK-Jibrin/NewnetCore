using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewnetCore.Models
{
    public class MockPersons : IPerson
    {

        private List<PersonsP> _PersonsList;
        public MockPersons()
        {
            _PersonsList = new List<PersonsP>()
            {
               new PersonsP() { Id = 1, Name ="Mohammad", Task = "Morning Walkout", Date = Convert.ToDateTime("10/01/2010").Date },
               new PersonsP() { Id = 2, Name = "Jubril", Task = "TodoList", Date = Convert.ToDateTime("01/01/2010").Date }

            };
        }

        public int Id { get; private set; }

        public PersonsP Add(PersonsP person)
        {
            person.Id = _PersonsList.Max(p => p.Id) + 1;
            _PersonsList.Add(person);
            return person;
        }

        public PersonsP Delete(int id)
        {
          PersonsP person =  _PersonsList.FirstOrDefault(p => p.Id == id);
              if(person != null)
            {
                _PersonsList.Remove(person);
                
            }
            return person;
        }

        public IEnumerable<PersonsP> GetAllPersonsP()
        {
            return _PersonsList;
        }

        //public IEnumerable<PersonsP> GetAllPersonsP(int id)
        //{
        //    throw new NotImplementedException();
        //}

        public PersonsP GetPersonsP(int Id)
        {
            return _PersonsList.FirstOrDefault(p => p.Id == Id);
        }

        public PersonsP Update(PersonsP personChanges)
        {
            PersonsP person = _PersonsList.FirstOrDefault(p => p.Id == personChanges.Id);
            if (person != null)
            {
                person.Name = personChanges.Name;
                person.Task = personChanges.Task;
                person.Date = personChanges.Date;

            }
            return person;
        }

        public PersonsP GetAllPersonsP(int id)
        {
            return _PersonsList.FirstOrDefault(p => p.Id == id);
        }

    }
}
 