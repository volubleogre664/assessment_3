namespace Assessment_3.Services
{
    using Assessment_3.Data;
    using Assessment_3.Interfaces;
    using Assessment_3.Models;

    public class CategoriesService : GenericService<Category>, ICategoriesService
    {
        private readonly ApplicationDbContext context;

        public CategoriesService(ApplicationDbContext context)
            : base(context)
        {
            this.context = context;
        }

        public new List<Category> GetAll()
        {
            var categories = base.GetAll();

            foreach (var category in categories)
            {
                category.CategoryQuantity = this.context.Product.Count(_ => _.CategoryId == category.CategoryId);
            }

            return categories;
        }
    }
}
