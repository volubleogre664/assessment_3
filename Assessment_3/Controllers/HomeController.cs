namespace Assessment_3.Controllers
{
    using Assessment_3.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    
    public class HomeController : Controller
    {
        private readonly ICategoriesService categoriesService;

        public HomeController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public IActionResult Index()
        {
            this.ViewBag.Categories = this.categoriesService.GetAll();
            return View();
        }
    }
}
