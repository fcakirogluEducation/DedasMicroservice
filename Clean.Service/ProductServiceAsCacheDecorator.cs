using Clean.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Service
{
    public class ProductServiceAsCacheDecorator(IProductService productService, ICacheService cacheService)
        : IProductService
    {
        public void AddProduct(ProductCreateRequestDto request)
        {
            productService.AddProduct(request);
        }

        public List<ProductDto> GetProduct()
        {
            //Cache aside design pattern

            if (cacheService.Get<List<ProductDto>>("productList") != null)
            {
                var productsAsCache = cacheService.Get<List<ProductDto>>("productList");

                return productsAsCache;
            }

            var productListAsDto = productService.GetProduct();


            cacheService.Set("productList", productListAsDto);

            return productListAsDto;
        }
    }
}