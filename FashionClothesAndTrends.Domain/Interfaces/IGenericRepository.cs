﻿using System.Linq.Expressions;
using FashionClothesAndTrends.Domain.Common;
using FashionClothesAndTrends.Domain.Specifications.Interfaces;

namespace FashionClothesAndTrends.Domain.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T> GetByIdAsync(Guid id);
    Task<IReadOnlyList<T>> ListAllAsync();
    Task<T> GetEntityWithSpec(ISpecification<T> spec);
    Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
    Task<int> CountAsync(ISpecification<T> spec);
    void Add(T entity);
    void Update(T entity);
    void Remove(T entity);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task RemoveAsync(T entity);
    Task<T> GetByConditionAsync(Expression<Func<T, bool>> predicate);
}