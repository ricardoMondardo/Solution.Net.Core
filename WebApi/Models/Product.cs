﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Product
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        public bool IsUse { get; set; }
        
        public int? ProductGradeId { get; set; }
        public ProductGrade ProductGrade { get; set; }

        public ProductDetail ProductDetail { get; set; } //one-to-one

        public ICollection<ProdutItem> ProdutItens { get; set; }

    }
}
