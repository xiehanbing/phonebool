using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MPACore.PhoneBook.PhoneBooks.PhoneNumbers
{
    public  enum PhoneType
    {
        [Description("手机")]
        Mobile,
        [Description("座机")]
        Home,
        [Description("公司")]
        Company
    }
}
