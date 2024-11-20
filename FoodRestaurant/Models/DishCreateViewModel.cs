namespace FoodRestaurant.Models
{
	public class DishCreateViewModel
	{
		public Dish Dish { get; set; }
		public List<Ingridients> Ingridients { get; set; }
		public List<int> SelectedIngridientsId { get; set; } // список выбранных ингридиентов
	}
}
