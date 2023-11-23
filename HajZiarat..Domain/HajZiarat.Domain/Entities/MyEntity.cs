namespace Domain.Entities
{
    /// <summary>
    /// موجودیت مورد نظر جهت ذخیره سازی دیتا
    /// </summary>
    public class MyEntity : BaseEntity
    {

        #region Constructors
        public MyEntity() : base()
        {
        }
        #endregion


        #region Properties
        /// <summary>
        /// دیتای خوانده شده از اکسل
        /// </summary>
        public string Title { get; set; } 
        #endregion

    }
}
