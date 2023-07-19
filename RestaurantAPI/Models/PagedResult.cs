﻿namespace RestaurantAPI.Models
{
    public class PagedResult<T>
    {
        public List<T>? Items { get; set; }
        public int TotalPages { get; set; }
        public int ItemsFrom { get; set; }
        public int ItemsTo { get; set; }
        public int TotalItemsCount { get; set; }

        public PagedResult(List<T> items, int totalItemsCount, int pageSize, int pageNumber)
        {
            Items = items;
            TotalItemsCount = totalItemsCount;
            ItemsFrom = (pageNumber - 1) * pageSize;
            ItemsTo = ItemsFrom + pageSize - 1;
            TotalPages =  (int)Math.Ceiling(totalItemsCount / (double)pageSize);
        }
    }
}
