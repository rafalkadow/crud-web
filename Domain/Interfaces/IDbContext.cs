﻿using Domain.Modules.Account;
using Domain.Modules.Base.Models;
using Domain.Modules.CategoryOfProduct.Models;
using Domain.Modules.Product.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Shared.Interfaces;

namespace Domain.Interfaces
{
    public interface IDbContext
    {
        public DbSet<Audit> Audits { get; set; }
        public DbSet<AccountModel> Account { get; set; }
        public DbSet<CategoryOfProductModel> CategoryOfProduct { get; set; }
        public DbSet<ProductModel> Product { get; set; }
        IQueryable<TEntity> GetQueryable<TEntity>()
            where TEntity : class, IEntity;

        Task CreateAsync<TEntity>(TEntity entity, IUserAccessor userAccessor)
            where TEntity : class, IEntity;

        Task UpdateAsync<TEntity>(TEntity entity)
            where TEntity : class, IEntity;

        Task UpdatePropertiesAsync<TEntity>(TEntity entity, ICommand model, IUserAccessor userAccessor)
           where TEntity : class, IEntity;

        Task DetachedAsync<TEntity>(TEntity entity)
         where TEntity : class, IEntity;

        Task DeleteAsync<TEntity>(TEntity entity)
            where TEntity : class, IEntity;

        Task DeleteRangeAsync<TEntity>(IList<TEntity> entity)
            where TEntity : class, IEntity;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitTransactionAsync();

        Task RollbackTransactionAsync();

        Task<bool> HasChangesAsync();
    }
}