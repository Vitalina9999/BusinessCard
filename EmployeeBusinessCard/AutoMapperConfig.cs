using AutoMapper;
using EmployeeBusinessCard.Entities;
using EmployeeBusinessCard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeBusinessCard
{
    public static class AutoMapperConfig
    {
        public static void Init()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Employee, EmployeeViewModel>();
            });
        }
    }
}
