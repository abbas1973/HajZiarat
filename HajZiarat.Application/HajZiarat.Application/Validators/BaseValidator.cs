using System.Net.Mail;
using Utilities;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Utilities.Files;
using IraniValidator;
using Microsoft.IdentityModel.Tokens;

namespace Application.Validators
{
    public static class BaseValidator
    {
        #region ولیدیت کردن موبایل بصورت 09111111111
        /// <summary>
        /// ولیدیت کردن موبایل بصورت 09111111111
        /// </summary>
        /// <param name="mobile">تلفن</param>
        /// <returns></returns>
        public static bool BeAValidMobile(string mobile)
        {
            if (string.IsNullOrEmpty(mobile))
                return true;

            if (mobile.Length != 11)
                return false;
            var regex = new Regex(@"^09\d{9}");
            return regex.IsMatch(mobile);
        }
        #endregion


        #region ولیدیت کردن تلفن 01111111111
        /// <summary>
        /// ولیدیت کردن تلفن بصورت 01111111111
        /// </summary>
        /// <param name="tel">تلفن</param>
        /// <returns></returns>
        public static bool BeAValidTel(string tel)
        {
            if (string.IsNullOrEmpty(tel))
                return true;

            if (tel.Length != 11)
                return false;
            var regex = new Regex(@"^0\d{10}");
            return regex.IsMatch(tel);
        }
        #endregion



        #region ولیدیت کردن ایمیل
        /// <summary>
        /// ولیدیت کردن ایمیل
        /// </summary>
        /// <param name="email">ایمیل</param>
        /// <returns></returns>
        public static bool BeAValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return true;

            try
            {
                MailAddress m = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        #endregion



        #region ولیدیت کردن آیپی
        /// <summary>
        /// ولیدیت کردن آیپی
        /// </summary>
        /// <param name="ip">آیپی</param>
        /// <returns></returns>
        public static bool BeAValidIp(string ip)
        {
            if (string.IsNullOrEmpty(ip))
                return true;

            if (ip.Length > 30)
                return false;
            var regex = new Regex(@"(http|https):\/\/\b(?:(?:25[0-5]|2[0-4]\d|[01]?\d\d?)\.){3}(?:25[0-5]|2[0-4]\d|[01]?\d\d?)(?::\d{0,4})?\b");
            var isValid = regex.IsMatch(ip);
            return isValid;
        }
        #endregion



        #region ولیدیت کردن نام
        /// <summary>
        /// ولیدیت کردن نام
        /// </summary>
        /// <param name="name">نام</param>
        /// <returns></returns>
        public static bool BeAValidName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return true;

            if (name.Length == 0)
                return false;

            if(!name.IsPersianCharacter())
                return false;

            return true;
        }
        #endregion



        #region ولیدیت کردن کد ملی بصورت 2161793128
        /// <summary>
        /// ولیدیت کردن کد ملی بصورت 2161793128
        /// </summary>
        /// <param name="nationalCode">کد ملی</param>
        /// <returns></returns>
        public static bool BeAValidNationalCode(string nationalCode)
        {
            if (string.IsNullOrEmpty(nationalCode))
                return true;

            var regex = new Regex(@"^[0-9]{10}");
            if (regex.IsMatch(nationalCode) == true)
            {
                char[] chArray = nationalCode.ToCharArray();
                int[] numArray = new int[chArray.Length];
                for (int i = 0; i < chArray.Length; i++)
                {
                    numArray[i] = (int)char.GetNumericValue(chArray[i]);
                }
                int num2 = numArray[9];
                int num3 = numArray[0] * 10 + numArray[1] * 9 + numArray[2] * 8 + numArray[3] * 7 + numArray[4] * 6 + numArray[5] * 5 + numArray[6] * 4 + numArray[7] * 3 + numArray[8] * 2;
                int num4 = num3 - num3 / 11 * 11;
                bool isValid;
                if (num4 == 0 && num2 == num4 || num4 == 1 && num2 == 1 || num4 > 1 && num2 == Math.Abs(num4 - 11))
                {
                    isValid = true;
                }
                else
                {
                    isValid = false;
                }
                switch (nationalCode)
                {
                    case "0000000000":
                    case "1111111111":
                    case "22222222222":
                    case "33333333333":
                    case "4444444444":
                    case "5555555555":
                    case "6666666666":
                    case "7777777777":
                    case "8888888888":
                    case "9999999999":
                    case "0123456789":
                    case "9876543210":
                        isValid = false;
                        break;
                }

                return isValid;
            }

            return false;
        }
        #endregion



        #region ولیدیت کردن کد پستی ده رقمی
        /// <summary>
        /// ولیدیت کردن کد پستی ده رقمی
        /// </summary>
        /// <param name="postalCode"></param>
        /// <returns></returns>
        public static bool BeAValidPostalCode(string postalCode)
        {
            if (string.IsNullOrEmpty(postalCode))
                return true;

            if (postalCode.Length != 10)
                return false;
            var regex = new Regex(@"^[0-9]{10}");
            return regex.IsMatch(postalCode);
        }
        #endregion




