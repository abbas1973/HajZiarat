using System;

namespace Domain.Entities
{
    public interface IBaseEntity
    {
        long Id { get; set; }


        #region مشخصات ایجاد
        DateTime CreateDate { get; set; }
        #endregion


        #region مشخصات ویرایش
         DateTime? ModifyDate { get; set; }
        #endregion
    }
}
