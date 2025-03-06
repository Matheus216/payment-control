var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureInitializer(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.InitializeMigration();

app.Run();

