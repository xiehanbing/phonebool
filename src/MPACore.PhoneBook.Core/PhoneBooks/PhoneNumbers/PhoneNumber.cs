using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using MPACore.PhoneBook.PhoneBooks.Persons;

namespace MPACore.PhoneBook.PhoneBooks.PhoneNumbers
{
   public  class PhoneNumber:Entity<long>,IHasCreationTime
    {
        /// <summary>
        /// 电话
        /// </summary>
        public string  Number { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime  CreationTime { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public PhoneType Type { get; set; }
        /// <summary>
        /// 联系人id
        /// </summary>
        public int PersonId { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public Person Person { get; set; }
    }
}
