using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;

namespace Grocery.Core.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductCategoryService(IProductCategoryRepository productCategoryRepository, IProductRepository productRepository)
        {
            _productCategoryRepository = productCategoryRepository;
            _productRepository = productRepository;
        }

        public List<ProductCategory> GetAll()
        {
            return _productCategoryRepository.GetAll();
        }
        
        public List<ProductCategory> GetAllOnCategoryId(int id)
        {
            List<ProductCategory> productCategories = _productCategoryRepository.GetAll().Where(c => c.CategoryId == id).ToList();
            FillService(productCategories);
            return productCategories;
        }
        
        public List<ProductCategory> Add(ProductCategory category)
        {
            _productCategoryRepository.Add(category);
            return _productCategoryRepository.GetAll();
        }
        
        private void FillService(List<ProductCategory> productCategory)
        {
            foreach (ProductCategory pc in productCategory)
            {
                pc.Product = _productRepository.Get(pc.ProductId) ?? new(0, "", 0, 0);
            }
        }
    }
}