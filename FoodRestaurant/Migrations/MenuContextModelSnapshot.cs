﻿// <auto-generated />
using FoodRestaurant.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FoodRestaurant.Migrations
{
    [DbContext(typeof(MenuContext))]
    partial class MenuContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FoodRestaurant.Models.Dish", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("imageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("price")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Dishes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Pilaf",
                            imageUrl = "https://happylates.com/wp-content/uploads/2014/11/tashkentskiy-plov-1.jpg",
                            price = 500.0
                        });
                });

            modelBuilder.Entity("FoodRestaurant.Models.DishIngridient", b =>
                {
                    b.Property<int>("DishId")
                        .HasColumnType("int");

                    b.Property<int>("IngridiendId")
                        .HasColumnType("int");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.HasKey("DishId", "IngridiendId");

                    b.HasIndex("IngridiendId");

                    b.ToTable("DishIngridients");

                    b.HasData(
                        new
                        {
                            DishId = 1,
                            IngridiendId = 1,
                            Id = 1
                        },
                        new
                        {
                            DishId = 1,
                            IngridiendId = 2,
                            Id = 2
                        },
                        new
                        {
                            DishId = 1,
                            IngridiendId = 3,
                            Id = 3
                        },
                        new
                        {
                            DishId = 1,
                            IngridiendId = 4,
                            Id = 4
                        },
                        new
                        {
                            DishId = 1,
                            IngridiendId = 5,
                            Id = 5
                        },
                        new
                        {
                            DishId = 1,
                            IngridiendId = 6,
                            Id = 6
                        });
                });

            modelBuilder.Entity("FoodRestaurant.Models.Ingridients", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Ingridients");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Mutton"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Rice"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Carrot"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Onion"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Garlic"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Cumin"
                        });
                });

            modelBuilder.Entity("FoodRestaurant.Models.DishIngridient", b =>
                {
                    b.HasOne("FoodRestaurant.Models.Dish", "Dish")
                        .WithMany("DishIngridients")
                        .HasForeignKey("DishId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FoodRestaurant.Models.Ingridients", "Ingridients")
                        .WithMany("DishIngridients")
                        .HasForeignKey("IngridiendId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dish");

                    b.Navigation("Ingridients");
                });

            modelBuilder.Entity("FoodRestaurant.Models.Dish", b =>
                {
                    b.Navigation("DishIngridients");
                });

            modelBuilder.Entity("FoodRestaurant.Models.Ingridients", b =>
                {
                    b.Navigation("DishIngridients");
                });
#pragma warning restore 612, 618
        }
    }
}
