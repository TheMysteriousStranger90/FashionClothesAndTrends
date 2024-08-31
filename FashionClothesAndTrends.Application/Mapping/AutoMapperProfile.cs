using AutoMapper;
using FashionClothesAndTrends.Application.DTOs;
using FashionClothesAndTrends.Application.Extensions;
using FashionClothesAndTrends.Application.Helpers;
using FashionClothesAndTrends.Domain.Entities;
using FashionClothesAndTrends.Domain.Entities.OrderAggregate;

namespace FashionClothesAndTrends.Application.Mapping;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<User, LoginDto>().ReverseMap();
        CreateMap<User, RegisterDto>().ReverseMap();

        CreateMap<User, UserDto>()
            .ForMember(dest => dest.PhotoUrl,
                opt => opt.MapFrom(src => src.UserPhotos.FirstOrDefault(x => x.IsMain).Url))
            .ForMember(dest => dest.Age,
                opt => opt.MapFrom(src => src.DateOfBirth.CalcuateAge()));
        
        CreateMap<UserPhoto, UserPhotoDto>();
        CreateMap<UserPhotoDto, UserPhoto>();
        
        CreateMap<ClothingItem, ClothingItemDto>()
            .ForPath(dest => dest.PictureUrl,
                opt => opt.MapFrom(src => src.ClothingItemPhotos.FirstOrDefault(x => x.IsMain).Url));
        CreateMap<ClothingItemPhoto, ClothingItemPhotoDto>();
        CreateMap<ClothingItemPhotoDto, ClothingItemPhoto>();
        
        CreateMap<ShippingAddress, AddressDto>().ReverseMap();
        CreateMap<AddressDto, AddressAggregate>();
        CreateMap<CustomerBasketDto, CustomerBasket>();
        CreateMap<BasketItemDto, BasketItem>();
        
        CreateMap<Order, OrderToReturnDto>()
            .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
            .ForMember(d => d.ShippingPrice, o => o.MapFrom(s => s.DeliveryMethod.Price));
        
        CreateMap<OrderItem, OrderItemDto>()
            .ForMember(d => d.ClothingItemId, o => o.MapFrom(s => s.ItemOrdered.ClothingItemId))
            .ForMember(d => d.ClothingItemName, o => o.MapFrom(s => s.ItemOrdered.ClothingItemName))
            .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.ItemOrdered.MainPictureUrl));
        
        CreateMap<Comment, CommentDto>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
            .ReverseMap();
        
        CreateMap<Coupon, CouponDto>().ReverseMap();
        
        CreateMap<FavoriteItem, FavoriteItemDto>()
            .ForPath(dest => dest.ClothingItemDto.Name, opt => opt.MapFrom(src => src.ClothingItem.Name))
            .ReverseMap();
        
        CreateMap<LikeDislike, LikeDislikeDto>()
            .ForPath(dest => dest.UserDto.Username, opt => opt.MapFrom(src => src.User.UserName))
            .ReverseMap();
        
        CreateMap<Notification, NotificationDto>().ReverseMap();
        
        CreateMap<Rating, RatingDto>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.UserName))
            .ReverseMap();
        
        CreateMap<Wishlist, WishlistDto>().ReverseMap();
        CreateMap<WishlistItem, WishlistItemDto>()
            .ForMember(dest => dest.ClothingItemName, opt => opt.MapFrom(src => src.ClothingItem.Name))
            .ReverseMap();
        
        CreateMap<OrderHistory, OrderHistoryDto>().ReverseMap();
        CreateMap<OrderItemHistory, OrderItemHistoryDto>().ReverseMap();
        
        CreateMap<DateTime, DateTime>().ConvertUsing(d => DateTime.SpecifyKind(d, DateTimeKind.Utc));
        CreateMap<DateTime?, DateTime?>()
            .ConvertUsing(d => d.HasValue ? DateTime.SpecifyKind(d.Value, DateTimeKind.Utc) : null);
    }
}
