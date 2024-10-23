using LinkDev.IKEA.BLL.Common.Services.Attachments;
using LinkDev.IKEA.BLL.Services.Departments;
using LinkDev.IKEA.BLL.Services.Employees;
using LinkDev.IKEA.DAL.Models.Identity;
using LinkDev.IKEA.DAL.Perisistance.Data;
using LinkDev.IKEA.DAL.Perisistance.UnitOfWork;
using LinkDev.IKEA.PL.Maill_Settings;
using LinkDev.IKEA.PL.Mapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.IKEA.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Service

            // Add services to the container.
            builder.Services.AddControllersWithViews();


            builder.Services.AddDbContext<ApplicationDbContext>(
                options => 
                {
                    options
                    .UseLazyLoadingProxies()
                    .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                }
            );


            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddTransient<IAttachmentService, AttachmentService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services
                .AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredUniqueChars = 1;

                options.User.RequireUniqueEmail = true;

                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(5);
                options.Lockout.MaxFailedAccessAttempts = 5;

			})
                .AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders();

            builder.Services.AddScoped<MaillSender, MaillSender>();
            builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfile()));


            // Add New Schema
            builder.Services
            .AddAuthentication(config =>
            {
                config.DefaultChallengeScheme = "Identity.Application";
            })
            .AddCookie("Hamda", options =>
            {
                options.AccessDeniedPath = "/Home/Error";
                options.LoginPath = "/Account/SignIn";
                options.ExpireTimeSpan = TimeSpan.FromDays(5);
            });


            // Configure Application Cookie
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Home/Error";
                options.Events.OnRedirectToLogout = 
                // BUG
                context =>
                    {
                        context.Response.Redirect("/Account/SignIn");
                        return Task.CompletedTask;
                    };
				options.LoginPath =  "/Account/SignIn";
                options.ExpireTimeSpan = TimeSpan.FromDays(5);
			});

            
            #endregion

            var app = builder.Build();

            #region Configure
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            #endregion
            
            app.Run();
        }
    }
}