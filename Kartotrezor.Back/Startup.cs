using Kartotrezor.Back.Services;

namespace Kartotrezor.Back
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

            services.AddCors();
            services.AddMvc();
            services.AddMemoryCache();
            
            services.AddTransient<ICacheMap, MapCacher>()
                .AddSingleton<MapCacher>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UsePathBase("/api");

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            app.UseCors(
                builder => builder
                 .AllowAnyOrigin()
                 .AllowAnyMethod()
                 .AllowAnyHeader()
            );

            app.UseRouting();

            app.UseAuthorization();

            app.UseDefaultFiles();

            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
