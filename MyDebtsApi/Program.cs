using MyDebtsApi.Data;

var builder = WebApplication.CreateBuilder(args);
builder
    .Services.
    AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });
builder.Services.AddDbContext<MyDebtsDbContext>();
var app = builder.Build();

//NÃO USA MAIS O MapGet porque preciso acessar os Controllers
//app.MapGet("/", () => "Hello World!");
app.MapControllers();

app.Run();