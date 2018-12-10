using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using MPACore.PhoneBook.PhoneBooks.Persons;
using MPACore.PhoneBook.PhoneBooks.Phones;

namespace MPACore.PhoneBook.PhoneBook.Dtos
{
    [AutoMapFrom(typeof(Person))]
    public class PersonListDto : FullAuditedEntityDto
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        public List<PhoneNumber> PhoneNumbers { get; set; }
    }
}
