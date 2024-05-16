using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean.Domain;

namespace Clean.Service
{
    public interface IProductRepository
    {
        Product Add(Product product);

        List<Product> GetProducts();
    }
}