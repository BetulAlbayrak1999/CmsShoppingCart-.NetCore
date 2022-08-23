using Microsoft.AspNetCore.Mvc;

namespace CmsShoppingCartMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PagesController : Controller
    {
        public string Index()
        {
            return "test";
        }
    }
}
