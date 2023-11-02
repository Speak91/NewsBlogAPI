using AutoMapper;
using NewsBlogAPI.Data;
using NewsBlogAPI.Models;

namespace NewsBlogAPI.AutoMapper
{
    public class NewsProfile : Profile
    {
        public NewsProfile()
        {
            CreateMap<AddNewsRequestModel, News>();
            CreateMap<UpdateNewsRequestModel, News>();
            CreateMap<News, GetNewsResponseModel>();
        }
    }
}
