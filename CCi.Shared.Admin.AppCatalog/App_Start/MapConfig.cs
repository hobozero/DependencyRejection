using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApplicationCatalog;
using AutoMapper;
using CCi.Shared.Admin.AppCatalog.Models;

namespace CCi.Shared.Admin.AppCatalog
{
    public static class MapConfig
    {
        public static void Config()
        {
            /*

                Assembly: null
                AutomatedDeploy: false
                BusinessArea: "BusArea"
                DeployedType: Web
                Description: "Description"
                OutputType: Library
                Path: null
                ProjectId: {00000000-0000-0000-0000-000000000000}
                ProjectType: null
                SolarWinds: Available
                UniqueId: null
                VCS: null
                */


            Mapper.Initialize(cfg => 
                cfg.CreateMap<DeployedApplication, ApplicationDto>()
                .ForMember(dest => dest.Assembly, opt => opt.MapFrom(src => src.ProjectFile.AssemblyName))
                .ForMember(dest => dest.OutputType, opt => opt.MapFrom(src => src.ProjectFile.OutputType))
                .ForMember(dest => dest.Path, opt => opt.MapFrom(src => src.ProjectFile.FilePath))
                .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.ProjectFile.ProjectId))
                .ForMember(dest => dest.ProjectType, opt => opt.MapFrom(src => src.ProjectFile.ProjectType))
                .ForMember(dest => dest.UniqueId, opt => opt.MapFrom(src => src.OctoName))
                .ForMember(dest => dest.VCS, opt => opt.MapFrom(src => src.ProjectFile.VcsInfo))
            );
            
        }
    }
}