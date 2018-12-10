using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using MPACore.PhoneBook.PhoneBooks.Phones;

namespace MPACore.PhoneBook.PhoneBooks.Persons
{
    /// <summary>
    /// 用户类
    /// </summary>
    //[Table("Person")]
    public class Person : FullAuditedEntity
    {
        /// <summary>
        /// 姓名
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [EmailAddress]
        [MaxLength(80)]
        public string Email { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [MaxLength(200)]
        public string Address { get; set; }
        /// <summary>
        /// 电话号码属性
        /// </summary>
        public ICollection<PhoneNumber> PhoneNumbers { get; set; }
    }
}
