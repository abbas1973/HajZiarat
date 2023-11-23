using Application.DTOs;
using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Redis.Services;
using Services.RedisService;
using Application.Exceptions;

namespace Application.Features.MyEntities
{
    #region Request
    /// <summary>
    /// بازیابی اطلاعات از ردیس و ذخیره در پایگاه داده
    /// </summary>
    public class TransferDataCommand
    : IRequest<BaseResult<string>>
    {
        #region Constructors
        public TransferDataCommand()
        {

        }
        #endregion
    }
    #endregion



    #region Handler
    public class TransferDataCommandHandler : IRequestHandler<TransferDataCommand, BaseResult<string>>
    {
        private readonly IRedisManager _redis;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public TransferDataCommandHandler(
            IRedisManager redis,
            IUnitOfWork uow,
            IMapper mapper)
        {
            _redis = redis;
            _uow = uow;
            _mapper = mapper;
        }


        public async Task<BaseResult<string>> Handle(TransferDataCommand request, CancellationToken cancellationToken)
        {
            #region بازیابی از ردیس
            var data = await _redis.db.GetEntities();
            if (data == null)
                throw new InternalServerException("خواندن اطلاعات از ردیس با خطا همراه بوده است!");

            if (!data.Any())
                throw new NotFoundException("داده ای جهت انتقال یافت نشد!");
            #endregion

            #region ذخیره در دیتابیس
            var model = _mapper.Map<List<MyEntity>>(data);
            await _uow.MyEntities.BulkInsertAsync(model);
            #endregion

            return new BaseResult<string>(true, "تعداد " + model.Count() + " رکورد با موفقیت در پایگاه داده ذخیره شد.");
        }
    }

    #endregion
}
