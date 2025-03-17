using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;
using NASPLOID.Models;

namespace NASPLOID;

public class DatabaseContext : ModelContext
{
    private static IDataProtector Protector;
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var serviceProvider = new ServiceCollection()
                .AddDataProtection()
                .SetApplicationName(Environment.GetEnvironmentVariable("MBAppName"))
                .Services
                .BuildServiceProvider();
            
            var protectedProvider = serviceProvider.GetService<IDataProtectionProvider>();
            Protector = protectedProvider.CreateProtector(Environment.GetEnvironmentVariable("MBAppName"));
        }
        //string connString = GetConnectionStringFromKeyVault();
        string connString = GetConnectionStringFromEnvironment();
        try
        {
            optionsBuilder.UseMySql(connString, ServerVersion.AutoDetect(connString));
        }
        catch (MySqlException e)
        {
            throw new Exception("Could not connect to mysql database.\n" + e.Message, e);
        }
    }
    
    private static string GetConnectionStringFromKeyVault()
    {
        string keyVaultUrl = Environment.GetEnvironmentVariable("AzureKeyVaultUrl");
        var client = new SecretClient(new Uri(keyVaultUrl), new DefaultAzureCredential());
        KeyVaultSecret secret = client.GetSecret("MoneyBearConn");
        return secret.Value;
    }

    private static string GetConnectionStringFromEnvironment()
    {
        string protectedString = Environment.GetEnvironmentVariable("MBConnString");
        return Protector.Unprotect(protectedString);
    }
}