using SignalRChat_backend.API.Hubs;
using SignalRChat_backend.API.Mapping;
using SignalRChat_backend.Data;
using SignalRChat_backend.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "CorsPolicy",
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:7013")
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials();
                      }
    );
});

//DI Configuration
builder.Services.RegisterDataDependencies(builder.Configuration);
builder.Services.RegisterServiceDependencies(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseCors("CorsPolicy");

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<ChatHub>("/chat");
});

app.Run();

public partial class Program { }