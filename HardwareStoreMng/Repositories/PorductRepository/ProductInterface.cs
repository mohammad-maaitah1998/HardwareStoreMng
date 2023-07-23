using HardwareStoreMng.DTO;
using HardwareStoreMng.Models;

namespace HardwareStoreMng.Repositories.PorductRepository
{
    public interface ProductInterface
    {
        IEnumerable<ProductDTO> GetAllProducts();
        ProductDTO GetProductById(int productId);
        void AddProduct(ProductDTO productDto);
        void UpdateProduct(ProductDTO productDto);
        void DeleteProduct(int productId);


    }
}
