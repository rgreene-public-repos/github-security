
using System;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

string keyVaultName = "azkv-demo";

var kvUri = "https://" + keyVaultName + ".vault.azure.net";
var credential = new ClientSecretCredential(
    "6c637512-c417-4e78-9d62-b61258e4b619",  // use value from "tenant"
    "5a066172-afa7-4e50-9aac-888b2611cac7", //  use value from "appId"
    "- it gone -" // use value from "password"  
);

var client = new SecretClient(new Uri(kvUri), credential);
var secret = client.GetSecret("demo-secret");
Console.WriteLine($"Result: {secret.Value.Name} is {secret.Value.Value}");
