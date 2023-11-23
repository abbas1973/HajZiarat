using Utilities;

namespace Application.DTOs
{
    public class BaseResult
    {
        /// <summary>
        /// وضعیت عملیات
        /// <para>
        /// true = موفق
        /// </para>
        /// <para>
        /// false = ناموفق
        /// </para>
        /// </summary>
        public bool IsSuccess { get; set; }


        /// <summary>
        /// پیغام های خطا در صورت ناموفق بودن عملیات
        /// </summary>
        public List<string> Errors { get; set; }

        /// <summary>
        /// متن های خطا بصورت یکجا
        /// </summary>
        public string Message => Errors == null ? null : string.Join(" | ", Errors);

        #region Constructors
        public BaseResult(bool status)
        {
            IsSuccess = status;
            Errors = new List<string>();
        }

        public BaseResult(bool status , List<string> errors = null)
        {
            IsSuccess = status;
            Errors = errors ?? new List<string>();
        }
        public BaseResult(bool status, string error = null)
        {
            IsSuccess = status;
            Errors = new List<string>();
            if (!string.IsNullOrEmpty(error))
                Errors.Add(error);
        }
        public BaseResult(Exception ex)
        {
            IsSuccess = false;
            var msg = "خطایی رخ داده است!";
            if (ex != null && !string.IsNullOrEmpty(ex.Message) && !ex.Message.IsEnglishText())
                msg = ex.Message;
            Errors = new List<string>() { msg };
        }
        public BaseResult()
        {
            Errors = new List<string>();
        } 
        #endregion
    }


    public class BaseResult<T> : BaseResult
    {        
        /// <summary>
        /// داده بازگشتی در صورت موفق بودن عملیات
        /// </summary>
        public T Value { get; set; }



        #region Constructors
        public BaseResult(bool status, List<string> errors = null, T value = default(T))
        {
            IsSuccess = status;
            Errors = errors ?? new List<string>();
            Value = value;
        }
        public BaseResult(bool status, string error = null, T value = default(T))
        {
            IsSuccess = status;
            Errors = new List<string>();
            if (!string.IsNullOrEmpty(error))
                Errors.Add(error);
            Value = value;
        }
        public BaseResult(T value = default(T))
        {
            IsSuccess = true;
            Errors = new List<string>();
            Value = value;
        }
        public BaseResult(Exception ex)
        {
            IsSuccess = false;
            Value = default(T);

            var msg = "خطایی رخ داده است!";
            if (ex != null && !string.IsNullOrEmpty(ex.Message) && !ex.Message.IsEnglishText())
                msg = ex.Message;
            Errors = new List<string>() { msg };
        }
        public BaseResult()
        {
            Errors = new List<string>();
        }
        #endregion
    }




}
