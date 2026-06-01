using Gkts.Karargah;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



string baglantiDizesi = "Host=localhost;Port=5433;Database=TaktikSahaDB;Username={YOUR USERNAME HERE};Password={YOUR PASSWORD HERE}";

builder.Services.AddDbContext<KarargahDbContext>(options =>
    options.UseNpgsql(baglantiDizesi));


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddCors(options =>
{
    options.AddPolicy("HaritaIzni", builder =>
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("HaritaIzni");

app.UseAuthorization();

app.MapControllers();

app.Run();
