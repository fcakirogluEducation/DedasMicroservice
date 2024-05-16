using Clean.Domain;

namespace Clean.Service
{
    public class ProductService(IProductRepository productRepository) : IProductService
    {
        public void AddProduct(ProductCreateRequestDto request)
        {
            var product = new Product() { Name = request.Name, Created = DateTime.Now };

            productRepository.Add(product);


            // Add product logic here
        }

        public List<ProductDto> GetProduct()
        {
            var products = productRepository.GetProducts();


            return products.Select(x => new ProductDto(x.Id, x.Name.ToLower(), x.Created)).ToList();
        }
    }
}