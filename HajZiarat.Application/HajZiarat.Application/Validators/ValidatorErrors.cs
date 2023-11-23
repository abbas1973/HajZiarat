using Resources;

namespace Application.Validators
{
    public static class ValidatorErrors
    {
        public static string English => string.Format(Messages.ErrorEnglish, "{PropertyName}");
        public static string Format => string.Format(Messages.ErrorRequired, "{PropertyName}");
        public static string FileIsImage => string.Format(Messages.ErrorFileIsImage, "{PropertyName}");

        /// <summary>
        /// ارور سایز فایل
        /// </summary>
        /// <param name="fileSize">برحسب مگابایت</param>
        /// <returns></returns>
        public static string FileSize(float fileSize) => string.Format(Messages.ErrorFileSize, "{PropertyName}", fileSize);
        public static string GreaterThan => string.Format(Messages.ErrorGreaterThan, "{PropertyName}", "{ComparisonValue}");
        public static string GreaterThanOrEqual => string.Format(Messages.ErrorGreaterThanOrEqual, "{PropertyName}", "{ComparisonValue}");
        public static string Length => string.Format(Messages.ErrorLength, "{PropertyName}", "{ComparisonValue}");
        public static string LessThan => string.Format(Messages.ErrorLessThan, "{PropertyName}", "{ComparisonValue}");
        public static string LessThanOrEqual => string.Format(Messages.ErrorLessThanOrEqual, "{PropertyName}", "{ComparisonValue}");
        public static string MaxLength => string.Format(Messages.ErrorMaxLength, "{PropertyName}", "{MaxLength}");
        public static string MinLength => string.Format(Messages.ErrorMinLength, "{PropertyName}", "{MinLength}");
        public static string Number => string.Format(Messages.ErrorNumber, "{PropertyName}");
        public static string Password => string.Format(Messages.ErrorPassword, "{PropertyName}");
        public static string Persian => string.Format(Messages.ErrorPersian, "{PropertyName}");
        public static string Repeated => string.Format(Messages.ErrorRepeated, "{PropertyName}");
        public static string Required => string.Format(Messages.ErrorRequired, "{PropertyName}");
        public static string Username => string.Format(Messages.ErrorUsername, "{PropertyName}");
    }
}
