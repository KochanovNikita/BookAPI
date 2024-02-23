using BookAPI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;

ApplicationContext db = new ApplicationContext();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BookAPI", Version = "v1" });
    c.EnableAnnotations();
});
builder.Services.AddControllers(options => options.EnableEndpointRouting = true); //Google it
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlite("Data Source=APIDatabase"));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseRouting();

/*app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});*/

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookAPI V1");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.Run();
