var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddMarten(options =>
{
    if (builder.Environment.IsDevelopment())
    {
        options.Connection(builder.Configuration.GetConnectionString("Database")!);
    }
    else
    {
        options.Connection(builder.Configuration.GetConnectionString("Database")!);
    }
    // options.AutoCreateSchemaObjects
}).UseLightweightSessions();

var app = builder.Build();

app.MapCarter();
app.MapGet("/", () => "Hello World!");
app.Run();