using AutoMapper;
using FashionClothesAndTrends.Application.DTOs;
using FashionClothesAndTrends.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace FashionClothesAndTrends.Application.Helpers;

public class ClothingItemUrlResolver : IValueResolver<ClothingItem, ClothingItemDto, string>
{
    private readonly IConfiguration _config;

    public ClothingItemUrlResolver(IConfiguration config)
    {
        _config = config;
    }

    public string Resolve(ClothingItem source, ClothingItemDto destination, string destMember,
        ResolutionContext context)
    {
        if (!string.IsNullOrEmpty(source.PictureUrl))
        {
            return _config["ApiUrl"] + source.PictureUrl;
        }

        return null;
    }
}