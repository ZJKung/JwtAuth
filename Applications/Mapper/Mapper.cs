using System;
using Applications.ViewModels;
using AutoMapper;
using DAL.Entities;

namespace Applications.Mapper
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            AllowNullDestinationValues = true;
            //source => destination
            CreateMap<LoginRequestModel, User>()
                .ForMember(dto => dto.Username, opt => opt.MapFrom(x => x.Username))
                .ForMember(dto => dto.Password, opt => opt.MapFrom(x => x.Password));
            CreateMap<User, LoginRequestModel>()
                .ForMember(dto => dto.Username, opt => opt.MapFrom(x => x.Username))
                .ForMember(dto => dto.Password, opt => opt.MapFrom(x => x.Password));

            CreateMap<UserCreateModel, User>()
                .ForMember(x => x.Username, y => y.MapFrom(z => z.Name))
                .ForMember(x => x.Password, y => y.MapFrom(z => z.Secret));


        }

    }
}
