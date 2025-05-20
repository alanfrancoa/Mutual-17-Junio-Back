
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Mutual_17_Junio_Back.Data;
using System.Text;

namespace Mutual_17_Junio_Back
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

            //Conexion a la base de datos
            builder.Services.AddDbContext<AppDbContext>(options =>
               options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));//Obtiene la cadena de conexion de la base de datos desde el appsettings.json

            //El CORS(Cross-Origin Resource Sharing) sirve para permitir que tu frontend(React en Mutual.Client)
            //se comunique con tu backend(Mutual.Server)
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("FrontendClient", policy =>
                {
                    policy.WithOrigins("http://localhost:7069")// el origen de React, actualizar con el puerto correcto
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });


            //Configuraciones del JWT
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "MutualAPI",  
                    ValidAudience = "MutualClientes",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("clave-super-secreta-segura-para-jwt-123456"))
                };

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine("Fallo autenticación JWT: " + context.Exception.Message);
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        Console.WriteLine("Token JWT validado correctamente");
                        return Task.CompletedTask;
                    }
                };
            });

            builder.Services.AddAuthorization(); // 👈 Asegurate de incluir esto

            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("FrontendClient");

            app.UseHttpsRedirection();

            app.UseAuthentication();  // 👈 Primero Authentication
            app.UseAuthorization();   // 👈 Luego Authorization

            app.MapControllers();

            app.Run();
        }
    }
}
