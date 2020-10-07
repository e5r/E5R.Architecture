using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UsingDataEntityFrameworkCore.Data;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using Microsoft.Data.Sqlite;
using E5R.Architecture.Infrastructure.Abstractions;
using E5R.Architecture.Infrastructure;
using System.Data;
using E5R.Architecture.Data.Abstractions;
using E5R.Architecture.Data.EntityFrameworkCore;

namespace UsingDataEntityFrameworkCore
{
    public class DefaultStorageReader<TDataModel>
        : StorageReaderByProperty<SchoolContext, TDataModel>
        where TDataModel : class, IDataModel
    {
        public DefaultStorageReader(UnitOfWorkProperty<SchoolContext> context)
            : base(context) { }
    }

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
            services.AddControllersWithViews();

            #region services.AddPropertyUnitOfWork(options);
            services.AddScoped<DbConnection, SqliteConnection>(_ =>
            {
                var connectionString = Configuration.GetConnectionString("SQLiteConnection");
                return new SqliteConnection(connectionString);
            });

            services.AddDbContext<SchoolContext>((serviceProvider, options) =>
            {
                var conn = serviceProvider.GetRequiredService<DbConnection>();

                options.UseSqlite(conn);
            });

            services.AddScoped<AppUnitOfWork>();
            services.AddScoped<IUnitOfWork>(serviceProvider =>
            {
                var uow = serviceProvider.GetRequiredService<AppUnitOfWork>();

                return uow;
            });
            services.AddScoped(typeof(UnitOfWorkProperty<>));
            //StorageReaderByProperty<SchoolContext>
            services.AddScoped(typeof(IStorageReader<>), typeof(DefaultStorageReader<>));

            services.AddScoped<UnitOfWorkByProperty>(serviceProvider =>
            {
                DbTransaction transaction = null;
                var conn = serviceProvider.GetRequiredService<DbConnection>();
                var context = serviceProvider.GetRequiredService<SchoolContext>();
                var uow = serviceProvider.GetRequiredService<AppUnitOfWork>();

                uow.Property(() => conn);
                uow.Property(() =>
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }

                    if (transaction == null)
                    {
                        transaction = conn.BeginTransaction();
                    }

                    return transaction;
                });
                uow.Property(() =>
                {
                    if (context.Database.CurrentTransaction == null)
                    {
                        context.Database.UseTransaction(uow.Property<DbTransaction>());
                    }

                    return context;
                });

                return uow;
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
