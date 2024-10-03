using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using pdtcc_doc_academy.Areas.Funcionario.Controllers;
using pdtcc_doc_academy.Repositories;
//using Microsoft.Extensions.Options;
//using Microsoft.Extensions.DependencyInjection;
//using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
//using Pomelo.EntityFrameworkCore.MySql;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");



// Add services to the container.
builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
        mysqlOptions =>
        {
            
        }));

// Registro do repositório no contêiner de dependências
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IFuncionariosRepository, FuncionariosRepository>();
builder.Services.AddScoped<IEscolaRepository, EscolaRepository>();
builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();
builder.Services.AddScoped<IProtocoloRepository, ProtocoloRepository>();
builder.Services.AddScoped<IAutorizacaoRepository, AutorizacaoRepository>();
builder.Services.AddScoped<IComunicadoRepository, ComunicadoRepository>();

// Configurar autenticação usando cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";  // Caminho para a página de login
        options.LogoutPath = "/Account/Logout";  // Caminho para logout
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);  // Tempo de expiração da sessão
    });


var app = builder.Build();

app.UseAuthentication();  // Adiciona o middleware de autenticação

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
    //Funcionarios


    endpoints.MapControllerRoute(
      name: "Funcionario",
      pattern: "{area:exists}/{controller=Funcionario}/{action=Index}/{id?}"
    );

    //Escolas

    endpoints.MapControllerRoute(
      name: "Escola",
      pattern: "{area:exists}/{controller=Escola}/{action=Index}/{id?}"
    );

    //Alunos


    endpoints.MapControllerRoute(
      name: "Aluno",
      pattern: "{area:exists}/{controller=Aluno}/{action=Index}/{id?}"
    );

    //default

    endpoints.MapControllerRoute(
      name: "default",
      pattern: "{controller=Home}/{action=Index}/{id?}"
    );
});
#pragma warning restore ASP0014 // Suggest using top level route registrations
app.Run();