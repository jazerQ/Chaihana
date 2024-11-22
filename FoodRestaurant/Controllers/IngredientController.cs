using FoodRestaurant.Data;
using FoodRestaurant.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodRestaurant.Controllers
{
	public class IngredientController : Controller
	{
		private readonly MenuContext _menuContext;
		public IngredientController(MenuContext menuContext)
		{
			_menuContext = menuContext;
		}
		public async Task<IActionResult> Index(string searchString)
		{
			var Ingredients = from ing in _menuContext.Ingridients select ing;
			if (!string.IsNullOrEmpty(searchString))
			{
				Ingredients = Ingredients.Where(ing => ing.Name.Contains(searchString));
			}
			ViewData["Message"] = searchString;
			return View(await Ingredients.ToListAsync());
		}
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Create(Ingridients ingridients) 
		{
			if (string.IsNullOrEmpty(ingridients.Name)) 
			{
				return View(ingridients);
			}
			await _menuContext.Ingridients.AddAsync(ingridients);
			await _menuContext.SaveChangesAsync();
			return RedirectToAction("Index");
		} 
	}
}
