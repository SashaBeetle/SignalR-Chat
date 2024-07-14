# SignalR-Chat
### Project for `'ShuttleX Inc.'` 
## Stack
* [.NET](https://dotnet.microsoft.com/) - free, open-source, cross-platform framework for building modern apps and powerful cloud services.
* [Postgres SQL](https://www.postgresql.org/) - is a powerful, open source object-relational database system with over 35 years of active development that has earned it a strong reputation for reliability, feature robustness, and performance.
* [Entity Framework](https://learn.microsoft.com/uk-ua/ef/) - object-relational mapping (ORM) framework for .NET developers that enables them to work with databases using .NET objects, simplifying the process of data access and manipulation.
* [AutoMapper](https://automapper.org/) - is a simple little library built to solve a deceptively complex problem - getting rid of code that mapped one object to another.
* [SignalR](https://dotnet.microsoft.com/en-us/apps/aspnet/signalr) - is open-source on GitHub, just like the rest of .NET. In addition to the source code, the protocol specification for communication between hubs and clients is open too.
* [NUnit](https://nunit.org/) - is an open-source unit testing framework for the .NET and Mono.
* [NuGet packages](https://learn.microsoft.com/uk-ua/nuget/) - type of software package used in the Microsoft .NET ecosystem, containing compiled code and other resources, and are used by developers to easily add functionality to their projects and share code between teams.

## How to run Backend
Open your system terminal and run commands:
```sh
git clone https://github.com/SashaBeetle/SignalR-Chat.git
```
Add your database connection string to files:
In `...\SignalR-Chat-backend\SignalR-Chat.Data\DIConfiguration.cs` method `GetConnectionString()`.Method should look like that:
```sh
private static void RegisterDatabaseDependencies(this IServiceCollection services, IConfigurationRoot configuration)
{
    services.AddDbContext<SignalRChatDbContext>(options =>
        options.UseNpgsql(configuration.GetConnectionString("SignalRChatDatabase")));
}
```
Add `User Secrets` and instead of `ConnectionString` add your database connection string. 
```sh
  "ConnectionStrings": {
    "SignalRChatDatabase": "ConnectionString"
  }
```
It is necessary to make migration and update database.
In `...\SignalR-Chat-backend\SignalR-Chat-backend.Data` open the PMC(Package Manager Console) and then push the command:
```sh
Update-Database
```
If you want to use this project with some client sides you need to add the CORS for project.
In `To Do\todo-backend\todo-backend.WEB\program.cs` in `builder.Services.AddCors` add CORS `policy.WithOrigins("http://localhost:0000", "http://google.com")` CORS should look like this:
```sh
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "CorsPolicy",
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:7013")
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                      });
});
```
