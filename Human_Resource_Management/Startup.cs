using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Human_Resource_Management.Models;
using Human_Resource_Management.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Human_Resource_Management
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
            services.AddMvc();

            services.AddTransient<ICompany, CompanyService>();//transient means instance is created everytime requested and never shared
            services.AddTransient<ISubCompany, SubCompanyService>();//transient means instance is created everytime requested and never shared
            services.AddTransient<IProject, ProjectService>();//transient means instance is created everytime requested and never shared
            services.AddTransient<IPosition, PositionService>();//transient means instance is created everytime requested and never shared
            services.AddTransient<IEmployee, EmployeeService>();//transient means instance is created everytime requested and never shared
            services.AddTransient<IEmployeeProject, EmployeeProjectService>();//transient means instance is created everytime requested and never shared

            //this is for creating data base for Human Resource Management
            services.AddDbContext<ManagementContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Company}/{action=Index}/{id?}");
            });

        }
    }
}
