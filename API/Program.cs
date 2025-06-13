using API.Models;
using API.Models.Reporting;
using API.Services;

var builder = WebApplication.CreateBuilder(args);

// Konfigurasi settings
builder.Services.Configure<QuotaConfig>(builder.Configuration.GetSection("QuotaConfig"));
builder.Services.Configure<ReportFormatConfig>(builder.Configuration.GetSection("ReportFormat"));

// Registrasi dependencies lainnya
builder.Services.AddHttpClient<PMBClient>();
builder.Services.AddScoped<IApplicantService, ApplicantService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<ReportGenerator>();
builder.Services.AddSingleton<UserService>();

// Registrasi PaymentProcessor dengan factory
builder.Services.AddScoped<IPaymentMethod>(provider =>
    new BankTransferPayment("default config or get from Configuration"));

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();