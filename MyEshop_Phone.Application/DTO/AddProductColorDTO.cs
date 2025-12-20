using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Application.DTO
{
    public class AddProductColorDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public List<int> SelectedColorIds { get; set; } = new();
        public IEnumerable<ShowColorDTO> colors { get; set; }
        public IEnumerable<ProductsShowDTO> productsShows { get; set; }

    }
    public class ProductWithColorsDto
    {
        public int ProductId { get; set; }
        public string ProductTitle { get; set; }
        public List<ShowColorDTO> Colors { get; set; }
    }
    public class ShowColorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class ProductsShowDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
