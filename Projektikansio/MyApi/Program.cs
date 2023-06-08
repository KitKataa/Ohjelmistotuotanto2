using Microsoft.EntityFrameworkCore;
using MyApi.Data;

namespace MyApi;

// DO NOT remove the Program class declaration or the Main method. These are needed for the tests.

// DO edit the content of Main method to handle the task requirements.
public class Program
{
    public static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        //builder.Services.AddCors(options =>
        //{
        //    options.AddPolicy(name: MyAllowSpecificOrigins,
        //                      policy =>
        //                      {
        //                          policy.WithOrigins("https://localhost:7089", "https://localhost:7180")
        //                          .AllowAnyHeader()
        //                          .AllowAnyMethod();
        //                      });
        //});

        builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
        {
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
            policy.AllowAnyOrigin();
        }));

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<MydbContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("mydb")));

        var app = builder.Build();


        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            SeedData.Initialize(services);
        }

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseCors();


        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}