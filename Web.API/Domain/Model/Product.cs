using System;
using System.Collections.Generic;

#nullable disable

namespace Web.API.Domain.Model
{
    public partial class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal? Price { get; set; }
    }
}
