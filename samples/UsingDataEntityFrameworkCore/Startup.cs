using System.Data;
using System.Data.Common;
using E5R.Architecture.Data.Abstractions;
using E5R.Architecture.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UsingDataEntityFrameworkCore.Data;

namespace UsingDataEntityFrameworkCore
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
            services.AddControllersWithViews();

            // Strategy.ByProperty
            services.AddScoped<DbConnection>(_ =>
                new SqliteConnection(Configuration.GetConnectionString("SQLiteConnection"))
            );

            services.AddDbContext<SchoolContext>((serviceProvider, options) =>
            {
                var conn = serviceProvider.GetRequiredService<DbConnection>();

                options.UseSqlite(conn);
            });

            services.AddScoped<DbContext>(serviceProvider => serviceProvider.GetRequiredService<SchoolContext>());

            services.AddUnitOfWorkPropertyStrategy(options =>
            {
                options
                    .AddProperty<DbContext>()
                    .AddProperty<SchoolContext>((uow, context) =>
                    {
                        if (context.Database.CurrentTransaction == null)
                        {
                            context.Database.UseTransaction(uow.Property<DbTransaction>());
                        }
                    });
            });

            services.AddStoragePropertyStrategy();
            services.AddInfrastructure(Configuration);

            // Strategy.TransactionScope
            //services.AddDbContext<SchoolContext>((serviceProvider, options) =>
            //{
            //    options.UseSqlite(Configuration.GetConnectionString("SQLiteConnection"));
            //});
            //services.AddScoped<DbContext>(ServiceProvider => ServiceProvider.GetRequiredService<SchoolContext>());
            //services.AddUnitOfWorkTransactionScopeStrategy();
            //services.AddStorageTransactionScopeStrategy();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseUnitOfWork();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
