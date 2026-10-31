using MinorProject5.Business.Services;
using MinorProject5.Data.Repositories;
using MinorProject5.Data.Database;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("FitnessDb")
    ?? throw new InvalidOperationException(
        "FitnessDb connection string is missing.");

// Database connection
builder.Services.AddSingleton(
    new DbConnectionFactory(connectionString));

// Data layer repositories
builder.Services.AddScoped<ClientRepository>();
builder.Services.AddScoped<TrainerRepository>();
builder.Services.AddScoped<ExerciseRepository>();

// Business layer services
builder.Services.AddScoped<ClientService>();
builder.Services.AddScoped<TrainerService>();
builder.Services.AddScoped<ExerciseService>();

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();