using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);
var localOrigins = "_localOrigins";


// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: localOrigins,
        policy =>
        {
            policy
                .WithOrigins("http://localhost:5173")
                .WithHeaders(HeaderNames.ContentType)
                .WithMethods("GET", "POST", "PATCH", "PUT");
        });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(localOrigins);

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
