using System;

namespace Maquer.CatalogService.Dtos
{
    public class ProductCreationDto
    {
        public string Name { get; set; }
        public string SubName { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
    }
}
