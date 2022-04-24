using ISSLocation.Middlewares;
using ISSLocation.Services;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.

builder.Services.AddHttpClient<IssLocationService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IConfiguration>(config);
builder.Services.AddSingleton<IIssLocationService, IssLocationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseMiddleware<ExceptionMiddleware>();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(build => build.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader());

app.Run();