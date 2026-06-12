using Asp.Versioning;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using ShoppingProducts.API;
using ShoppingProducts.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
builder.Services.AddScoped<ProductService>();
builder.Services.AddDbContext<ProductDbContext>(option =>
{
    // option
    // // .UseLazyLoadingProxies()
    // .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    option.UseNpgsql(builder.Configuration.GetConnectionString("postgres"));
});

builder.Services.AddMicrosoftIdentityWebApiAuthentication(builder.Configuration, "AzureAd");

// CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("uiApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// add rabbitmq mass transit
// builder.Services.AddMassTransit(x =>
// {
//     x.UsingRabbitMq((context, config) =>
//     {
//         config.Host("localhost", "/", h =>
//         {
//             h.Username("guest");
//             h.Password("guest");
//         });
//     });
// });

// API versioning
// https://www.milanjovanovic.tech/blog/api-versioning-in-aspnetcore
builder.Services.AddApiVersioning(option =>
{
    option.DefaultApiVersion = new ApiVersion(1);
    option.ReportApiVersions = true;
    option.AssumeDefaultVersionWhenUnspecified = true;
    option.ApiVersionReader = new UrlSegmentApiVersionReader();
    // ApiVersionReader.Combine(
    //     new UrlSegmentApiVersionReader()
    //     // new HeaderApiVersionReader("X-Api-Version")
    // );
})
.AddMvc()
.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'V";
    options.SubstituteApiVersionInUrl = true;
});


var app = builder.Build();

// Return depercated message for old versioned API - creating custom middleware 
// app.

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.UseCors("uiApp");

app.Run();

// for integration testing, we need program class so making ti accessible since by default its internal.
public partial class Program{}