﻿using BlogChampion.Data;
using BlogChampion.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
namespace BlogChampion
{
	public class Startup
	{
		private IConfiguration _config;
        public Startup(IConfiguration config)
		{
			_config = config;
		}
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<AppDbContext>(options => options.UseSqlServer(_config["DefaultConnection"]));

            services.AddDefaultIdentity<IdentityUser>(options =>
			{
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireDigit = false;
				options.Password.RequireLowercase = false;
				options.Password.RequireUppercase = false;
				options.Password.RequiredLength = 6;
			})
                .AddRoles<IdentityRole>()
				.AddEntityFrameworkStores<AppDbContext>();

            services.AddTransient<IRepository, Repository>();
		
			services.AddMvc(options => options.EnableEndpointRouting = false);



		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if(env.IsDevelopment()) { 
				app.UseDeveloperExceptionPage();
			}
			app.UseStaticFiles();
			app.UseAuthentication(); 

			app.UseMvcWithDefaultRoute();
		}
	}
}
