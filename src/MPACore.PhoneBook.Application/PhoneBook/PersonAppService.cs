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
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using MPACore.PhoneBook.PhoneBook.Dtos;
using MPACore.PhoneBook.PhoneBooks.Persons;
using PhoneNumber = MPACore.PhoneBook.PhoneBooks.Phones.PhoneNumber;

namespace MPACore.PhoneBook.PhoneBook
{
    public class PersonAppService : PhoneBookAppServiceBase, IPersonAppService
    {
        private readonly IRepository<Person> _personRepostRepository;
        private readonly IRepository<PhoneNumber> _phoneRepository;
        public PersonAppService(IRepository<Person> personRepostRepository, IRepository<PhoneNumber> phoneRepository)
        {
            _personRepostRepository = personRepostRepository;
            _phoneRepository = phoneRepository;
        }

        public async Task<PagedResultDto<PersonListDto>> GetPagePersonAsync(GetPersonInput input)
        {
            var query = _personRepostRepository.GetAllIncluding(a => a.PhoneNumbers);
            //var query = _personRepostRepository.GetAll();
            var personCount = await query.CountAsync();
            var persons = await query.OrderBy(input.Sorting).PageBy(input).ToListAsync();
            var dtos = persons.MapTo<List<PersonListDto>>();
            return new PagedResultDto<PersonListDto>(personCount, dtos);
        }

        public async Task<PersonListDto> GetPersonByIdAsync(NullableIdDto input)
        {
            var query = await _personRepostRepository.GetAllIncluding(a => a.PhoneNumbers).FirstOrDefaultAsync(q => q.Id == input.Id.Value);
            return query.MapTo<PersonListDto>();
        }

        public async Task<GetPersonEditOuputDto> GetPersonForEditAsync(NullableIdDto input)
        {
            var output = new GetPersonEditOuputDto();
            PersonEditDto personEditDto = new PersonEditDto();
            if (input.Id.HasValue)
            {
                var entity = await _personRepostRepository.GetAllIncluding(a => a.PhoneNumbers).FirstOrDefaultAsync(q => q.Id == input.Id.Value); ;
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
            var personId = await _personRepostRepository.UpdateAsync(intput.MapTo<Person>());

            if (intput?.PhoneNumbers?.Any() ?? false)
            {
                intput.PhoneNumbers = intput.PhoneNumbers.Select(o =>
                {
                    o.PersonId = personId.Id;
                    o.Id = o.Id > 0 ? o.Id : 0;
                    return o;
                }).ToList();
                await _phoneRepository.InsertOrUpdateAsync(intput.PhoneNumbers.FirstOrDefault().MapTo<PhoneNumber>());
            }

        }

        protected async Task AddPersonAsync(PersonEditDto intput)
        {
            var entity = intput.MapTo<Person>();
            var personId = await _personRepostRepository.InsertAndGetIdAsync(intput.MapTo<Person>());
            if (intput?.PhoneNumbers?.Any() ?? false)
            {
                intput.PhoneNumbers = intput.PhoneNumbers.Select(o =>
                {
                    o.PersonId = personId;
                    o.CreationTime=DateTime.Now;
                    return o;
                }).ToList();
                await _phoneRepository.InsertOrUpdateAsync(intput.PhoneNumbers.FirstOrDefault().MapTo<PhoneNumber>());
            }
        }
    }
}
