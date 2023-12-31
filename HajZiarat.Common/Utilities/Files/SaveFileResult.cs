﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities.Files
{
    public class SaveFileResult
    {
        /// <summary>
        /// وضعیت عملیات
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// پیغام مناسب بخصوص در هنگام خطا
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// آبجکتی که در خروجی برمیگردد
        /// </summary>
        public string FileName { get; set; }


        /// <summary>
        /// مسیر کامل ذخیره سازی فایل شامل آدرس و نام فایل
        /// </summary>
        public string FullPath { get; set; }

        public SaveFileResult()
        {

        }

        public SaveFileResult(bool _status, string _message, string _fileName = null, string fullPath = null)
        {
            Status = _status;
            Message = _message;
            FileName = _fileName;
            FullPath = fullPath;
        }
    }

}
