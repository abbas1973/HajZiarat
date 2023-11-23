using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public abstract class BaseEntity : IBaseEntity
    {
        #region Constructors
        public BaseEntity()
        {
            CreateDate = DateTime.Now;
        }
        #endregion


        #region Properties
        public long Id { get; set; }


        #region مشخصات ایجاد
        public DateTime CreateDate { get; set; }
        #endregion


        #region مشخصات ویرایش
        public DateTime? ModifyDate { get; set; }
        #endregion 
        #endregion

    }
}
