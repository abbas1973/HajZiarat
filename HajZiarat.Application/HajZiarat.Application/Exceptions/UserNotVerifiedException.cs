using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Exceptions
{
    public class UserNotVerifiedException : ApplicationException
    {
        public UserNotVerifiedException() : base("تلفن همراه تایید نشده است!")
        {

        }
    }
}
