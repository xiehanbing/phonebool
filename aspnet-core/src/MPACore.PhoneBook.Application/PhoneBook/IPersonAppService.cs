using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MPACore.PhoneBook.PhoneBook.Dtos;

namespace MPACore.PhoneBook.PhoneBook
{
    public interface IPersonAppService : IApplicationService
    {
        /// <summary>
        /// 获取人的x信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<PersonListDto>> GetPagePersonAsync(GetPersonInput input);
        /// <summary>
        /// 获取个人信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PersonListDto> GetPersonByIdAsync(NullableIdDto input);

        Task<GetPersonEditOuputDto> GetPersonForEditAsync(NullableIdDto input);
        /// <summary>
        /// 添加或更新
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task AddOrUpdatePersonAsync(AddOrUpdatePersonDto input);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeletePersonAsync(int id);
    }
}
