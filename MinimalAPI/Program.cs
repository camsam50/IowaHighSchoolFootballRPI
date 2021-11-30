
using Azure.Identity;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;

var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();





//var uri = "https://iahsfbrpivault.vault.azure.net/";
//builder.Configuration.AddAzureKeyVault(
//        new Uri(uri),
//        new DefaultAzureCredential()
//    );



var keyVaultEndpoint = "https://iahsfbrpivault.vault.azure.net";
if (!string.IsNullOrEmpty(keyVaultEndpoint))
{
    var azureServiceTokenProvider = new AzureServiceTokenProvider();
    var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
    builder.Configuration.AddAzureKeyVault(keyVaultEndpoint, keyVaultClient, new DefaultKeyVaultSecretManager());
}





var variable = builder.Configuration.GetValue<string>("Some_App_Value");
var localvar = builder.Configuration.GetValue<string>("secret10");
var secretvar = builder.Configuration.GetValue<string>("secret1");

var vaultSecret = builder.Configuration["secret1"];


var message = variable + " " + localvar + " " + secretvar + " " + vaultSecret;

var app = builder.Build();


if(variable != null)
{
    app.MapGet("/", () => message);
}
else
{
    app.MapGet("/", () => "HELLO WORLD (version 2)!");
}




//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//var summaries = new[]
//{
//    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//};

//app.MapGet("/weatherforecast", () =>
//{
//    var forecast = Enumerable.Range(1, 5).Select(index =>
//       new WeatherForecast
//       (
//           DateTime.Now.AddDays(index),
//           Random.Shared.Next(-20, 55),
//           summaries[Random.Shared.Next(summaries.Length)]
//       ))
//        .ToArray();
//    return forecast;
//})
//.WithName("GetWeatherForecast");

app.Run();

//internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
//{
//    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
//}