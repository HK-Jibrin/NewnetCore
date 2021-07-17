using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewnetCore.Models
{
  public  interface IPerson
    {
        PersonsP GetPersonsP(int Id);
        PersonsP GetAllPersonsP(int id);
        PersonsP Add(PersonsP person);
        PersonsP Update(PersonsP personChanges);
        PersonsP Delete(int id);
        IEnumerable<PersonsP> GetAllPersonsP();
    }
}
