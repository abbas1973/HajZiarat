using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Exceptions
{
    public class ExternalServiceException : ApplicationException
    {
        /// <summary>
        /// خطای فراخوانی سرویس بیرونی
        /// </summary>
        /// <param name="message">پیغام خطا</param>
        /// <param name="serviceName">نام سرویس فراخوانی شده</param>
        /// <param name="func">نام فانکشن فراخوانی شده</param>
        public ExternalServiceException(string message, string serviceName, string func) : base(message)
        {
            ServiceName = serviceName;
            Function = func;
        }


        public string ServiceName { get; set; }
        public string Function { get; set; }
    }
}
