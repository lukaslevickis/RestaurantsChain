using System;
using API.DAL.Entities;
using API.Dtos;
using AutoMapper;

namespace API.Profiles
{
    public class MainProfile : Profile
    {
        public MainProfile()
        {
            CreateMap<Restaurant, RestaurantDto>();
        }
    }
}
