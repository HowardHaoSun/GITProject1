using System;


namespace SportsStore.Models
{
    public class PagingInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }

        public int TotalPage
        {
            get { return (int)Math.Ceiling((double)TotalItems / ItemsPerPage); }
        }
    }
}