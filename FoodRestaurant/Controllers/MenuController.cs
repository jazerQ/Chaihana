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
		/*public async Task<IActionResult> CreateIngredient() 
		{
			Ingridients ingridient = new Ingridients { DishIngridients = await _menuContext.DishIngridients.ToListAsync() };
			return View(ingridient);
		}*/
		[HttpPost]
		public async Task<IActionResult> CreateIngredient([Bind("Id, Name, DishIngridients")]Ingridients ingridients) 
		{
			/*if (!ModelState.IsValid) 
			{
				foreach(var err in ModelState.Values.SelectMany(e => e.Errors)) 
				{
					Console.WriteLine(err.ErrorMessage);
				}
			}*/
			if (string.IsNullOrEmpty(ingridients.Name))
			{
				return View(ingridients);
			}
			await _menuContext.Ingridients.AddAsync(ingridients);
			await _menuContext.SaveChangesAsync();
			return RedirectToAction("Index");
			
		}
		public async Task<IActionResult> Delete(int id) 
		{
			Dish? dish = await _menuContext.Dishes.FirstOrDefaultAsync(x => x.Id == id);
			return View(dish);

		}
		/*public async Task<IActionResult> DeleteIngredient()*/
		public async Task<IActionResult> DeleteConfirmed(int id) 
		{
			var item = await _menuContext.Dishes.FindAsync(id);
			if (item != null) 
			{
				_menuContext.Remove(item);
				await _menuContext.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			return RedirectToAction("Delete", id);
		}
		public async Task<IActionResult> Update(int id) 
		{
			Dish? item = _menuContext.Dishes.FirstOrDefault(x => x.Id == id);
			if (item != null)
			{
				List<int> selectIng = new List<int>();
				foreach(var di in _menuContext.DishIngridients) 
				{
					if (item.Id == di.DishId) {
						selectIng.Add(di.IngridiendId);
					}
				}
				DishCreateViewModel dishCreateViewModel = new DishCreateViewModel { Dish = item, Ingridients = await _menuContext.Ingridients.ToListAsync(), SelectedIngridientsId = selectIng };
				return View(dishCreateViewModel);
			}
			return RedirectToAction("Index");
		}

		[HttpPost]
		public async Task<IActionResult> Update(DishCreateViewModel dishCreateViewModel) 
		{

			_menuContext.Dishes.Update(dishCreateViewModel.Dish);
			
			
			var dishIngredients = await _menuContext.DishIngridients.Where(di => di.DishId == dishCreateViewModel.Dish.Id).ToListAsync();



			foreach (var ingridientId in dishCreateViewModel.SelectedIngridientsId)
			{
				var currentDi = dishIngredients.FirstOrDefault(di => di.IngridiendId == ingridientId);
				if (currentDi == null)
				{
					_menuContext.DishIngridients.Add(new DishIngridient { IngridiendId = ingridientId, DishId = dishCreateViewModel.Dish.Id });

				}
				
			}
			var RemoveDi = await _menuContext.DishIngridients.Where(x => x.DishId == dishCreateViewModel.Dish.Id && !dishCreateViewModel.SelectedIngridientsId.Contains(x.IngridiendId)).ToListAsync();
			_menuContext.DishIngridients.RemoveRange(RemoveDi);
			await _menuContext.SaveChangesAsync();
			return RedirectToAction("Index");
			
		}
	}
}
