

using Dominic.Net.Models;
using Dominic.Net.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Dominic.Net.Controllers
{
    
    public class PieController : Controller
    {
        private readonly IPieRepository _pieRepository;
        public readonly ICategoryRepository _categoryRepository;

        public PieController(IPieRepository pieRepository, ICategoryRepository categoryRepository)
        {
            _pieRepository = pieRepository;
            _categoryRepository = categoryRepository;
        }

        public IActionResult List()
        {
            // ViewBag.CurrentCategory = "Cheese Cakes";
            // return View(_pieRepository.AllPies);
            PieListViewModel pieListViewModel = new PieListViewModel(_pieRepository.AllPies, "All Pies.............. 🫣");
            return View(pieListViewModel);
        }
        public IActionResult Details(int id)
        {
            var pie = _pieRepository.GetPieById(id);
            if (pie == null)
            {
                return NotFound();
            }
            else
            {
                return View(pie);
            }
        }
    }
}