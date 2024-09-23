using AutoMapper;
using FashionClothesAndTrends.Application.DTOs;
using FashionClothesAndTrends.Application.Exceptions;
using FashionClothesAndTrends.Application.Services.Interfaces;
using FashionClothesAndTrends.Application.UoW;
using FashionClothesAndTrends.Domain.Entities.Enums;
using FashionClothesAndTrends.Domain.Entities.OrderAggregate;

namespace FashionClothesAndTrends.Application.Services;

public class OrderHistoryService : IOrderHistoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public OrderHistoryService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task CreateOrderHistoryAsync(OrderHistoryDto orderHistoryDto, string userId)
    {
        if (orderHistoryDto == null)
        {
            throw new ArgumentNullException(nameof(orderHistoryDto));
        }

        var orderHistory = new OrderHistory
        {
            OrderDate = orderHistoryDto.OrderDate,
            TotalAmount = orderHistoryDto.TotalAmount,
            Status = Enum.Parse<OrderStatus>(orderHistoryDto.Status),
            ShippingAddress = orderHistoryDto.ShippingAddress,
            UserId = userId,
            OrderItems = orderHistoryDto.OrderItems.Select(item => new OrderItemHistory
            {
                ClothingItemId = item.ClothingItemId,
                ClothingItemName = item.ClothingItemName,
                Quantity = item.Quantity,
                PriceAtPurchase = item.PriceAtPurchase
            }).ToList()
        };

        await _unitOfWork.OrderHistoryRepository.AddAsync(orderHistory);
        await _unitOfWork.SaveAsync();
    }

    public async Task<IReadOnlyList<OrderHistoryToReturnDto>> GetOrderHistoriesForUserAsync(string userId)
    {
        var orderHistories = await _unitOfWork.OrderHistoryRepository.GetOrderHistoryByUserIdAsync(userId);
        if (orderHistories == null)
        {
            throw new NotFoundException($"Order histories not found.");
        }

        return _mapper.Map<IReadOnlyList<OrderHistoryToReturnDto>>(orderHistories);
    }

    public async Task<OrderHistoryToReturnDto> GetOrderHistoryByIdAsync(Guid id)
    {
        var orderHistory = await _unitOfWork.OrderHistoryRepository.GetByIdAsync(id);
        if (orderHistory == null)
        {
            throw new NotFoundException($"Order history with ID '{id}' not found.");
        }

        return _mapper.Map<OrderHistoryToReturnDto>(orderHistory);
    }

    public async Task<IReadOnlyList<OrderHistoryToReturnDto>> GatAllOrderHistoriesAsync()
    {
        var orderHistories = await _unitOfWork.OrderHistoryRepository.ListAllAsync();
        if (orderHistories == null)
        {
            throw new NotFoundException($"Order histories not found.");
        }

        return _mapper.Map<IReadOnlyList<OrderHistoryToReturnDto>>(orderHistories);
    }
}