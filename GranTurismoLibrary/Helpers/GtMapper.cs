using AutoMapper;
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
                cfg.CreateMap<GranTurismoFramework.DataTransfer.CarInfo, CarInfo>()
                    .ForMember(dest => dest.CarId, opt => opt.MapFrom(src => src.Car.CarId))
                    .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Car.FullName))
                    .ForMember(dest => dest.ImageName, opt => opt.MapFrom(src => src.Car.ImageName))

                    .ForMember(dest => dest.ManufacturerId, opt => opt.MapFrom(src => src.Manufacturer.ManufacturerId))
                    .ForMember(dest => dest.ManufacturerName, opt => opt.MapFrom(src => src.Manufacturer.Name))
                    .ForMember(dest => dest.RegionId, opt => opt.MapFrom(src => src.Region.RegionId))
                    .ForMember(dest => dest.RegionName, opt => opt.MapFrom(src => src.Region.Name));
            });

            _mapper = new Mapper(config);
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
