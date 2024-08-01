using Microsoft.EntityFrameworkCore;
using susalem.vue.Data;
using susalem.vue.Services;

var builder = WebApplication.CreateBuilder(args);

//øÁ”Ú
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowCrossOrigin",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    options.JsonSerializerOptions.WriteIndented = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<UserService>();
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowCrossOrigin");//øÁ”Ú

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();