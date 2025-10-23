
using Microsoft.EntityFrameworkCore;

namespace Dominic.Net.Models
{
    public class PieRepository : IPieRepository
    {
        private readonly DominicShopDbContext _context;

        public PieRepository(DominicShopDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Pie> AllPies
        {
            get
            {
                return _context.Pies.Include(p => p.Category);
            }
        }

        public IEnumerable<Pie> PiesOfWeek
        {
            get
            {
                return _context.Pies.Include(p => p.Category).Where(p => p.IsPieOfTheWeek);
            }
        }

        public Pie? GetPieById(int pieId)
        {
            return _context.Pies.Include(p => p.Category).FirstOrDefault(p => p.PieId == pieId);
        }
    }
}