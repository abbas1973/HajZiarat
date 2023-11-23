using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Exceptions
{
    public class FileException : ApplicationException
    {
        public FileException(string message) : base(message)
        {

        }
    }
}
