using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.InMemory;
using DAL.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using Services.Services;
using Services.Interfaces;
using DAL.Interfaces;
using DAL.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CleanArchitecture
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();
			services.AddDbContext<AppDbContext>(options=>options.UseInMemoryDatabase("testDb"));
			services.AddScoped<IUserService, UserServices>();
			services.AddScoped<IUserRepositoty, UserRepository>();
			services.AddScoped<JwtTokenService>();
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.TokenValidationParameters = new TokenValidationParameters
					{
						// �z�L�o���ŧi�A�N�i�H�q "sub" ���Ȩó]�w�� User.Identity.Name
						NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",
						// �z�L�o���ŧi�A�N�i�H�q "roles" ���ȡA�åi�� [Authorize] �P�_����
						RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",

						// �@��ڭ̳��|���� Issuer
						ValidateIssuer = true,
						ValidIssuer = Configuration.GetValue<string>("JwtSettings:Issuer"),

						// �q�`���ӻݭn���� Audience
						ValidateAudience = false,
						//ValidAudience = "JwtAuthDemo", // �����ҴN���ݭn��g

						// �@��ڭ̳��|���� Token �����Ĵ���
						ValidateLifetime = true,

						// �p�G Token ���]�t key �~�ݭn���ҡA�@�볣�u��ñ���Ӥw
						ValidateIssuerSigningKey = false,

						// "1234567890123456" ���ӱq IConfiguration ���o
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetValue<string>("JwtSettings:SignKey")))
					};
				});

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthentication();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}