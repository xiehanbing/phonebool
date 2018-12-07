using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Abp.Domain.Entities.Auditing;

namespace MPACore.PhoneBook.PhoneBooks.Persons
{
    /// <summary>
    /// 联系人
    /// </summary>
    [Table("Person")]
    public class Person : FullAuditedEntity
    {
        /// <summary>
        /// 联系人名称
        /// </summary>
        [Required,MaxLength(20)]
        public string  Name { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [EmailAddress, MaxLength(80)]
        public string  Email { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [MaxLength(200)]
        public string  Address { get; set; }


    }
}
