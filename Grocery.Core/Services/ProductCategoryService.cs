using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;

namespace Grocery.Core.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IProductRepository _productRepository;

        public ProductCategoryService(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }

        public List<ProductCategory> GetAll()
        {
            return _productCategoryRepository.GetAll();
        }
        
        public List<ProductCategory> GetAllOnCategoryId(int id)
        {
            return _productCategoryRepository.GetAll().Where(c => c.CategoryId == id).ToList();
        }
        
        private void FillService(List<ProductCategory> productCategory)
        {
            foreach (ProductCategory pc in productCategory)
            {
                pc.Product = _productRepository.Get(pc.ProductId) ?? new(0, "", 0);
            }
        }
    }
}