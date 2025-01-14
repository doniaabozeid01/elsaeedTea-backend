using elsaeedTea.data.Context;
using elsaeedTea.repository.Interfaces;
using elsaeedTea.repository.Repositories;
using elsaeedTea.service.Services.teaImagesServices;
using elsaeedTea.service.Services.teaImagesServices.Dtos;

//using elsaeedTea.service.Services.teaImagesServices.Dtos;
using elsaeedTea.service.Services.teaProductServices;
using elsaeedTea.service.Services.teaProductServices.Dtos;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<ElsaeedTeaDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ITeaServices, TeaServices>();
builder.Services.AddScoped<IImageServices, ImageServices>();


builder.Services.AddAutoMapper(typeof(ProductProfile));
builder.Services.AddAutoMapper(typeof(ImageProfile));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles(); 

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
