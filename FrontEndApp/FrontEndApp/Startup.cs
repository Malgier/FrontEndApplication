using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.IO;

namespace FrontEndApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile(String.Format("appsettings.{0}.json", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")), optional: true)
                .AddJsonFile(String.Format("appsettings.{0}.json", Environment.GetEnvironmentVariable("AppSettings")), optional: true)
                .Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton(Configuration);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MY TOP SECRET TEST KEY")),
                ValidateIssuer = true,
                ValidIssuer = "issuer",
                ValidateAudience = true,
                ValidAudience = "audience",
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = tokenValidationParameters;
                o.RequireHttpsMetadata = false;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                //app.Use(async (context, next) =>
                //{
                //    //Fake token
                //    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MY TOP SECRET TEST KEY"));
                //    var claims = new Claim[]
                //    {
                //        new Claim(ClaimTypes.NameIdentifier, "1"),
                //        new Claim(ClaimTypes.Role, "Customer")
                //    };

                //    var token = new JwtSecurityToken(
                //        issuer: "issuer",
                //        audience: "audience",
                //        claims: claims,
                //        notBefore: DateTime.Now.Subtract(new TimeSpan(2, 1, 1)),
                //        expires: DateTime.Now.AddDays(7),
                //        signingCredentials: new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256)
                //    );

                //    context.Request.Headers.Add("Authorization", "Bearer " + new JwtSecurityTokenHandler().WriteToken(token));

                //    await next.Invoke();
                //});
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseMiddleware<JWTCookieToHeader>();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
