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
			ViewData["Message"] = searchString;
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
		public async Task<IActionResult> Create() 
		{
			DishCreateViewModel dishCreateViewModel = new DishCreateViewModel
			{
				Dish = new Dish(),
				Ingridients = await _menuContext.Ingridients.ToListAsync(),

			};
			return View(dishCreateViewModel);
		}
		[HttpPost]
		public async Task<IActionResult> Create(DishCreateViewModel model) 
		{
		
			if (!ModelState.IsValid)
			{
				await _menuContext.Dishes.AddAsync(model.Dish);
				await _menuContext.SaveChangesAsync();
				foreach (int ingridientId in model.SelectedIngridientsId)
				{
					await _menuContext.DishIngridients.AddAsync(new DishIngridient
					{
						DishId = model.Dish.Id,
						IngridiendId = ingridientId
					});
				}
				await _menuContext.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			model.Ingridients = await _menuContext.Ingridients.ToListAsync();
			return View(model);
		}
	}
}
