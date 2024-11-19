using Microsoft.AspNetCore.Mvc;
using FoodRestaurant.Data;
using FoodRestaurant.Models;
using Microsoft.EntityFrameworkCore;




namespace FoodRestaurant.Controllers
{
	public class MenuController : Controller
	{
		private readonly MenuContext _menuContext;
		public MenuController(MenuContext menuContext) 
		{
			_menuContext = menuContext;
		}
		public async Task<IActionResult> Index(string searchString)
		{
			var dishes = from d in _menuContext.Dishes select d;
			if (!string.IsNullOrEmpty(searchString)) 
			{
				dishes = dishes.Where(d => d.Name.Contains(searchString));
			}
			return View(await dishes.ToListAsync());
		}
		public async Task<IActionResult> Details(int? id) 
		{
			var dish = await _menuContext.Dishes
				.Include(di => di.DishIngridients)
				.ThenInclude(i => i.Ingridients)
				.FirstOrDefaultAsync(x => x.Id == id);
			if (dish != null) 
			{
				return View(dish);
			}
			return NotFound();

		}
	}
}
