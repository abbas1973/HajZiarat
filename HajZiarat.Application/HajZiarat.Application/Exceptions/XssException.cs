using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Exceptions
{
    public class XssException : ApplicationException
    {
        public XssException(string message) : base(message)
        {

        }
    }
}
