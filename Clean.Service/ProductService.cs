using Clean.Domain;

namespace Clean.Service
{
    public class ProductService(IProductRepository productRepository, ICacheService cacheService)
    {
        public void AddProduct(ProductCreateRequestDto request)
        {
            var product = new Product() { Name = request.Name, Created = DateTime.Now };

            productRepository.Add(product);


            // Add product logic here
        }

        public List<ProductDto> GetProduct()
        {
            //Cache aside design pattern

            if (cacheService.Get<List<Product>>("productList") != null)
            {
                var productsAsCache = cacheService.Get<List<Product>>("productList");
                return productsAsCache.Select(x => new ProductDto(x.Id, x.Name, x.Created)).ToList();
            }


            //all product
            var products = productRepository.GetProducts();

            //set cache
            cacheService.Set("productList", products);
            return products.Select(x => new ProductDto(x.Id, x.Name, x.Created)).ToList();
        }
    }
}