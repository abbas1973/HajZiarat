using System;
using System.Collections.Generic;
using System.Text;
using Utilities;

namespace Application.DTOs
{
    /// <summary>
    /// مدل لازم برای نمایش خطا
    /// </summary>
    public class ErrorPageDTO
    {
        #region Constructors
        public ErrorPageDTO()
        {

        }

        public ErrorPageDTO(int errorCode, string title, string error)
        {
            ErrorCode = errorCode;
            Title = title;
            Errors = new List<string>() { error };
        }
        public ErrorPageDTO(int errorCode, string title, List<string> errors)
        {
            ErrorCode = errorCode;
            Title = title;
            Errors = errors;
        }

        public ErrorPageDTO(int errorCode, string title, Exception ex)
        {
            ErrorCode = errorCode;
            Title = title;
            string err = "خطایی رخ داده است!";
            if (ex != null && !string.IsNullOrEmpty(ex.Message) && ex.Message.HasPersianCharacter())
                err = ex.Message;
            Errors = new List<string>() { err };
        }
        #endregion


        #region Properties
        /// <summary>
        /// کد خطا مثل 404، 500 و ...
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// عنوان خطا
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// توضیحات خطا
        /// </summary>
        public List<string> Errors { get; set; }

        #endregion
    }
}
