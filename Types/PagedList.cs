using Gero.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gero.API.Types
{
    public class PagedList<T>
    {
        public PagedList(IQueryable<T> model, int pageNumber, int pageSize)
        {
            TotalEntries = model.Count();
            PageNumber = pageNumber;
            PageSize = pageSize;
            List = model
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToList();
        }

        public int TotalEntries { get; }
        public int PageNumber { get; }
        public int PageSize { get; }
        public int TotalPages => (int)Math.Ceiling(TotalEntries / (double)PageSize);
        public List<T> List { get; }

        public Paging Get()
        {
            return new Paging(TotalEntries, PageNumber, PageSize, TotalPages);
        }
    }
}
