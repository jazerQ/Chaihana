﻿using FoodRestaurant.Data;
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
		public async Task<IActionResult> Delete(int id)
		{
			var ingredientRemove = await _menuContext.Ingridients.FirstOrDefaultAsync(ing => ing.Id == id);
			return View(ingredientRemove);
		}
		[HttpPost]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var shouldDelete = await _menuContext.Ingridients.FindAsync(id);
			if (shouldDelete != null)
			{
				_menuContext.Remove(shouldDelete);
				await _menuContext.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			return RedirectToAction("Delete", id);
		}
		public IActionResult Update(int id)
		{
			var ing = _menuContext.Ingridients.FirstOrDefault(x => x.Id == id);
			if (ing != null) {
				return View(ing);
			}
			return RedirectToAction("Index");
		}
		[HttpPost]
		public async Task<IActionResult> Update(Ingridients ingridients) 
		{
			_menuContext.Ingridients.Update(ingridients);
			await _menuContext.SaveChangesAsync();
			return RedirectToAction("Index");
			
			

		}
	}
}
