namespace Application.Exceptions
{
    public class CustomSqlException : ApplicationException
    {
        public CustomSqlException(string message, Exception innerException) : base(message, innerException)
        {

        }

    }
}
