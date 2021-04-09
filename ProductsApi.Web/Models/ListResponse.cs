using System.Collections.Generic;

namespace XeroTechnicalTest.Models
{
    public class ListResponse<T>
    {
        public ListResponse(List<T> items)
        {
            Items = items;
        }
        
        public List<T> Items { get; set; }
    }
}