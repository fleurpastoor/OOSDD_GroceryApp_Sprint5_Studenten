using Grocery.Core.Models;


namespace Grocery.Core.Interfaces.Services
{
   public interface IProductCategoryService
   {
       List<ProductCategory> GetAll();
       List<ProductCategory> GetAllOnCategoryId(int id);
       
       public List<ProductCategory> Add(ProductCategory category);
   } 
}

