using ContabilidadTaller.Interfaces;
using ContabilidadTaller.Services;
using Microsoft.Extensions.FileProviders;

//-----------------WEB A APP -----------------
using System.Diagnostics;
//--------------------------------------------

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// MIS SERVICIOS
builder.Services.AddScoped<ContabilidadTaller.Services.ServiciosJson>();

var app = builder.Build();

//-----------------WEB A APP -----------------
app.Lifetime.ApplicationStarted.Register(() => {
    var url = "http://localhost:5000"; //
    Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
});
//--------------------------------------------

app.UseStaticFiles();
var rutaEmpresas = Path.Combine(builder.Environment.ContentRootPath, "Empresas");
if (!Directory.Exists(rutaEmpresas))
{
    Directory.CreateDirectory(rutaEmpresas);
}
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(rutaEmpresas),
    RequestPath = "/Empresas"
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
