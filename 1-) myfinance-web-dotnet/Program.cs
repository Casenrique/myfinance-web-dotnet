using myfinance_web_dotnet_infra;
using myfinance_web_dotnet_service;
using myfinance_web_dotnet_service.Interfaces;
using Microsoft.Extensions.Configuration; // Certifique-se de incluir isso
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços ao contêiner.
builder.Services.AddControllersWithViews();

// Configura a conexão do DbContext
builder.Services.AddDbContext<MyFinanceDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));

// Registra os serviços
builder.Services.AddScoped<IPlanoContaService, PlanoContaService>();
builder.Services.AddScoped<ITransacaoService, TransacaoService>();

var app = builder.Build();

// Configura o pipeline de requisições HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // O valor padrão do HSTS é 30 dias. Você pode querer alterar isso para cenários de produção.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
