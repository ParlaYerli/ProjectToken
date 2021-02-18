using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.API.Domain.Model;
using Web.API.Resources;

namespace Web.API.Mapping
{
    public class ProductMapping:Profile
    {
        public ProductMapping()
        {
            CreateMap<ProductResource,Product>();
            CreateMap<Product, ProductResource>();
        }
    }
}
