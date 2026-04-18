
using System.Text;
using Mattger_BL.Helpers;
using Mattger_BL.IServices;
using Mattger_BL.Services;
using Mattger_DAL.Data;
using Mattger_DAL.Entities;
using Mattger_DAL.IRepos;
using Mattger_DAL.Repos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Mattger_PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<MattgerDBContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            // قراءة Connection String من appsettings.json أو Environment Variable
//            var connectionString = Environment.GetEnvironmentVariable("MYSQL_URL");
//if (string.IsNullOrEmpty(connectionString))
//    throw new Exception("MYSQL_URL not found!");

// استخدمه في DbContext
//builder.Services.AddDbContext<MattgerDBContext>(options =>
//    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// تسجيل DbContext
//builder.Services.AddDbContext<MattgerDBContext>(options =>
//    options.UseSqlServer(connectionString));
            builder.Services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRep<>));

            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IBrandService, BrandService>();
            builder.Services.AddScoped<ITypeService, TypeService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<ICartService, CartService>(); 
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IWishlistService, WishlistService>();
            builder.Services.AddScoped<ICouponService, CouponService>();
            builder.Services.AddScoped<IProductReviewService, ProductReviewService>();
            builder.Services.AddScoped<IPaymentMethodService, PaymentMethodService>();
            builder.Services.AddScoped<JwtService>();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //app settings
            builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));


            //identity
            builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<MattgerDBContext>()
                .AddDefaultTokenProviders();


            //jwt
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
                    )
                };
            });
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngular",
                    policy =>
                    {
                        policy
                            //.WithOrigins("http://localhost:4200")
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            var app = builder.Build();
            app.UseHttpsRedirection();
            app.UseCors("AllowAngular");
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
