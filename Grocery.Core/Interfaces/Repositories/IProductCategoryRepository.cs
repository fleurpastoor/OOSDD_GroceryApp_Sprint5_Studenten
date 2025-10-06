using Grocery.Core.Models;

namespace Grocery.Core.Interfaces.Repositories
{

    public interface IProductCategoryRepository
    {
        public List<ProductCategory> GetAll();
    }
}