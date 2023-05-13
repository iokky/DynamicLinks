using DynamicLinks.Dal;
using DynamicLinks.Dal.Repositories;
using DynamicLinks.Dal.Repositories.Interfaces;
using DynamicLinks.Domain.Entity;
using DynamicLinks.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
//builder.WebHost.UseUrls("https://[::]:33333");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
    options.MapType<Uri>(() => new OpenApiSchema
    {
        Type = "string",
        Example = new OpenApiString("https://excample.com")
    })
);


/*ADD DB CONTEXT*/
builder.Services.AddDbContext<DynamicLinksDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DynamicLinksDb")
));


/*ADD REDIS*/
builder.Services.AddScoped<ICacheService<DynamicLinkEntity>, CacheService>();
/*builder.Services.AddStackExchangeRedisCache(options => 
{
    options.Configuration = "localhost:6379";
    options.InstanceName = "local";
});*/


/*ADD REPOSITORIES*/
builder.Services.AddScoped<IRepository<DynamicLinkEntity>, DynamicLinkRepository>();

/*ADD SERVICES*/
builder.Services.AddTransient<IManagedService, RedirectHandlerService>();
builder.Services.AddScoped<ILinkReponseFactory, LinkResponseFactory>();



/*ADD CORS*/
builder.Services.AddCors(options =>
{
    options.AddPolicy("Main", builder =>
    {
        builder.AllowAnyHeader();
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
    });
});


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("Main");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run("http://10.10.0.4");

