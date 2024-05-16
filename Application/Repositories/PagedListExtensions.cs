﻿using Domain.Modules;
using Microsoft.EntityFrameworkCore;

namespace Application.Repositories.Interfaces.Extensions
{
    public static class PaginatedListExtensions
    {
        public static async Task<PaginatedList<T>> ToPaginatedListAsync<T>(this IQueryable<T> source, int pageNumber, int pageSize)
        {
            var totalCount = source.Count();
            var items = await source
                                .Skip((pageNumber - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync();

            return new PaginatedList<T>(items, pageNumber, pageSize, totalCount);
        }

        public static PaginatedList<T> ToPaginatedList<T>(this IEnumerable<T> source, int pageNumber, int pageSize)
        {
            var totalCount = source.Count();
            var items = source
                                .Skip((pageNumber - 1) * pageSize)
                                .Take(pageSize)
                                .ToList();

            return new PaginatedList<T>(items, pageNumber, pageSize, totalCount);
        }
    }
}