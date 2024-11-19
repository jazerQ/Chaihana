using FoodRestaurant.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodRestaurant.Data
{
	public class MenuContext : DbContext
	{
		public MenuContext(DbContextOptions<MenuContext> options) : base(options) { }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<DishIngridient>().HasKey(di => new 
			{
				di.DishId,
				di.IngridiendId
			});
			modelBuilder.Entity<DishIngridient>().HasOne(d => d.Dish).WithMany(di => di.DishIngridients).HasForeignKey(d => d.DishId);
			modelBuilder.Entity<DishIngridient>().HasOne(i => i.Ingridients).WithMany(di => di.DishIngridients).HasForeignKey(i => i.IngridiendId);

			modelBuilder.Entity<Dish>().HasData
				(
				new Dish { Id = 1, Name = "Pilaf", price = 500, imageUrl = @"https://happylates.com/wp-content/uploads/2014/11/tashkentskiy-plov-1.jpg" }
				);
			modelBuilder.Entity<Ingridients>().HasData(
				new Ingridients { Id = 1, Name = "Mutton" },
				new Ingridients { Id = 2, Name = "Rice" },
				new Ingridients { Id = 3, Name = "Carrot" },
				new Ingridients { Id = 4, Name = "Onion" },
				new Ingridients { Id = 5, Name = "Garlic" },
				new Ingridients { Id = 6, Name = "Cumin" }
				);
			modelBuilder.Entity<DishIngridient>().HasData(
				new DishIngridient { Id = 1, DishId = 1, IngridiendId = 1 },
				new DishIngridient { Id = 2, DishId = 1, IngridiendId = 2 },
				new DishIngridient { Id = 3, DishId = 1, IngridiendId = 3 },
				new DishIngridient { Id = 4, DishId = 1, IngridiendId = 4 },
				new DishIngridient { Id = 5, DishId = 1, IngridiendId = 5 },
				new DishIngridient { Id = 6, DishId = 1, IngridiendId = 6 }
				);
			base.OnModelCreating(modelBuilder);
		}
		public DbSet<Dish> Dishes { get; set; }
		public DbSet<Ingridients> Ingridients { get; set; }
		public DbSet<DishIngridient> DishIngridients { get; set; }
	}
}
