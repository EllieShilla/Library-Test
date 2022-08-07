using AutoMapper;
using Library_.DTO;
using Library_.Models;

namespace Library_.Data
{
    public class AppMappingProfile:Profile
    {
        public AppMappingProfile()
        {
            CreateMap<Review, ReviewDTO>();
        }
    }
}
