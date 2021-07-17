using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewnetCore.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed (this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonsP>().HasData(
               new PersonsP
               {
                   Id = 1,
                   Name = "Amina",
                   Task = "Launch",
                   Date = Convert.ToDateTime("01/01/2010").Date

               },
               new PersonsP
               {
                   Id = 2,
                   Name = "Kolo",
                   Task = "Morning Code",
                   Date = Convert.ToDateTime("01/01/2010").Date

               }
               );

        }
    }
}
