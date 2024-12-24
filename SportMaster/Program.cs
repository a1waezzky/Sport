

using Microsoft.EntityFrameworkCore;
using SportMaster.Models;

namespace SportMaster
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            var connectionString = builder.Configuration["ConnectionString"];
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("ConnectionString is missing in configuration.");
            }


            builder.Services.AddDbContext<A1waezzkyContext>(
                options => options.UseSqlServer(connectionString));

            // Настройка CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            // Добавление контроллеров
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Настройка HTTP-конвейера
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            // Использование CORS
            app.UseCors("MyPolicy");

            // Регистрация контроллеров
            app.MapControllers();

            app.Run();
        }
    }
}