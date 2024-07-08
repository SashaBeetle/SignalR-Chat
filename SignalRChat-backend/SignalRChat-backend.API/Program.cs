using SignalRChat_backend.API.Hubs;
using SignalRChat_backend.Data;
using SignalRChat_backend.Services;


var builder = WebApplication.CreateBuilder(args);

//DI Configuration
builder.Services.RegisterDataDependencies(builder.Configuration);
builder.Services.RegisterServiceDependencies(builder.Configuration);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<ChatHub>("chat");

app.Run();
