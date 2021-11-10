using System.Collections.Generic;

namespace CDCC.Bussiness.Common
{
    public class PagedResult<T>
    {
        public PagedResult(List<T> item, int total)
        {
            this.Items = item;
            this.TotalRecord = total;
        }

        public PagedResult()
        {
           
        }

        public List<T> Items { get; set; }
        public int TotalRecord { get; set; }
    }
}
