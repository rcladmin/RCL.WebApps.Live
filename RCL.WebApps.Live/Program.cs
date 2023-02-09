using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using RCL.Core.Azure.BlobStorage;
using RCL.Core.Identity.Graph;
using RCL.Core.Identity.Tools;
using RCL.WebApps.Live.DataContext;
using RCL.WebApps.Live.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddRCLIdentityGraphServices(options => builder.Configuration.Bind("AzureAd", options));
builder.Services.AddRCLIdentitySecurityGroupServices();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy =>
       policy.Requirements.Add(new GroupsCheckRequirement(new string[] { "Admin" })));
});

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddAzureBlobStorageServices(options =>
{
    options.ConnectionString = builder.Configuration.GetConnectionString("Storage");
});

builder.Services.AddDbContext<LiveDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));

builder.Services.AddPaymentRequestServices(options => builder.Configuration.Bind("PaymentRequest", options));

builder.Services.AddRazorPages()
    .AddMicrosoftIdentityUI();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();
