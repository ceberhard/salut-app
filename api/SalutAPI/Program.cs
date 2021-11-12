using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Cors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "SalutAPI", Version = "v1" });
});

builder.Services.AddCors(o => o.AddPolicy("SalutPolicy", builder => {
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseCors("SalutPolicy");
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SalutAPI v1"));
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
