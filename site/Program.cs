using site;
using site.TheyVoteForYou;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.secret.json", optional: true);
builder.Services.AddRazorPages();
builder.Services.Configure<TheyVoteForYouApiClientConfig>(builder.Configuration.GetSection("TheyVoteForYou"));
builder.Services.AddScoped<ITheyVoteForYouApiClient, TheyVoteForYouApiClient>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllers();

app.Run();
