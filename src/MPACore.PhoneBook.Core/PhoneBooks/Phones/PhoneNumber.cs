using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using MPACore.PhoneBook.PhoneBooks.Persons;

namespace MPACore.PhoneBook.PhoneBooks.Phones
{
    public class PhoneNumber : Entity<long>,IHasCreationTime
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        public DateTime CreationTime { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        [Required,MaxLength(20)]
        public string  Number { get; set; }
        /// <summary>
        /// /电话类型
        /// </summary>
        public PhoneNumberType Type { get; set; }
        /// <summary>
        /// persion的id
        /// </summary>
        public int PersonId { get; set; }
        /// <summary>
        /// 用户
        /// </summary>
        public Person Person { get; set; }
    }
}
