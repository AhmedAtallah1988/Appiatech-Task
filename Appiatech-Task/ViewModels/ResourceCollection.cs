using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appiatech_Task.ViewModels
{
    public class ResourceCollection<T>
    {
        public ResourceCollection(IEnumerable<T> items, long totalResults)
        {
            Items = items;
            TotalResults = totalResults;
        }
        public IEnumerable<T> Items { get; set; }
        public long TotalResults { get; set; }
    }
}
