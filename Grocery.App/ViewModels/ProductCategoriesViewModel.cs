using CommunityToolkit.Mvvm.ComponentModel;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;

namespace Grocery.App.ViewModels
{
    [QueryProperty(nameof(Category), nameof(Category))]
    public partial class ProductCategoriesViewModel : BaseViewModel
    {
        private readonly IProductCategoryService _productCategoryService;
        private readonly IProductService _productService;
        public ObservableCollection<ProductCategory> ProductCategories { get; set; } = [];
        
        public ObservableCollection<Product> AvailableProducts { get; set; } = [];

        private string searchText = "";
        
        [ObservableProperty] 
        private Category category = new(0, "None");

        public ProductCategoriesViewModel(IProductCategoryService productCategoryService, IProductService productService)
        {
            _productCategoryService = productCategoryService;
            _productService = productService;
        }

        public void LoadProductCategoriesByCategory(Category category)
        {
            ProductCategories.Clear();

            var productCategories = _productCategoryService.GetAllOnCategoryId(category.Id);
            foreach (var productCategory in productCategories)
                ProductCategories.Add(productCategory);
        }
        
        private void GetAvailableProducts()
        {
            AvailableProducts.Clear();
            foreach (Product p in _productService.GetAll())
                if (ProductCategories.FirstOrDefault(g => g.ProductId == p.Id) == null  && p.Stock > 0 && (searchText=="" || p.Name.ToLower().Contains(searchText.ToLower())))
                    AvailableProducts.Add(p);
        }
        
        [RelayCommand]
        public void PerformSearch(string searchText)
        {
            this.searchText = searchText;
            GetAvailableProducts();
        }
        partial void OnCategoryChanged(Category value)
        {
            LoadProductCategoriesByCategory(value);
            GetAvailableProducts();
        }
        
        [RelayCommand]
        public void AddProduct(Product product)
        {
            if (product == null) return;
            ProductCategory category = new(0, "", product.Id, Category.Id);
            _productCategoryService.Add(category);
            product.Stock--;
            _productService.Update(product);
            AvailableProducts.Remove(product);
            OnCategoryChanged(Category);
        }
    }
}