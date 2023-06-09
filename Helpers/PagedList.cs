using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace API.Helpers
{
    public class PagedList<T> : List<T>
    {
        public PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
        {
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            PageSize = pageSize;
            TotalCount = count;
            AddRange(items);
        }

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            // CountAsync() is an extension method from Microsoft.EntityFrameworkCore
            var count = await source.CountAsync();
            // Skip() and Take() are extension methods from System.Linq
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            // Return a new instance of PagedList<T>
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}