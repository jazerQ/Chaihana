using Microsoft.AspNetCore.Mvc;

namespace FoodRestaurant.Controllers
{
	public class IngredientController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
