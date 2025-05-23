using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.Sharing
{
    public class ProductParam
    {
        //string sort,int?CategotyId,int pageSize,int pageNumber
        public string? sort { get; set; }
        public int? CategotyId { get; set; }
        public int MaxPageSize { get; set; } = 6;
        private int _pageSize=3;
        public string ? Search { get; set; }
        public int pageSize
        {
            get { return _pageSize=3; }
            set { _pageSize = value>MaxPageSize?MaxPageSize:value; }
        }
        public int pageNumber { get; set; } = 1;
    }
}
