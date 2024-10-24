using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using pdtcc_doc_academy.Repositories;
//using Microsoft.Extensions.Options;
//using Microsoft.Extensions.DependencyInjection;
//using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
//using Pomelo.EntityFrameworkCore.MySql;





var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


 void ConfigureServices(IServiceCollection services)
{
    services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.LoginPath = "/Account/Login"; // Caminho para a página de login
        });

    services.AddAuthorization(options =>
    {
        options.AddPolicy("AlunoPolicy", policy => policy.RequireRole("Aluno"));
        options.AddPolicy("FuncionarioPolicy", policy => policy.RequireRole("Funcionario"));
        options.AddPolicy("EscolaPolicy", policy => policy.RequireRole("Escola"));
    });

    services.AddControllersWithViews();
}



// Add services to the container.
builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
        mysqlOptions =>
        {
        }));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(Options =>
    {

    });

// Registro do repositório no contêiner de dependências
builder.Services.AddControllersWithViews();

var app = builder.Build();

//app.UseAuthentication();  // Adiciona o middleware de autenticação

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

#pragma warning disable ASP0014 // Suggest using top level route registrations
app.UseEndpoints(endpoints =>
{ 

    endpoints.MapControllerRoute(
      name: "default",
      pattern: "{controller=autenticacao}/{action=login}/{id?}" //rota default de quando executa o sistema
    );
});
#pragma warning restore ASP0014 // Suggest using top level route registrations
app.Run();