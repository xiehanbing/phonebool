using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MPACore.PhoneBook.Controllers;
using MPACore.PhoneBook.PhoneBook;
using MPACore.PhoneBook.PhoneBook.Dtos;

namespace MPACore.PhoneBook.Web.Controllers
{
    public class PersonsController : PhoneBookControllerBase
    {
        private readonly IPersonAppService _personAppService;

        public PersonsController(IPersonAppService personAppService)
        {
            _personAppService = personAppService;
        }
        public async Task<IActionResult> Index(GetPersonInput input)
        {
            var data = await _personAppService.GetPagePersonAsync(input);
            return View(data);
        }
    }
}