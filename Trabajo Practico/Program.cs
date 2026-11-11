using System.Globalization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.EntityFrameworkCore;
using TrabajoPracticoDatos.Data;
using TrabajoPracticoDatos.Interfaces;
using TrabajoPracticoDatos.Repositorios;

CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(options =>
{
    options.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(campo => $"El campo {campo} tiene que ser un número.");
    options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((valor, campo) => $"El valor '{valor}' no es válido para {campo}.");
    options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(valor => "Este campo es obligatorio.");
    options.ModelBindingMessageProvider.SetValueIsInvalidAccessor(valor => $"El valor '{valor}' no es válido.");
    options.ModelBindingMessageProvider.SetUnknownValueIsInvalidAccessor(campo => $"El valor ingresado en {campo} no es válido.");
});

builder.Services.AddDbContext<TiendaContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("TiendaDb")));

builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<ICompraRepository, CompraRepository>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.AccessDeniedPath = "/Login/AccesoDenegado";
})
.AddGoogle(options =>
{
    options.ClientId = builder.Configuration["GoogleKeys:ClientId"]!;
    options.ClientSecret = builder.Configuration["GoogleKeys:ClientSecret"]!;

    options.Events.OnRedirectToAuthorizationEndpoint = context =>
    {
        context.Response.Redirect(context.RedirectUri + "&prompt=select_account");
        return Task.CompletedTask;
    };

    options.Events.OnTicketReceived = context =>
    {
        string destino = context.ReturnUri ?? "/";

        if (!destino.StartsWith("/Login/GoogleResponse"))
        {
            context.ReturnUri = "/Login/GoogleResponse?returnUrl=" + Uri.EscapeDataString(destino);
        }

        return Task.CompletedTask;
    };
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
