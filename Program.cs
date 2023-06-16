using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using BlogChampion.Data;
using Microsoft.AspNetCore.Identity;

namespace BlogChampion
{
	public class Program
	{
		public static void Main(string[] args)
		{
			
			var host = CreateWebHostBuilder(args).Build();
            try
            {
                var scope = host.Services.CreateScope();

				var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
				var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
				var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

				context.Database.EnsureCreated();

				var adminRole = new IdentityRole("Admin");
				if (!context.Roles.Any())
				{
					roleManager.CreateAsync(adminRole).GetAwaiter().GetResult();
					//create roles

				}
				if (!context.Users.Any(u => u.UserName == "admin"))
				{
					//create an admin if none exist
					var adminUser = new IdentityUser
					{
						UserName = "admin",
						Email = "chase@test.com"
					};
					userManager.CreateAsync(adminUser, "password").GetAwaiter().GetResult();
					//add roles
					userManager.AddToRoleAsync(adminUser, adminRole.Name);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
            host.Run(); 
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) => 
			WebHost.CreateDefaultBuilder(args)
			.UseStartup<Startup>();
	}
}
