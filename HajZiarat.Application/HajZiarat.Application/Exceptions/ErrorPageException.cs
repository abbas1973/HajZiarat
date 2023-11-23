using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Exceptions
{
    /// <summary>
    /// اکسپشن اختصاصی که فقط برای صفحه خطا پرتاب میشود
    /// </summary>
    public class ErrorPageException : Exception
    {
        public ErrorPageException(
            string title,
            int errorCode, 
            List<string> errors,
            bool redirectToLogin = false, 
            string retUrl = null, 
            bool redirectConfirmMobile = false) : base(string.Join(" | ", errors))
        {
            Title = title;
            ErrorCode = errorCode;
            Errors = errors;
            RedirectToLogin = redirectToLogin;
            RetUrl = retUrl;
            RedirectConfirmMobile = redirectConfirmMobile;
        }

        /// <summary>
        /// کاربر به صفحه لاگین ریدایرکت شود؟
        /// </summary>
        public bool RedirectToLogin { get; set; }
        /// <summary>
        /// اگر به صفحه لاگین منتقل شد آدرسبرگشت چیست؟
        /// </summary>
        public string RetUrl { get; set; }

        /// <summary>
        /// کاربر به صفحه تایید موبایل ریدایرکت شود
        /// </summary>
        public bool RedirectConfirmMobile { get; set; }
        public string Title { get; set; }
        public int ErrorCode { get; set; }
        public List<string> Errors { get; set; }
    }
}
