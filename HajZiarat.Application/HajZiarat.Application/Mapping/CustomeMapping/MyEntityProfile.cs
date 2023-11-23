using AutoMapper;
using Domain.Entities;
using Redis.DTOs;

namespace Application.Mapping
{
    public class MyEntityProfile : Profile
    {
        public MyEntityProfile()
        {
            CreateMap<MyEntityRedisDTO, MyEntity>();
        }
    }
}
