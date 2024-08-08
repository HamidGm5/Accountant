using Accountant.Web;
using Accountant.Web.Services;
using Accountant.Web.Services.Contract;
using Blazored.Toast;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7034/") });

builder.Services.AddScoped<IIncomeServices, IncomeServices>();
builder.Services.AddScoped<IPaymentServices, PaymentServices>();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<ILoanServices, LoanServices>();
builder.Services.AddScoped<IinstallmentServices, InstallmentServices>();

builder.Services.AddBlazoredToast();


await builder.Build().RunAsync();
