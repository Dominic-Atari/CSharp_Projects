namespace Dominic.Net.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DominicShopDbContext _context;

        public CategoryRepository(DominicShopDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> AllCategories
        {
            get
            {
                return _context.Categories.OrderBy(c => c.CategoryName);
            }
        }
    }
}