using AutoMapper;
using TransfloDriver.BLL.Profiles;
using TransfloDriver.BLL.Services.Drivers;
using TransfloDriver.DAL.Repositories.Entities;
using TransfloDriver.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


string Cors = "CorsPolicyAll";
builder.Services.AddCors(options =>
{
    options.AddPolicy(Cors,
                      policy =>
                      {
                          policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                      });
});


var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new DriverProfile());
});
IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddScoped<IDriverService, DriverService>();
builder.Services.AddScoped<IDriverRepository, DriverRepository>();
builder.Configuration.GetSection("AppSettings").Get<AppSettings>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(Cors);
app.UseAuthorization();

app.MapControllers();

app.Run();
