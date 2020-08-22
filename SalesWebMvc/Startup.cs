using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Services;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

namespace SalesWebMvc
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.

                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);


            services.AddDbContext<SalesWebMvcContext>(options =>
                    options.UseMySql(Configuration.GetConnectionString("SalesWebMvcContext"), builder =>
                    builder.MigrationsAssembly("SalesWebMvc")));
            
            
            services.AddScoped<SeedingService>(); //add classe na injeção de dep da aplicaçao
            services.AddScoped<SellerService>(); //  serviço pode ser injetado em outras classes
            services.AddScoped<DepartmentService>();
            services.AddScoped<SalesRecordService>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, SeedingService seedingService)
        {

            //definir configurações de localização, usaremos a do Estados Unidos
            var enUS = new CultureInfo("en-US");
            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(enUS), //qual será o lacation padrão da apliação (enUS)
                SupportedCultures = new List<CultureInfo> { enUS }, //quais são as location possíveis para a minha apliação: por enquanto só o enUs
                SupportedUICultures = new List<CultureInfo> { enUS }
            };
            //definido as configurações de localização
            app.UseRequestLocalization(localizationOptions);



            if (env.IsDevelopment()) //se estiver no perfil de desenvolvimento
            {
                app.UseDeveloperExceptionPage();
                seedingService.Seed(); //metodo que popula a bsase
            }
            else //se estiver em outro perfil
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                //define a pagina padrão/index se não for passado outro caminho na rota
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
