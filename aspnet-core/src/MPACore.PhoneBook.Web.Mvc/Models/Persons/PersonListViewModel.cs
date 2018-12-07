using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MPACore.PhoneBook.PhoneBook.Dtos;

namespace MPACore.PhoneBook.Web.Models.Persons
{
    public class PersonListViewModel
    {
        public IReadOnlyList<PersonListDto> Persons { get; set; }
    }
}
