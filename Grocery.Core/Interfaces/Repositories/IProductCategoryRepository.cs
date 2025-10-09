using Grocery.Core.Models;

namespace Grocery.Core.Interfaces.Repositories
{

    public interface IProductCategoryRepository
    {
        public List<ProductCategory> GetAll();
        public List<ProductCategory> GetAllOnCategoryId(int id);
        public List<ProductCategory> Add(ProductCategory category);
        
    }
}