using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean.Domain;
using Clean.Service;

namespace Clean.Repository
{
    public class ProductRepository(AppDbContext context) : IProductRepository
    {
        public Product Add(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
            return product;
        }

        public List<Product> GetProducts()
        {
            return context.Products.ToList();
        }
    }
}