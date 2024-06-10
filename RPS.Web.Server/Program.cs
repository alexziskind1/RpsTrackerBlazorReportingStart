using Microsoft.AspNetCore.Builder;
using RPS.Data;
using RPS.Web.Server;
using RPS.Web.Server.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTelerikBlazor();



// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var tempDataContext = new PtInMemoryContext();

builder.Services.AddSingleton<IPtUserRepository, PtUserRepository>(c => new PtUserRepository(tempDataContext));
builder.Services.AddSingleton<IPtItemsRepository, PtItemsRepository>(c => new PtItemsRepository(tempDataContext));
builder.Services.AddSingleton<IPtDashboardRepository, PtDashboardRepository>(c => new PtDashboardRepository(tempDataContext));
builder.Services.AddSingleton<IPtTasksRepository, PtTasksRepository>(c => new PtTasksRepository(tempDataContext));
builder.Services.AddSingleton<IPtCommentsRepository, PtCommentsRepository>(c => new PtCommentsRepository(tempDataContext));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.UseAntiforgery();

//app.MapDefaultControllerRoute();


app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
