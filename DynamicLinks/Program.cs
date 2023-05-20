using DynamicLinks.Services;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var congifuration = builder.Configuration;

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

/*ADD REDIS*/
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = congifuration["Redis_server"];
    //Uncomment to add prefix to redis keys
    //options.InstanceName = "local";
});

/*ADD SERVICES*/
builder.Services.AddTransient<IRedirectService, RedirectService>();
builder.Services.AddTransient<ILinkReponseFactory, LinkResponseFactory>();

/*ADD FORWARD HEADERS OPTION*/
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});


/*ADD CORS*/
builder.Services.AddCors(options =>
{
    options.AddPolicy("Allow", builder =>
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

// Warn Headers
app.UseForwardedHeaders();

app.UseCors("Allow");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run(congifuration["Run_on"]);

