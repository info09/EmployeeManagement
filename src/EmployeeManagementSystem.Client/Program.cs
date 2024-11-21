using Blazored.LocalStorage;
using EmployeeManagementSystem.BaseLibrary.Entities;
using EmployeeManagementSystem.Client;
using EmployeeManagementSystem.Client.ApplicationStates;
using EmployeeManagementSystem.ClientLibrary.Helpers;
using EmployeeManagementSystem.ClientLibrary.Services.Contracts;
using EmployeeManagementSystem.ClientLibrary.Services.Implementations;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Popups;
using Syncfusion.Licensing;

SyncfusionLicenseProvider.RegisterLicense("MzU4NzQ2MUAzMjM3MmUzMDJlMzBGWVh1UkU0cjF1MjZzMmZHQnZoOVVQdlpYa09zWCtqbkpaMExuRFI3UFJFPQ==");
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddTransient<CustomHttpHandler>();
builder.Services.AddHttpClient("SystemApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:6001/");
}).AddHttpMessageHandler<CustomHttpHandler>();

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:6001/") });
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<GetHttpClient>();
builder.Services.AddScoped<LocalStorageService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<IUserAccountService, UserAccountService>();

#region GeneralDepartment / Departmet / Branch

builder.Services.AddScoped<IGenericService<GeneralDepartment>, GenericService<GeneralDepartment>>();
builder.Services.AddScoped<IGenericService<Department>, GenericService<Department>>();
builder.Services.AddScoped<IGenericService<Branch>, GenericService<Branch>>();

#endregion

#region Country / City / Town

builder.Services.AddScoped<IGenericService<Country>, GenericService<Country>>();
builder.Services.AddScoped<IGenericService<City>, GenericService<City>>();
builder.Services.AddScoped<IGenericService<Town>, GenericService<Town>>();

#endregion

#region Employee

builder.Services.AddScoped<IGenericService<Employee>, GenericService<Employee>>();

#endregion

builder.Services.AddScoped<AllState>();
builder.Services.AddSyncfusionBlazor();
builder.Services.AddScoped<SfDialogService>();

await builder.Build().RunAsync();