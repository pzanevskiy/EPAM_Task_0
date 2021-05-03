using AutoMapper;
using System;
using System.Text;
using Task2.Models;
using Text_Analyzer.TextUtility.DataTransferObject;

namespace Text_Analyzer.TextUtility.Utils
{
    public class MapperConfig
    {
        public static MapperConfiguration Configure()
        {
            var config = new MapperConfiguration
            (
                cfg =>
                {
                    cfg.CreateMap<ConcordanceItem, ConcordanceItemsDTO>().ReverseMap();
                }
            );
            return config;
        }
    }
}
