using PlatformService.Context;
using PlatformService.IReposatiory;
using PlatformService.Repositories;
using PlatformService.SeedingData;
using Microsoft.EntityFrameworkCore;
using PaltformService.SyncDataServices.Http;
using PaltformService.AsyncDataServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IPlatformRepository, PlatformRepository>();
builder.Services.AddHttpClient<ICommandDataClient,CommandDataClient>(); 
builder.Services.AddSingleton<IMessageBusClient, MessageBusClient>();   
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSwaggerGen();
Console.WriteLine("******************************start using Cluster Ip***************************");
if (builder.Environment.IsDevelopment())
{
    Console.WriteLine("---------------------------Development Environment--------------------------------");
    builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMemooryDb")); 

}
else
{

    Console.WriteLine("---------------------------Production Environment--------------------------------");

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("PlatformServiceConn")));

}
var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
initialization.prepareData(app,builder.Environment.IsProduction());

app.Run();
