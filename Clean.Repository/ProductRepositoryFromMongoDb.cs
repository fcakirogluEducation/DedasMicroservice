using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean.Domain;
using Clean.Service;

namespace Clean.Repository
{
    public class ProductRepositoryFromMongoDb : IProductRepository
    {
        public Product Add(Product product)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetProducts()
        {
            throw new NotImplementedException();
        }
    }
}