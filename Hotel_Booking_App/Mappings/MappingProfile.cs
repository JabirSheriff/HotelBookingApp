using AutoMapper;
using Hotel_Booking_App.Models;
using Hotel_Booking_App.Models.DTOs.Hotel_Room;

namespace Hotel_Booking_App.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddHotelDto, Hotel>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Ignore Id as it's auto-generated
                .ForMember(dest => dest.HotelOwnerId, opt => opt.Ignore()); // OwnerId is set manually

            CreateMap<Hotel, HotelResponseDto>();

            CreateMap<AddRoomDto, Room>();
            CreateMap<Room, RoomResponseDto>();
        }
    }
}
