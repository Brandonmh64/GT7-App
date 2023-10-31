using AutoMapper;
using GranTurismoFramework;
using GranTurismoFramework.DataTransfer.Simple;
using GranTurismoLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GranTurismoLibrary.Helpers
{
    public static class GtMapper
    {
        private static AutoMapper.Mapper _mapper { get; set; }


        static GtMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<GranTurismoFramework.DataTransfer.OwnedCarInfoDto, OwnedCarInfo>()
                    .ForMember(dest => dest.CarId, opt => opt.MapFrom(src => src.Car.CarId))
                    .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Car.FullName))
                    .ForMember(dest => dest.ImageName, opt => opt.MapFrom(src => src.ImageName))

                    .ForMember(dest => dest.ManufacturerId, opt => opt.MapFrom(src => src.Manufacturer.ManufacturerId))
                    .ForMember(dest => dest.ManufacturerName, opt => opt.MapFrom(src => src.Manufacturer.Name))
                    .ForMember(dest => dest.RegionId, opt => opt.MapFrom(src => src.Region.RegionId))
                    .ForMember(dest => dest.RegionName, opt => opt.MapFrom(src => src.Region.Name))
                    .ForMember(dest => dest.PrimaryDriverId, opt => opt.MapFrom(src => src.PrimaryDriverId))
                    .ForMember(dest => dest.PrimaryDriverName, opt => opt.MapFrom(src => src.PrimaryDriverName));

                cfg.CreateMap<OwnedCarInfo, GranTurismoFramework.DataTransfer.OwnedCarInfoDto>()
                    .ForMember(dest => dest.Car, opt => opt.MapFrom(src => new CarDto()
                    {
                        CarId = src.CarId,
                        FullName = src.FullName,
                        ManufacturerId = src.ManufacturerId,
                    }))
                    .ForMember(dest => dest.Manufacturer, opt => opt.MapFrom(src => new ManufacturerDto()
                    {
                        ManufacturerId = src.ManufacturerId,
                        Name = src.ManufacturerName,
                        RegionId = src.RegionId,
                    }))
                    .ForMember(dest => dest.Region, opt => opt.MapFrom(src => new RegionDto()
                    {
                        RegionId = src.RegionId,
                        Name = src.RegionName
                    }));


                cfg.CreateMap<TireType, TireTypeDto>();
                cfg.CreateMap<TireTypeDto, TireType>();

                cfg.CreateMap<TuneDto, TuneInfo>();
                cfg.CreateMap<TuneInfo, TuneDto>();
            });

            _mapper = new Mapper(config);
        }


        public static TDest Map<TSource, TDest>(TSource source)
        {
            return _mapper.Map<TDest>(source);
        }

        public static List<TDest> MapList<TSource, TDest>(List<TSource> sourceList)
        {
            var mappedList = new List<TDest>();

            foreach (var source in sourceList)
            {
                var mapped = _mapper.Map<TSource, TDest>(source);
                mappedList.Add(mapped);
            }

            return mappedList;
        }
    }
}
