using Appets.DataAccess.Entities;
using Appets.WebUI.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appets.WebUI.Extensions
{
    public class MappingProfileExtensions : Profile
    {
        public MappingProfileExtensions()
        {
            CreateMap<tbRoles, RoleViewModel>().ReverseMap();
        }
    }
}
