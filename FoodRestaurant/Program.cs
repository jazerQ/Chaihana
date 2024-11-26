using Azure;
using FoodRestaurant.Data;
using FoodRestaurant.Middlewares;
using FoodRestaurant.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<MenuContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.Use(async(context, next) => {
	Console.WriteLine("Запрос получен: " + context.Request.Path);
	await next.Invoke();
	Console.WriteLine("ответ отправляется!");
});

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");
/*app.Run(async (context) =>
{
	var path = context.Request.Path;
	var FullPath = $"Views/{path}.cshtml";
	var response = context.Response;

	response.ContentType = "text/html; charset=utf-8";
	if (File.Exists(FullPath))
	{
		await response.SendFileAsync(FullPath);
	}
	else
	{
		response.StatusCode = 404;
		await response.WriteAsJsonAsync("<h2>Not Found!</h2>");

	}
});*/
/*app.Run(async(context) =>
{
	Gun gun = new Gun(9, "Uzi");
	Console.WriteLine($"{gun.Mm} , {gun.Name}");
	context.Response.Headers.ContentType = "application/json; charset=utf-8";
	await context.Response.WriteAsJsonAsync(gun);
});*/
/*app.Run(async (context) =>
{
	//context.Response.Headers.ContentDisposition = "attachment; filename=chillguy.jpg";
	await context.Response.SendFileAsync("chillguy.jpg");
});*/
app.Map("/time", appBuilder => 
{
	var time = DateTime.Now.ToShortTimeString();
	appBuilder.Use(async (context, next) =>
	{
		Console.WriteLine($"TIME: {time}");
		await next.Invoke();
	});
	appBuilder.Run(async (context) => 
	{
		await context.Response.WriteAsync(time);
	
	});
});
app.UseMiddleware<LogMiddleware>();
app.Run();
