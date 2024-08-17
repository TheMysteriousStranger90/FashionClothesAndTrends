using AutoMapper;
using FashionClothesAndTrends.Application.DTOs;
using FashionClothesAndTrends.Application.Exceptions;
using FashionClothesAndTrends.Application.Services.Interfaces;
using FashionClothesAndTrends.Application.UoW;
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

    public async Task<OrderHistoryDto> CreateOrderHistoryAsync(OrderDto orderDto)
    {
        var order = _mapper.Map<Order>(orderDto);

        var orderHistory = new OrderHistory
        {
            OrderDate = order.OrderDate.DateTime,
            TotalAmount = order.GetTotal(),
            Status = order.Status,
            ShippingAddress = _mapper.Map<string>(order.ShipToAddress.AddressLine),
            UserId = order.BuyerEmail,
            OrderItems = order.OrderItems.Select(item => new OrderItemHistory
            {
                ClothingItemId = item.ItemOrdered.ClothingItemId,
                ClothingItemName = item.ItemOrdered.ClothingItemName,
                Quantity = item.Quantity,
                PriceAtPurchase = item.Price
            }).ToList()
        };

        _unitOfWork.GenericRepository<OrderHistory>().Add(orderHistory);
        var result = await _unitOfWork.SaveAsync();

        if (result <= 0)
        {
            throw new InternalServerException("Failed to save the order history.");
        }

        return _mapper.Map<OrderHistoryDto>(orderHistory);
    }

    public async Task<IReadOnlyList<OrderHistoryDto>> GetOrderHistoriesForUserAsync(string userId)
    {
        var orderHistories = await _unitOfWork.GenericRepository<OrderHistory>().ListAllAsync();
        var res = orderHistories.Where(u => u.UserId == userId).ToList();

        return _mapper.Map<IReadOnlyList<OrderHistoryDto>>(res);
    }

    public async Task<OrderHistoryDto> GetOrderHistoryByIdAsync(Guid id)
    {
        var orderHistory = await _unitOfWork.GenericRepository<OrderHistory>().GetByIdAsync(id);
        if (orderHistory == null)
        {
            throw new NotFoundException($"Order history with ID '{id}' not found.");
        }

        return _mapper.Map<OrderHistoryDto>(orderHistory);
    }
}
