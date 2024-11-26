namespace FoodRestaurant.Middlewares
{
	public class LogMiddleware
	{
		private readonly RequestDelegate next;

		public LogMiddleware(RequestDelegate next)
		{
			this.next = next;
		}
		public async Task InvokeAsync(HttpContext context) 
		{
			var log = new LoggerInfo(context.Request.Path, context.Request.Method);
			await next.Invoke(context);
			log.CalculateDifference();
			Console.WriteLine($"PATH - {log.path}, METHOD - {log.method}, TIMEDIFF - {log.timeRequest}");
		}
	}
}
class LoggerInfo 
{
	public string path { get; set; }
	public string method { get; set; }
	public TimeSpan timeRequest { get; set; }
	private DateTime timeStart { get; set; }
	public LoggerInfo(string path, string method) {
		this.path = path;
		this.method = method;
		this.timeStart = DateTime.Now; 
	}
	public void CalculateDifference() 
	{
		timeRequest = DateTime.Now.Subtract(timeStart);
	} 
}
