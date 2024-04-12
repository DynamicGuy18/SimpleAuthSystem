using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AuthUsingIdentity.Data;
using AuthUsingIdentity.Areas.Identity.Data;
namespace AuthUsingIdentity
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
   var connectionString = builder.Configuration.GetConnectionString("AuthDbContextConnection") ?? throw new InvalidOperationException("Connection string 'AuthDbContextConnection' not found.");

   builder.Services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(connectionString));

   builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<AuthDbContext>();
			builder.Services.Configure<IdentityOptions>(options =>
			{
				options.Password.RequireUppercase = false;
			});

			// Add services to the container.
			builder.Services.AddControllersWithViews();
			builder.Services.AddRazorPages();
			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
			}
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}"
				);

			app.MapRazorPages();

			app.Run();
		}
	}
}
