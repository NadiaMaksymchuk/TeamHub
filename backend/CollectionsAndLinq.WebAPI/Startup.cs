using CollectionsAndLinq.DAL.Context;
using CollectionsAndLinq.WebAPI.Extensions;
using CollectionsAndLinq.WebAPI.Filters;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

namespace CollectionsAndLinq.WebAPI
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
            services.AddMvc(option => option.EnableEndpointRouting = false);

            services.AddDbContext<ProjectDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("ProjectAccountingDataBase"));
                options.EnableSensitiveDataLogging(true);
            });

            services.RegisterCustomServices();

            services.RegisterAutoMapper();

            services.RegisterCustomValidators();

            services
                .AddMvcCore(options => options.Filters.Add(typeof(CustomExceptionFilterAttribute)))
                .AddFluentValidation();

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.ConfigureCustomValidationErrors();
        }

        public void Configure(IApplicationBuilder app, Microsoft.Extensions.Hosting.IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors(builder => builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .WithOrigins("http://localhost:4200"));

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseRouting();

            app.UseEndpoints(cfg =>
            {
                cfg.MapControllers();
            });

            InitializeDatabase(app);
        }

        private static void InitializeDatabase(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                using var context = scope.ServiceProvider.GetRequiredService<ProjectDbContext>();
                context.Database.Migrate();
            };
        }
    }
}
