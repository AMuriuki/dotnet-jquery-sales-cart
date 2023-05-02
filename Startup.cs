using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using sales_invoicing_dotnet.Data;

namespace sales_invoicing_dotnet
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<Data.SalesContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("SalesContext")));
            services.AddControllersWithViews();
        }

        public class SalesContextFactory : IDesignTimeDbContextFactory<SalesContext>
        {
            public SalesContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<SalesContext>();
                optionsBuilder.UseSqlite("Data Source=sales.db");

                return new SalesContext(optionsBuilder.Options);
            }
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Sales/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Sales}/{action=Index}/{id?}");
            });
        }
    }
}