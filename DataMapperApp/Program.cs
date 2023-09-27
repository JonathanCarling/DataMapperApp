using DataMapperApp.Configuration;
using DataMapperApp.DataMappers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var appSettings = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettings);

var assembly = typeof(IDataMapper).Assembly;
var type = assembly.ExportedTypes.First(x => x.Name == appSettings["DataMapperClassName"]);
builder.Services.AddScoped(typeof(IDataMapper), type);

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
