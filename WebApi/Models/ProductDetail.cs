﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class ProductDetail
    {
        public int Id { get; set; }
        public string ComeFrom { get; set; }
        public string MadeFor { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
