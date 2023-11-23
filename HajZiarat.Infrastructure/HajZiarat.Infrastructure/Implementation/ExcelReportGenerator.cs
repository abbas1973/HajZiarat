using Application.DTOs;
using ClosedXML.Excel;
using Etehadie.Application.Contracts;
using Microsoft.AspNetCore.Http;
using System.Data;
using System.Reflection;

namespace Infrastructure.Implementation
{
    /// <summary>
    /// کار با فایل اکسل
    /// </summary>
    public class ExcelReportGenerator<T> : IExcelReportGenerator<T> where T : class
    {

        #region گرفتن خروجی دیتاتیبل از فایل اکسل بر اساس پروپرتی های تایپ  مشخص شده
        /// <summary>
        /// گرفتن خروجی دیتاتیبل از فایل اکسل بر اساس پروپرتی های تایپ  مشخص شده
        /// </summary>
        /// <returns></returns>
        public BaseResult<DataTable> ImportExceltoDatatable(IFormFile file)
        {
            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);

                // Open the Excel file using ClosedXML.
                using (XLWorkbook workBook = new XLWorkbook(stream))
                {
                    //Read the first Sheet from Excel file.
                    IXLWorksheet workSheet = workBook.Worksheet(1);

                    //Create a new DataTable.
                    DataTable dt = new DataTable();

                    //Get all the properties
                    PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                    foreach (PropertyInfo prop in Props)
                    {
                        //Defining type of data column gives proper data table 
                        var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                        //Setting column names as Property names
                        string coulmnName = prop.Name;
                        dt.Columns.Add(coulmnName);
                    }

                    if (workSheet.ColumnsUsed().Count() > dt.Columns.Count)
                        return new BaseResult<DataTable>(false, "فایل اکسل باید حاوی تنها " + dt.Columns.Count + " ستون داده باشد!");

                    //Loop through the Worksheet rows.
                    var rows = workSheet.RowsUsed();
                    foreach (IXLRow row in rows)
                    {
                        //Add rows to DataTable.
                        dt.Rows.Add();
                        int i = 0;

                        foreach (IXLCell cell in row.Cells(row.FirstCellUsed().Address.ColumnNumber, row.LastCellUsed().Address.ColumnNumber))
                        {
                            string val = null;
                            if (cell.HasFormula)
                                val = cell.CachedValue?.ToString();
                            else
                                val = cell.Value?.ToString();

                            dt.Rows[dt.Rows.Count - 1][i] = string.IsNullOrEmpty(val) ? null : val;// cell.GetValue<type>();
                            i++;
                        }
                    }

                    return new BaseResult<DataTable>(dt);
                }
            }
        } 
        #endregion


    }


}
