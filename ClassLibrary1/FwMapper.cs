using AutoMapper;
using GranTurismoFramework.DataTransfer.Simple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GranTurismoFramework
{
    public static class FwMapper
    {
        private static AutoMapper.Mapper mapper { get; set; }

        static FwMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Car, CarDto>();

                cfg.CreateMap<Course, CourseDto>();

                cfg.CreateMap<Driver, DriverDto>();

                cfg.CreateMap<Manufacturer, ManufacturerDto>();
                
                cfg.CreateMap<Region, RegionDto>();
                
                cfg.CreateMap<TireType, TireTypeDto>();
                
                cfg.CreateMap<Tune, TuneDto>();
                cfg.CreateMap<TuneDto, Tune>();


                cfg.CreateMap<DataTransfer.OwnedCarInfoDto, OwnedCar>();
            });

            mapper = new Mapper(config);
        }

        public static TDest Map<TSource, TDest>(TSource source)
        {
            return mapper.Map<TDest>(source);
        }

        public static List<TDest> MapList<TSource, TDest>(List<TSource> sourceList)
        {
            var mappedList = new List<TDest>();

            foreach (var source in sourceList)
            {
                var mapped = mapper.Map<TSource, TDest>(source);
                mappedList.Add(mapped);
            }

            return mappedList;
        }
    }
}
