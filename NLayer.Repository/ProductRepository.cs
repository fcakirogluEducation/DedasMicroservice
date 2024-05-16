using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository
{
    public class ProductRepository(AppDbContext context) : IProductRepository
    {
        public IEnumerable<Product> GetAll()
        {
            return context.Products.ToList();
        }
    }
}