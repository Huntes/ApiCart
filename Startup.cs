using ApiCart.Domain;
using ApiCart.Repositories;
using ApiCart.Repositories.Interfaces;
using ApiCart.Services;
using ApiCart.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace ApiCart
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
           
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            DependencyInjections(services);


            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "SHOP MAIS API",
                    Description = "Documentação de api para cadastro de compras",
                    Version = "v1"
                });
            });
            #endregion

            services.AddControllers();
            services.AddCors();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Shop Mais API");
                c.RoutePrefix = "swagger";
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void DependencyInjections(IServiceCollection services)
        {
            services.AddSingleton<Context>(); //Dados in memory

            services.AddScoped<ICartService, CartService>();
            services.AddScoped<ICartRepository, CartRepository>();
        }
    }
}
