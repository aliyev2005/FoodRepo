using AutoMapper;
using FoodProject.DTO;
using FoodProject.Model;

namespace FoodProject.Helper
{
    public class MapingProfiles: Profile
    {
        public MapingProfiles()
        {
            CreateMap<User, ProfileRequest>();
            CreateMap<Order, GetUpcomingOrdersRequest>();
        }
    }
}
