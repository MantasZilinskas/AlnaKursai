using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using TodoApp.Buisiness.Interfaces;
using TodoApp.Buisiness.Models;
using TodoApp.Buisiness.Services;
using TodoApp.Data.Context;
using TodoApp.Data.Interfaces;
using TodoApp.Data.Models;
using TodoApp.Data.Providers;

namespace TodoApp.Web
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
            services.AddControllersWithViews();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSingleton(typeof(IDataService<>), typeof(DataService<>));
            services.AddSingleton(typeof(IDataProvider<>), typeof(InMemoryDataProvider<>));

            services.AddScoped<IAsyncDataService<TodoItemVO>, TodoItemService>();
            services.AddScoped<IAsyncDataService<CategoryVO>, CategoryService>();
            services.AddScoped<IAsyncDataService<TagVO>, TagService>();
            services.AddScoped<IItemTagService, ItemTagService>();

            services.AddScoped<IAsyncDataProvider<TodoItemDAO>, TodoItemProvider>();
            services.AddScoped<IAsyncDataProvider<CategoryDAO>, CategoryProvider>();
            services.AddScoped<IAsyncDataProvider<TagDAO>, TagProvider>();
            services.AddScoped<IItemTagProvider, ItemTagProvider>();



            services.AddDbContext<TodoAppContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("ToDoAppContext")));
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

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=TodoItemAdmin}/{action=Index}/{id?}");
            });
        }
    }
}
