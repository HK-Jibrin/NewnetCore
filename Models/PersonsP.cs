using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewnetCore.Models
{
    public class PersonsP
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50,ErrorMessage ="Name cannot exceed the number of character specified")]
        public String Name { get; set; }
        [Required]
        public string Task { get; set; } 
        public DateTime Date{ get; set; }
        public string PhotoPath { get; set; }
    }
}
