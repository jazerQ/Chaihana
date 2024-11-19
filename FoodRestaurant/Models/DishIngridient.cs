namespace FoodRestaurant.Models
{
	public class DishIngridient
	{
		public int Id { get; set; }
		public int DishId { get; set; }
		public Dish Dish { get; set; }
		public int IngridiendId { get; set; }
		public Ingridients Ingridients { get; set; }
	}
}
