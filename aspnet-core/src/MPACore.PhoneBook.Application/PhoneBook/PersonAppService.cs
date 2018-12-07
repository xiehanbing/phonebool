using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using MPACore.PhoneBook.PhoneBook.Dtos;
using MPACore.PhoneBook.PhoneBooks.Persons;

namespace MPACore.PhoneBook.PhoneBook
{
    public class PersonAppService : PhoneBookAppServiceBase, IPersonAppService
    {
        private readonly IRepository<Person> _personRepostRepository;
        public PersonAppService(IRepository<Person> personRepostRepository)
        {
            _personRepostRepository = personRepostRepository;
        }

        public async Task<PagedResultDto<PersonListDto>> GetPagePersonAsync(GetPersonInput input)
        {
            var query = _personRepostRepository.GetAll();
            var personCount = await query.CountAsync();
            var persons = await query.OrderBy(input.Sorting).PageBy(input).ToListAsync();
            var dtos = persons.MapTo<List<PersonListDto>>();
            return new PagedResultDto<PersonListDto>(personCount, dtos);
        }

        public async Task<PersonListDto> GetPersonByIdAsync(NullableIdDto input)
        {
            var query = await _personRepostRepository.GetAsync(input.Id.Value);
            return query.MapTo<PersonListDto>();
        }

        public async Task<GetPersonEditOuputDto> GetPersonForEditAsync(NullableIdDto input)
        {
           var output=new GetPersonEditOuputDto();
            PersonEditDto personEditDto=new PersonEditDto();
            if (input.Id.HasValue)
            {
                var entity = await _personRepostRepository.GetAsync(input.Id.Value);
                personEditDto = entity.MapTo<PersonEditDto>();
            }

            output.Person = personEditDto;
            return output;
        }

        public async Task AddOrUpdatePersonAsync(AddOrUpdatePersonDto input)
        {
            Logger.Debug("insert a new person");
            if (!input.Person.Id.HasValue)
            {
                await AddPersonAsync(input.Person);
            }
            else
            {
                await UpdatePersonAsync(input.Person);
            }
        }

        public async Task DeletePersonAsync(int id)
        {
            await _personRepostRepository.DeleteAsync(id);
        }

        protected async Task UpdatePersonAsync(PersonEditDto intput)
        {
            await _personRepostRepository.UpdateAsync(intput.MapTo<Person>());
        }

        protected async Task AddPersonAsync(PersonEditDto intput)
        {
            await _personRepostRepository.InsertAsync(intput.MapTo<Person>());
        }
    }
}
