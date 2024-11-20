namespace FoodRestaurant.Models
{
	public class Dish
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;
		public string imageUrl { get; set; } = null!;
		public double price { get; set; }
		
		public List<DishIngridient> DishIngridients { get; set; }
	}
}
