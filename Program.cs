using BlogApi.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDbContext<BlogApiDataContext>();

var app = builder.Build();

app.MapControllers();

app.Run();
