# Azure App Configuration & Feature Flags
Sample application using ASP .Net 6.0 to use Azure App Configuration and Feature Flags.

## Based on the following examples:
- [Quickstart: Create an ASP.NET Core app with Azure App Configuration](https://docs.microsoft.com/en-us/azure/azure-app-configuration/quickstart-aspnet-core-app?tabs=core5x)
- [Quickstart: Add feature flags to an ASP.NET Core app](https://docs.microsoft.com/en-us/azure/azure-app-configuration/quickstart-feature-flag-aspnet-core?tabs=core5x)
- [Tutorial: Use feature flags in an ASP.NET Core app](https://docs.microsoft.com/en-us/azure/azure-app-configuration/use-feature-flags-dotnet-core?tabs=core5x)

## Create the resources
### Requirements
- [Active Azure Subscription](https://azure.microsoft.com/en-us/free/)
- [Azure CLI](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli)
- [.Net 6.0](https://dotnet.microsoft.com/download)



### Script
```pwsh
az group create --name FeatureFlag --location eastus
az appconfig create --resource-group FeatureFlag --name UNIQUECONFIGNAME --location westus --enable-public-network
az appconfig kv set -n UNIQUECONFIGNAME --key TestApp:Settings:BackgroundColor --value '#FFF' -y
az appconfig kv set -n UNIQUECONFIGNAME --key TestApp:Settings:FontColor --value '#000' -y
az appconfig kv set -n UNIQUECONFIGNAME --key TestApp:Settings:FontSize --value '24' -y
az appconfig kv set -n UNIQUECONFIGNAME --key TestApp:Settings:Message --value 'Data from Azure AppConfiguration' -y
az appconfig feature set -n UNIQUECONFIGNAME  --feature  'Beta' -y
dotnet new mvc --no-https --output TestAppConfig
cd TestAppConfig
dotnet user-secrets init
dotnet add package Microsoft.Azure.AppConfiguration.AspNetCore
dotnet add package Microsoft.FeatureManagement.AspNetCore
$cons = az appconfig credential list -n 'UNIQUECONFIGNAME' --query '[0].connectionString'
dotnet user-secrets set ConnectionStrings:AppConfig $cons
```