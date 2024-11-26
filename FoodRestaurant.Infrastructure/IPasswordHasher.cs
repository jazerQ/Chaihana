namespace FoodRestaurant.Infrastructure
{
	public interface IPasswordHasher
	{
		string Generate(string password);
	}
}