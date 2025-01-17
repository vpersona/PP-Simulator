using Microsoft.Extensions.DependencyInjection;
using Simulator.Maps;
using Simulator;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


builder.Services.AddRazorPages();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddSingleton(provider =>
{
    SmallSquareMap map = new(8, 6);
    List<IMappable> mappables = new()
    {
        new Orc("Gorbag"),
        new Elf("Elandor"),
        new Animals { Description = "Eagles", Size = 3, IsFlying = true },
        new Animals { Description = "Bunnies", Size = 3 },
        new Animals { Description = "Ostriches", Size = 3, IsBird = true }
    };
    List<Point> points = new()
    {
        new(2, 2),
        new(3, 1),
        new(2, 0),
        new(3, 1),
        new(0, 0)
    };
    string moves = "dlrludlurduluru";
    Simulation simulation = new(map, mappables, points, moves);
    return simulation;
});

var app = builder.Build();
app.UseSession();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection(); 
app.UseStaticFiles(); 
app.UseAuthorization();


app.UseSession();

app.UseRouting();
app.MapRazorPages();


app.MapGet("/", async context =>
{
    context.Response.Redirect("/Simulation");
});

app.Run();
