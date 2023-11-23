using Application.DTOs;
using Microsoft.AspNetCore.Http;
using System.Data;

namespace Etehadie.Application.Contracts
{
    public interface IExcelReportGenerator<T> where T : class
	{
        #region گرفتن خروجی دیتاتیبل از فایل اکسل بر اساس پروپرتی های تایپ  مشخص شده
        /// <summary>
        /// گرفتن خروجی دیتاتیبل از فایل اکسل بر اساس پروپرتی های تایپ  مشخص شده
        /// </summary>
        /// <returns></returns>
        BaseResult<DataTable> ImportExceltoDatatable(IFormFile file); 
        #endregion

    }
}
