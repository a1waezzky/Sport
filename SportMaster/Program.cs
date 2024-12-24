

using Microsoft.EntityFrameworkCore;
using SportMaster.Models;

namespace SportMaster
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Настройка HTTPS
            builder.WebHost.UseKestrel(options =>
            {
                options.ListenAnyIP(5001, listenOptions =>
                {
                    listenOptions.UseHttps();
                });
            });

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

            app.UseHttpsRedirection(); // Перенаправление HTTP на HTTPS
            app.UseAuthorization();

            // Использование CORS
            app.UseCors("MyPolicy");

            // Регистрация контроллеров
            app.MapControllers();

            app.Run();
        }
    }
}