namespace FoodRestaurant.Models
{
	public class Ingridients
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public List<DishIngridient> DishIngridients { get; set; }
	}
}
