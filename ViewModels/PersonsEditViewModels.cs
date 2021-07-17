using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewnetCore.ViewModels
{
    public class PersonsEditViewModels : PersonsCreateViewModels
    {
        public int Id { get; set; }
        public string ExistingPhotoPath { get; set; }
    }
}
