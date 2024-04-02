using AutoMapper;
using CommunityEventPlanner.Data.Data;
using CommunityEventPlanner.Data.Models;
using CommunityEventPlanner.Service.Interface;
using CommunityEventPlanner.Shared.Contract;
using Microsoft.EntityFrameworkCore;

namespace CommunityEventPlanner.Service.MapperProfile
{
    public class CommunityEventProfile : Profile
    {
        public CommunityEventProfile() 
        { 
            CreateMap<CommunityEventCreationRequest, CommunityEvent>().ReverseMap();
        }
    }
}
