namespace Redis.DTOs
{
    /// <summary>
    ///  مدل لازم برای ارسال پیامک در بکگراند
    /// </summary>
    public class MyEntityRedisDTO
    {
        #region Constructors
        public MyEntityRedisDTO()
        {
            
        }

        public MyEntityRedisDTO(string title)
        {
            Title = title;
        }
        #endregion


        #region Properties
        public string Title { get; set; }
        #endregion

    }
}
