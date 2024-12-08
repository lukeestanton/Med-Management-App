using Api.Clinic.Enterprise;
using MedManagementLibrary;
using MedManagementLibrary.DTO;
using Api.Clinic.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register Database and PhysicianEC
builder.Services.AddScoped<DatabaseHelper>();
builder.Services.AddScoped<PhysicianEC>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();
