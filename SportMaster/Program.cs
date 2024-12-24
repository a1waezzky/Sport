

using Microsoft.EntityFrameworkCore;
using SportMaster.Models;

namespace SportMaster
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ��������� HTTPS
            builder.WebHost.UseKestrel(options =>
            {
                options.ListenAnyIP(5001, listenOptions =>
                {
                    listenOptions.UseHttps();
                });
            });

            // ��������� CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            // ���������� ������������
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // ��������� HTTP-���������
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection(); // ��������������� HTTP �� HTTPS
            app.UseAuthorization();

            // ������������� CORS
            app.UseCors("MyPolicy");

            // ����������� ������������
            app.MapControllers();

            app.Run();
        }
    }
}