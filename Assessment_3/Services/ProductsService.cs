namespace Assessment_3.Services
{
    using Assessment_3.Data;
    using Assessment_3.Interfaces;
    using Assessment_3.Models;

    public class ProductsService : GenericService<Product>, IProductsService
    {
        public ProductsService(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
