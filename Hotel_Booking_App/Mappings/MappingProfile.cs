using AutoMapper;
using Hotel_Booking_App.Models;
using Hotel_Booking_App.Models.DTOs.Hotel_Room;
using Hotel_Booking_App.Models.DTOs.Booking;
using Hotel_Booking_App.Interface.Hotel_Room;
using Hotel_Booking_App.Models.DTOs.Review;
using HotelBookingApp.Models;

namespace Hotel_Booking_App.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // ✅ Hotel Mapping  
            CreateMap<AddHotelDto, Hotel>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Hotel, HotelResponseDto>()
                .ForMember(dest => dest.Reviews, opt => opt.MapFrom(src => src.Reviews));

            CreateMap<Review, ReviewResponseDto>();

            // ✅ Room Mapping  
            CreateMap<Room, RoomRequestDto>().ReverseMap();
            CreateMap<Room, RoomResponseDto>();

            // ✅ Booking Mapping  
            CreateMap<BookingRequestDto, Booking>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => BookingStatus.Pending))
                .ForMember(dest => dest.SpecialRequest, opt => opt.MapFrom(src => src.SpecialRequest ?? ""))
                .ForMember(dest => dest.BookingRooms, opt => opt.Ignore())
                .ForMember(dest => dest.Customer, opt => opt.Ignore())
                .ForMember(dest => dest.Hotel, opt => opt.Ignore());

            CreateMap<Booking, BookingResponseDto>()
                .ForMember(dest => dest.BookingId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.HotelName, opt => opt.MapFrom(src => src.Hotel != null ? src.Hotel.Name : "Unknown Hotel"))
                .ForMember(dest => dest.RoomsBooked, opt => opt.MapFrom(src => src.BookingRooms.Select(br => new RoomDetailsDto
                {
                    RoomId = br.Room.Id,
                    RoomNumber = br.Room.RoomNumber,
                    RoomType = br.Room.Type,
                    PricePerNight = br.Room.PricePerNight
                }).ToList()))
                .ForMember(dest => dest.RoomsBookedCount, opt => opt.MapFrom(src => src.BookingRooms.Count))
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
