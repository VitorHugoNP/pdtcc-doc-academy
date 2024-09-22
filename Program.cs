using Microsoft.EntityFrameworkCore;
using pdtcc_doc_academy.Areas.Funcionario.Controllers;
using pdtcc_doc_academy.Repositories;
//using Microsoft.Extensions.Options;
//using Microsoft.Extensions.DependencyInjection;
//using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
//using Pomelo.EntityFrameworkCore.MySql;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddScoped<IComunicadosRepository, ComunicadosRepository>();

// Add services to the container.
builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
        mysqlOptions =>
        {
            
        }));

// Registro do repositório no contêiner de dependências
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IFuncionariosRepository, FuncionariosRepository>();


var app = builder.Build();

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
    //Funcionarios Comunicados


    endpoints.MapControllerRoute(
      name: "Funcionario",
      pattern: "{area:exists}/{controller=Funcionario}/{action=Index}/{id?}"
    );

    endpoints.MapControllerRoute(
      name: "default",
      pattern: "{controller=Home}/{action=Index}/{id?}"
    );
});
#pragma warning restore ASP0014 // Suggest using top level route registrations
app.Run();