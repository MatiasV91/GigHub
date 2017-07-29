using AutoMapper;
using GigHub.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.Models
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<ApplicationUser, UserDto>();
            CreateMap<Gig, GigDto>();
            CreateMap<Notification, NotificationDto>();
        }
    }
}