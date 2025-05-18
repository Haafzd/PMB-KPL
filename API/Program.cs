// PMB-API/Program.cs
using API.Data;
using PMB.Reporting;
using PMB.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<DepartmentQuotaService>();
builder.Services.AddScoped<DepartmentRuleLoader>();
builder.Services.AddScoped<ReportGenerator>();
builder.Services.AddScoped<ReportTemplateLoader>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();