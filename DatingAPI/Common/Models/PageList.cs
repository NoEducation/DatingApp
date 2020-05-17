using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DatingAPI.Common.Models
{
    public class PageList<T> : List<T>
    {
        public int TotalItems { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        public PageList(IEnumerable<T> items , int totalItems
            , int currentPage, int pageSize ) : base(items)
        {
            TotalPages =(int)Math.Ceiling( totalItems / (double)pageSize);
            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
        }

        public static async Task<PageList<T>> CreateAsync<T>(IQueryable<T> source, int pageNumber , int pageSize)
        {
            var totalItems = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * (pageSize == default(int) ? 10 : pageSize ))
                .Take(pageSize).ToListAsync();

            return new PageList<T>(items,totalItems,pageNumber, (pageSize == default(int) ? 10 : pageSize));
        }
    }
}
