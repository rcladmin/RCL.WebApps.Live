using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using RCL.Core.Azure.BlobStorage;
using RCL.Core.Identity.Graph;
using RCL.Core.Identity.Tools;
using RCL.WebApps.Live.DataContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddRCLIdentityGraphServices(options => builder.Configuration.Bind("AzureAd", options));
builder.Services.AddRCLIdentitySecurityGroupServices();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy =>
       policy.Requirements.Add(new GroupsCheckRequirement(new string[] { "Admin" })));
});

builder.Services.AddAzureBlobStorageServices(options =>
{
    options.ConnectionString = builder.Configuration.GetConnectionString("Storage");
});

builder.Services.AddDbContext<LiveDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));

builder.Services.AddRazorPages()
    .AddMicrosoftIdentityUI();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
