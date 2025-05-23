﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.DTO
{
    public record ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal NewPrice { get; set; }
        public decimal OldPrice { get; set; }
        public string CategoryName { get; set; }
        public virtual List<PhotoDTO> Photos { get; set; }
    }
    public record ReturnProductDTO
    {
        public List<ProductDTO> products { get; set; }
        public int TotalCount { get; set; }
    }
    public record PhotoDTO
    {
        public string ImageName { get; set; }
        public int ProductId { get; set; }
    }
    public record AddProductDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal NewPrice { get; set; }
        public decimal OldPrice { get; set; }
        public int CategoryID { get; set; }
        public IFormFileCollection Photos  { get; set; }


    }
    public record UpdateProductDTO:AddProductDTO
    {
        public int Id { get; set; }
    }
}
