using CovidMap.Hubs;
using CovidMap.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CovidMap
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
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration["ConStr"]);
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CovidMap", Version = "v1" });
            });

            //API projemizi dýþarýya açacak ayarlarý tanýmladýk
            services.AddCors(options =>
            {
                //WithOrigins parametresiyle hangi web uygulamalarý api ye eriþebilir karar veriyorum
                options.AddPolicy("MyCors", builder => { builder.WithOrigins("https://localhost:44369","http://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials(); });
            });

            //Projeye SignalR ekledik
            services.AddSignalR();

            //DI ile kullanabilmek için ekledik
            services.AddScoped<CovidService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CovidMap v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //Oluþturduðumuz Policy i projeye tanýttýk
            app.UseCors("MyCors");

            app.UseAuthorization();

            

            app.UseEndpoints(endpoints =>
            {
                //Hub End pointi tanýmlandý
                endpoints.MapHub<CovidHub>("/MyHub");
                endpoints.MapControllers();
            });
        }
    }
}
