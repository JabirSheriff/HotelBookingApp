using AutoMapper;
using Hotel_Booking_App.Models;
using Hotel_Booking_App.Models.DTOs.Hotel_Room;
using Hotel_Booking_App.Models.DTOs.Booking;
using Hotel_Booking_App.Interface.Hotel_Room;

namespace Hotel_Booking_App.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // ✅ Hotel Mapping  
            CreateMap<AddHotelDto, Hotel>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); // Ignore Id as it's auto-generated  

            CreateMap<Hotel, HotelResponseDto>();

            // ✅ Room Mapping  
            CreateMap<RoomRequestDto, Room>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Ignore Id (auto-generated)  
                .ForMember(dest => dest.Hotel, opt => opt.Ignore()); // Hotel entity should not be mapped directly  

            CreateMap<Room, RoomResponseDto>();

            // ✅ Booking Mapping  
            CreateMap<BookingRequestDto, Booking>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Ignore Id (auto-generated)  
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => BookingStatus.Pending)) // Default status  
                .ForMember(dest => dest.BookingRooms, opt => opt.Ignore()) // BookingRooms handled separately  
                .ForMember(dest => dest.Customer, opt => opt.Ignore()) // Customer set separately  
                .ForMember(dest => dest.Hotel, opt => opt.Ignore()) // Hotel entity should not be mapped directly  
                .ForMember(dest => dest.SpecialRequest, opt => opt.MapFrom(src => src.SpecialRequest ?? "")); // ✅ Prevent NULL

            CreateMap<Booking, BookingResponseDto>()
                //.ForMember(dest => dest.CustomerFullName, opt => opt.MapFrom(src => src.Customer != null ? src.Customer.FullName : string.Empty))
                .ForMember(dest => dest.HotelName, opt => opt.MapFrom(src => src.Hotel.Name))
                .ForMember(dest => dest.RoomsBookedCount, static opt => opt.MapFrom(static src => src.BookingRooms.Select(br => new RoomDetailsDto
                {
                    RoomId = br.Room.Id,
                    RoomNumber = br.Room.RoomNumber,
                    RoomType = br.Room.Type,
                    PricePerNight = br.Room.PricePerNight
                }).ToList()))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice));

            // ✅ BookingRoom Mapping  
            CreateMap<BookingRoom, RoomDetailsDto>()
                .ForMember(dest => dest.RoomId, opt => opt.MapFrom(src => src.Room.Id))
                .ForMember(dest => dest.RoomNumber, opt => opt.MapFrom(src => src.Room.RoomNumber))
                .ForMember(dest => dest.RoomType, opt => opt.MapFrom(src => src.Room.Type))
                .ForMember(dest => dest.PricePerNight, opt => opt.MapFrom(src => src.Room.PricePerNight));
        }
    }
}
