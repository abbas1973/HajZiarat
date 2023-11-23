using Application.DTOs;
using Application.Repositories;
using AutoMapper;
using Base.Application.Mapping;
using Domain.Entities;
using DTO.MyEntity;
using Etehadie.Application.Contracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Redis.Services;
using StackExchange.Redis;
using System.ComponentModel.DataAnnotations;
using Services.RedisService;
using Redis.DTOs;

namespace Application.Features.MyEntities
{
    #region Request
    public class ImportExcelCommand
    : IRequest<BaseResult<string>>, IMapFrom<MyEntity>
    {
        #region Constructors
        public ImportExcelCommand()
        {

        }
        public ImportExcelCommand(IFormFile file)
        {
            ExcelFile = file;
        }
        #endregion


        #region Properties
        [Display(Name = "فایل")]
        public IFormFile ExcelFile { get; set; }
        #endregion



        #region Mapping
        public void Mapping(Profile profile) =>
            profile.CreateMap<ImportExcelCommand, MyEntity>();
        #endregion
    }
    #endregion



    #region Handler
    public class ImportExcelCommandHandler : IRequestHandler<ImportExcelCommand, BaseResult<string>>
    {
        private readonly IExcelReportGenerator<MyEntityExcelDTO> _excelHelper;
        private readonly IRedisManager _redis;
        public ImportExcelCommandHandler(
            IExcelReportGenerator<MyEntityExcelDTO> excelHelper,
            IRedisManager redis)
        {
            _redis = redis;
            _excelHelper = excelHelper;
        }


        public async Task<BaseResult<string>> Handle(ImportExcelCommand request, CancellationToken cancellationToken)
        {
            #region تبدیل داده درون اکسل به لیستی از مدل مورد نظر
            var res = _excelHelper.ImportExceltoDatatable(request.ExcelFile);
            if (!res.IsSuccess)
                return new BaseResult<string>(false, res.Errors);

            var data = res.Value.Select("NOT(Title IS NULL OR Title='')")
            .Select(dr => new MyEntityRedisDTO
            {
                Title = dr.IsNull("Title") ? null : Convert.ToString(dr["Title"])
            }).ToList();
            #endregion


            #region ذخیره داده در ردیس
            var insertedCount = await _redis.db.SetEntities(data);
            if(insertedCount < 0)
                return new BaseResult<string>(false, "درج اطلاعات در ردیس با خطا همراه بوده است!");
            #endregion

            #region پیغام خروجی
            var totalCount = data.Count();
            string msg = $"تعداد {insertedCount} رکورد از مجموع {totalCount} رکورد با موفقیت در ردیس ذخیره شد.";
            if (totalCount > insertedCount)
                msg += $" {totalCount - insertedCount} رکورد تکراری بوده است!";
            return new BaseResult<string>(msg);
            #endregion
        }
    }

    #endregion
}
