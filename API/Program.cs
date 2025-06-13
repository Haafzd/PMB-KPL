// Program.cs
using API.Models;
using API.Models.Reporting;
using API.Services;
using Microsoft.Extensions.Options;
using System.Net;


var builder = WebApplication.CreateBuilder(args);

// Konfigurasi settings
builder.Services.Configure<QuotaConfig>(builder.Configuration.GetSection("QuotaConfig"));
builder.Services.Configure<ReportFormatConfig>(builder.Configuration.GetSection("ReportFormat"));
builder.Services.AddSingleton<UserService>();


builder.Services.AddScoped<ReportGenerator>();

// Registrasi PaymentProcessor dengan factory
builder.Services.AddScoped<IPaymentMethod>(provider =>
    new BankTransferPayment("default config or get from Configuration"));


// Registrasi dependencies lainnya
builder.Services.AddHttpClient<PMBClient>();
builder.Services.AddScoped<IApplicantService, ApplicantService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();



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