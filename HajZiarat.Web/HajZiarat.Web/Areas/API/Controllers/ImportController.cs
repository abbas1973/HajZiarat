using Application.DTOs;
using Application.Features.MyEntities;
using Asp.Versioning;
using HajZiarat.Web.Controllers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace HajZiarat.Web.Areas.API.Controllers
{
    /// <summary>
    /// ایمپورت دیتا با فایل اکسل
    /// </summary>
    public class ImportController : BaseApiController
    {

        #region آپلود فایل اکسل و ذخیره در ردیس
        /// <summary>
        /// آپلود فایل اکسل و ذخیره در ردیس
        /// </summary>
        /// <param name="excel">
        /// فایل اکسل با پسوند xlsx یا xls
        /// </param>
        /// <returns></returns>
        [Route("[action]")]
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BaseResult<string>), 200)]
        public async Task<ActionResult<string>> FileToRedis(IFormFile excel)
        {
            var res = await Mediator.Send(new ImportExcelCommand(excel));
            return Ok(res);
        }
        #endregion



        #region انتقال دیتا از ردیس به sql
        /// <summary>
        /// انتقال دیتا از ردیس به sql
        /// </summary>
        /// <returns></returns>
        [Route("[action]")]
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BaseResult<string>), 200)]
        public async Task<ActionResult<string>> RedisToSql()
        {
            var res = await Mediator.Send(new TransferDataCommand());
            return Ok(res);
        } 
        #endregion
    }
}