        #region ولیدیت کردن عرض جغرافیایی - latitude
        /// <summary>
        /// ولیدیت کردن عرض جغرافیایی - latitude
        /// </summary>
        /// <param name="postalCode"></param>
        /// <returns></returns>
        public static bool BeAValidLatitude(double? latitude)
        {
            if (latitude == null)
                return true;

            return latitude > -90 && latitude < 90;
        }


        /// <summary>
        /// ولیدیت کردن عرض جغرافیایی - latitude
        /// </summary>
        /// <param name="postalCode"></param>
        /// <returns></returns>
        public static bool BeAValidLatitude(double latitude)
        {
            return latitude > -90 && latitude < 90;
        }
        #endregion




        #region ولیدیت کردن طول جغرافیایی - longitude
        /// <summary>
        /// ولیدیت کردن طول جغرافیایی - longitude
        /// </summary>
        /// <param name="postalCode"></param>
        /// <returns></returns>
        public static bool BeAValidLongitude(double? longitude)
        {
            if (longitude == null)
                return true;

            return longitude > -180 && longitude < 180;
        }


        /// <summary>
        /// ولیدیت کردن طول جغرافیایی - longitude
        /// </summary>
        /// <param name="postalCode"></param>
        /// <returns></returns>
        public static bool BeAValidLongitude(double longitude)
        {
            return longitude > -180 && longitude < 180;
        }
        #endregion



        #region ولیدیت کردن نام کاربری
        /// <summary>
        /// ولیدیت کردن نام کاربری
        /// شامل حروف انگلیسی اعداد و کاراکتر های _ - .
        /// </summary>
        /// <param name="username">نام کاربری</param>
        /// <returns></returns>
        public static bool BeAValidUsername(string username)
        {
            if (string.IsNullOrEmpty(username))
                return true;

            var regex = new Regex(@"^[A-Za-z0-9._-]{5,30}$");
            return regex.IsMatch(username);
        }
        #endregion




        #region ولیدیت کردن کلمه عبور
        /// <summary>
        /// ولیدیت کردن کلمه عبور
        /// شامل حروف انگلیسی، اعداد و کاراکتر های خاص
        /// </summary>
        /// <param name="password">کلمه عبور</param>
        /// <returns></returns>
        public static bool BeAValidPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                return true;

            //var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,150}$");
            var regex = new Regex(@"^(?=.*[a-zA-Z])(?=.*\d).{6,150}$");
            return regex.IsMatch(password);
        }
        #endregion


        #region ولیدیت کردن تصویر
        /// <summary>
        /// ولیدیت کردن تصویر
        /// </summary>
        /// <returns></returns>
        public static bool BeAValidImage(IFormFile pic)
        {
            if (pic == null)
                return true;

            return pic.IsImage() && pic.IsValidFile();
        }
        #endregion



        #region ولیدیت کردن اکسل
        /// <summary>
        /// ولیدیت کردن تصویر
        /// </summary>
        /// <returns></returns>
        public static bool BeAValidExcel(IFormFile file)
        {
            if (file == null)
                return true;

            return file.IsExcel();
        }
        #endregion



        #region ولیدیت کردن شناسه ملی شرکت ها
        /// <summary>
        /// ولیدیت کردن شناسه ملی شرکت ها
        /// </summary>
        /// <returns></returns>
        public static bool BeAValidNationalId(string nationalId)
        {
            if (string.IsNullOrEmpty(nationalId))
                return true;

            return nationalId.IsValidCompanyNationalId();
        }
        #endregion


        #region ولیدیت کردن شماره شبا 24 رقمی
        /// <summary>
        /// ولیدیت کردن شماره شبا 24 رقمی
        /// </summary>
        /// <param name="postalCode"></param>
        /// <returns></returns>
        public static bool BeAValidSheba(string sheba)
        {
            if (string.IsNullOrEmpty(sheba))
                return true;

            if (sheba.Length != 24)
                return false;
            var regex = new Regex(@"^[0-9]{24}");
            return regex.IsMatch(sheba);
        }
        #endregion


        #region ولیدیت کردن شماره حساب 12 تا 16 رقمی
        /// <summary>
        /// ولیدیت کردن شماره حساب 12 تا 16 رقمی
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <returns></returns>
        public static bool BeAValidAccountNumber(string accountNumber)
        {
            if (string.IsNullOrEmpty(accountNumber))
                return true;

            if (accountNumber.Length < 12 || accountNumber.Length > 16)
                return false;
            var regex = new Regex(@"^[0-9]{12,16}");
            return regex.IsMatch(accountNumber);
        }
        #endregion
    }
}
