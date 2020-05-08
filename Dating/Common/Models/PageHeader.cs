using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dating.Common.Models
{
    public class PageHeader
    {
        public int TotalItems{ get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int ItemsPerPage { get; set; }


        #region Constructor
        public PageHeader()
        {

        }

        public PageHeader(int totalItems, int totalPages, int currentPage, int itemsPerPage)
        {
            TotalItems = totalItems;
            TotalPages = totalPages;
            CurrentPage = currentPage;
            ItemsPerPage = itemsPerPage;
        }
        #endregion
        
    }
}
