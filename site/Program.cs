using site;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.secret.json", optional: true);
builder.Services.AddRazorPages();
builder.Services.Configure<PolicyServiceConfig>(builder.Configuration.GetSection("TheyVoteForYou"));
builder.Services.AddScoped<IPolicyService, PolicyService>();

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
