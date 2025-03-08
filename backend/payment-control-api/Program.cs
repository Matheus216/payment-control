var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureInitializer(builder.Configuration);

var app = builder.Build();

app.UseCors("AllowAll");

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.MapControllers();
app.InitializeMigration();


app.Run();

