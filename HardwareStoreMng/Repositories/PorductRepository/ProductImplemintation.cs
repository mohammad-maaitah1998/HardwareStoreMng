using HardwareStoreMng.Context;
using HardwareStoreMng.DTO;
using HardwareStoreMng.Models;

namespace HardwareStoreMng.Repositories.PorductRepository
{
    public class ProductImplemintation : ProductInterface
    {
        public readonly HardwareStoreMngContext _context;

        public ProductImplemintation(HardwareStoreMngContext context)
        {
            _context = context;
        }
        public void AddProduct(ProductDTO productDto)
        {
            var newProduct = new Product
            {
                productId = productDto.productId,
                productName = productDto.productName,
                productDescription = productDto.productDescription,
                price = productDto.price,

            };
            

            _context.Product.Add(newProduct);
            _context.SaveChanges();

        }

        public void DeleteProduct(int productId)
        {
            var existingProduct = _context.Product.Find(productId);

            if (existingProduct == null)
            {
                
                return;
            }

            _context.Product.Remove(existingProduct);
            _context.SaveChanges();
        }

        public IEnumerable<ProductDTO> GetAllProducts()
        {
            var products = _context.Product.ToList();
            var productDtos = products.Select(p => new ProductDTO
            {
                productId = p.productId,
                productName = p.productName,
                productDescription = p.productDescription,
                price = p.price
                
            });
            return productDtos;
        }

        public ProductDTO GetProductById(int productId)
        {
            var product = _context.Product.FirstOrDefault(p => p.productId == productId);

            if (product == null)
            {
                return null; 
            }

            
            var productDto = new ProductDTO
            {
                productId = product.productId,
                productName = product.productName,
                productDescription = product.productDescription,
                price= product.price
            };

            return productDto;
        }

        public void UpdateProduct(ProductDTO productDto)
        {

            var existingProduct = _context.Product.Find(productDto.productId);

            if (existingProduct == null)
            {
                
                return;
            }

            existingProduct.price = productDto.price;
            

            _context.SaveChanges();

        }
    }
}
