using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.API.Domain.Model;

namespace Web.API.Domain.Responses
{
    public class ProductListResponse : BaseResponse
    {
        public IEnumerable<Product> ProductList { get; set; }
        public ProductListResponse(bool success, string message,IEnumerable<Product> productList) : base(success, message)
        {
            this.ProductList = productList;
        }
        public ProductListResponse(IEnumerable<Product> productList) :this(true,string.Empty, productList) { }
        public ProductListResponse(string message) : this(false,message,null) { }
    }
}
