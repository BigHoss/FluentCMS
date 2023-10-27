using FluentCMS.Web.UI;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddConfig(builder.Environment);

var services = builder.Services;

// Add services to the container.
services.AddRazorComponents()
    .AddInteractiveServerComponents();

services.AddControllers();

services.AddApiDocumentation();

services.AddHttpContextAccessor();

services.AddScoped(sp =>
{
    var httpContextAccessor = sp.GetRequiredService<IHttpContextAccessor>();
    var request = httpContextAccessor?.HttpContext?.Request;
    var domain = string.Format("{0}://{1}/api/", request?.Scheme, request?.Host.Value);
    var baseAddress = new Uri(domain);

    return new HttpClient(new HttpClientHandler { AllowAutoRedirect = false })
    {
        BaseAddress = new Uri(domain),

    };
});


var app = builder.Build();

app.UseExceptionHandler("/Error", createScopeForErrors: true);

//app.UseHsts();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAntiforgery();

app.UseApiDocumentation();

app.UseAuthorization();

app.MapControllers();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
