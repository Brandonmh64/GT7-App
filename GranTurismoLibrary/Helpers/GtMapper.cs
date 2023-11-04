using AutoMapper;
using GranTurismoFramework;
using GranTurismoFramework.DataTransfer;
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
                cfg.CreateMap<Course, CourseDto>();
                cfg.CreateMap<CourseDto, Course>();

                cfg.CreateMap<Driver, DriverDto>();
                cfg.CreateMap<DriverDto, Driver>();

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

                cfg.CreateMap<Region, RegionDto>();
                cfg.CreateMap<RegionDto, Region>();

                cfg.CreateMap<TimeTrialInfo, TimeTrialInfoDto>()
                    .ForMember(dest => dest.TrackInfo, opt => opt.MapFrom(src => new TrackInfoDto()
                    {
                        Track = new Track()
                        {
                            TrackId = src.Track.TrackId,
                            CourseId = src.Course.CourseId,
                            Name = src.Track.TrackName
                        },
                        Course = new Course()
                        {
                            CourseId = src.Course.CourseId,
                            Name = src.Course.Name,
                            RegionId = src.Region.RegionId
                        },
                        Region = new Region()
                        {
                            RegionId = src.Region.RegionId,
                            Name = src.Region.Name,
                        }
                    }))
                    .ForMember(dest => dest.OwnedCarInfo, opt => opt.MapFrom(src => Map<OwnedCarInfo, OwnedCarInfoDto>(src.OwnedCarInfo)))
                    .ForMember(dest => dest.Tune, opt => opt.MapFrom(src => Map<TuneInfo, TuneInfoDto>(src.TuneInfo)))
                    .ForMember(dest => dest.Driver, opt => opt.MapFrom(src => Map<DriverDto, Driver>(src.Driver)));
                cfg.CreateMap<TimeTrialInfoDto, TimeTrialInfo>()
                    .ForMember(dest => dest.Track, opt => opt.MapFrom(src => new TrackInfo()
                    {
                        TrackId = src.TrackInfo.Track.TrackId,
                        TrackName = src.TrackInfo.Track.Name,
                        CourseId = src.TrackInfo.Course.CourseId,
                        CourseName = src.TrackInfo.Course.Name,
                        RegionId = src.TrackInfo.Region.RegionId,
                        RegionName = src.TrackInfo.Region.Name,
                    }))
                    .ForMember(dest => dest.Course, opt => opt.MapFrom(src => Map<Course, CourseDto>(src.TrackInfo.Course)))
                    .ForMember(dest => dest.Region, opt => opt.MapFrom(src => Map<Region, RegionDto>(src.TrackInfo.Region)))
                    .ForMember(dest => dest.TuneInfo, opt => opt.MapFrom(src => Map<TuneInfoDto, TuneInfo>(src.Tune)))
                    .ForMember(dest => dest.Driver, opt => opt.MapFrom(src => Map<Driver, DriverDto>(src.Driver)));

                cfg.CreateMap<TimeTrialInfo, PastRecord>()
                    .ForMember(dest => dest.SessionDateTime, opt => opt.Ignore());

                cfg.CreateMap<TireType, TireTypeDto>();
                cfg.CreateMap<TireTypeDto, TireType>();

                cfg.CreateMap<GranTurismoFramework.DataTransfer.TrackInfoDto, TrackInfo>()
                    .ForMember(dest => dest.RegionId, opt => opt.MapFrom(src => src.Region.RegionId))
                    .ForMember(dest => dest.CourseId, opt => opt.MapFrom(src => src.Course.CourseId))
                    .ForMember(dest => dest.TrackId, opt => opt.MapFrom(src => src.Track.TrackId));
                cfg.CreateMap<TrackInfo, TrackInfoDto>()
                    .ForMember(dest => dest.Region, opt => opt.MapFrom(src => new RegionDto() { Name = src.RegionName, RegionId = src.RegionId }))
                    .ForMember(dest => dest.Course, opt => opt.MapFrom(src => new CourseDto() { Name = src.CourseName, RegionId = src.RegionId, CourseId = src.CourseId }))
                    .ForMember(dest => dest.Track, opt => opt.MapFrom(src => new Track() { Name = src.TrackName, TrackId = src.TrackId, CourseId = src.CourseId }));

                cfg.CreateMap<TuneDto, TuneInfo>();
                cfg.CreateMap<TuneInfo, TuneDto>();
                cfg.CreateMap<TuneInfo, Tune>();
                cfg.CreateMap<TuneInfoDto, TuneInfo>();
                cfg.CreateMap<TuneInfo, TuneInfoDto>();
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
