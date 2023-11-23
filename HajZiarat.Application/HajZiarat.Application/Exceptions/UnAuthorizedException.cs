using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Exceptions
{
    public class UnAuthorizedException : ApplicationException
    {
        public UnAuthorizedException(string message, bool redirectToLogin = true, string retUrl = null) : base(message)
        {
            RedirectToLogin = redirectToLogin;
            RetUrl = retUrl;
        }

        public bool RedirectToLogin { get; set; }
        public string RetUrl { get; set; }
    }
}
