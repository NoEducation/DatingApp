using DatingAPI.Common.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace DatingAPI.Common.Extensions
{
    public static class ResponseExtensions
    {
        public static void AddApplicationError(this HttpResponse response,string errorMessage)
        {
            response.Headers.Add("Application-Error", errorMessage);
            response.Headers.Add("Access-Control-Expose_Headers", "Application-Error");
            response.Headers.Add("Access-Contorl-Allow-Origin", "*");
        }

        public static void AddPagination(this HttpResponse response, int currentPage, int itemsPerPage, int totalItems,
            int totalPages)
        {
            var paginationHeader = new PageHeader(totalItems,totalPages,currentPage,itemsPerPage);
            response.Headers.Add("Pagination",JsonConvert.SerializeObject(paginationHeader));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }
    }
}
