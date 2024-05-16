using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean.Domain;

namespace Clean.Service
{
    public interface IProductService
    {
        void AddProduct(ProductCreateRequestDto request);

        List<ProductDto> GetProduct();
    }
}