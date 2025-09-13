using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RepoPattrenWithUnitOfWork.Core;
using RepoPattrenWithUnitOfWork.Core.CQRS.Handllers.Author;
using RepoPattrenWithUnitOfWork.Core.CQRS.Querys;
using RepoPattrenWithUnitOfWork.Core.Dto;
using RepoPattrenWithUnitOfWork.Core.Interface.Service;
using RepoPattrenWithUnitOfWork.Core.Service.ExternalServices;
using RepoPattrenWithUnitOfWork.EF;
using RepoPattrenWithUnitOfWork.EF.Triggers;
using System.Reflection;
using AutoMapper;


var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<NhtsaApiSettings>(
    builder.Configuration.GetSection("ApiSettings:NhtsaApi"));
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "My API",
        Version = "v1"
    });
});
builder.Services.AddDbContext<ApplicationDbContext>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
    o.UseTriggers(triggersOptions => triggersOptions.AddTrigger<SoftDeleteTrigger>());
}
);


//o.UseTriggers(triggersOptions => triggersOptions.AddTrigger<SoftDeleteTrigger>());

//builder.Services.AddTransient(typeof(IBaseRepository<>),typeof(BaseRepository<>));//register to baserepo inside api

builder.Services.Configure<NhtsaApiSettings>(
    builder.Configuration.GetSection("ApiSettings:NhtsaApi"));

builder.Services.AddMediatR(Assembly.GetAssembly(typeof(GetAllMakesQuery)));

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
//builder.Services.AddTransient<Profile, MappingProfile >();

builder.Services.AddAutoMapper(cfg => { }, typeof(MappingProfile).Assembly);

builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddHttpClient<INhtsaApiClient, NhtsaApiClient>((sp, client) =>
{
    var opts = sp.GetRequiredService<
        Microsoft.Extensions.Options.IOptions<NhtsaApiSettings>>().Value;

    client.BaseAddress = new Uri(opts.BaseUrl.TrimEnd('/') + "/");
    client.Timeout = TimeSpan.FromSeconds(30);
    client.DefaultRequestHeaders.Add("User-Agent", "VehicleApp/1.0");
});
builder.Services.AddScoped<VehicleMakeQueryProcessor>();
builder.Services.AddScoped<VehicleModelQueryProcessor>();

//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
////builder.Services.AddMediatR(Assembly.GetAssembly(typeof(GetByIdClientQuery)));
builder.Services.AddCors(options =>


{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
    var app = builder.Build();
// Apply pending migrations automatically
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}
app.UseCors();



app.UseHttpsRedirection();

//app.UseClientization();

app.MapControllers();

app.Run("https://localhost:7227");
//app.Run("http://192.168.1.223:47170");
//app.Run("http://192.168.1.151:5000");