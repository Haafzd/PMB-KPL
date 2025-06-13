// Program.cs
using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PMB.Models;
using PMB.Reporting;
using PMB.Services;

var builder = WebApplication.CreateBuilder(args);

// Konfigurasi settings
builder.Services.Configure<QuotaConfig>(builder.Configuration.GetSection("QuotaConfig"));
builder.Services.Configure<ReportFormatConfig>(builder.Configuration.GetSection("ReportFormat"));

// Registrasi services
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<QuotaConfig>>().Value);
builder.Services.AddScoped<DepartmentQuotaService>();


builder.Services.AddScoped<ReportGenerator>(provider =>
    new ReportGenerator(provider.GetRequiredService<IOptions<ReportFormatConfig>>().Value));

// Registrasi PaymentProcessor dengan factory
builder.Services.AddScoped<PaymentProcessor<BankTransferPayment>>(provider =>
    new PaymentProcessor<BankTransferPayment>(new BankTransferPayment("")));

// Registrasi dependencies lainnya
builder.Services.AddScoped<DepartmentRuleLoader>();
builder.Services.AddDbContext<AppDbContext>();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();