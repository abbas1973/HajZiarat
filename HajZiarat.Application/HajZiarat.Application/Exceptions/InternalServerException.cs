using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Exceptions
{
    public class InternalServerException : ApplicationException
    {
        public InternalServerException(string message) : base(message)
        {

        }
    }
}
