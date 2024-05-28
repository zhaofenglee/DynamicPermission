# DynamicPermission

![img.png](/docs/images/img.png)

![img2.png](/docs/images/img2.png)

## Getting Started

### 1.Install the following NuGet packages.
* JS.Abp.DynamicPermission.Application
* JS.Abp.DynamicPermission.Application.Contracts
* JS.Abp.DynamicPermission.Domain
* JS.Abp.DynamicPermission.Domain.Shared
* JS.Abp.DynamicPermission.EntityFrameworkCore
* JS.Abp.DynamicPermission.HttpApi
* JS.Abp.DynamicPermission.HttpApi.Client
* JS.Abp.DynamicPermission.MongoDB

### 2.Add `DependsOn` attribute to configure the module
* [DependsOn(typeof(DynamicPermissionApplicationModule))]
* [DependsOn(typeof(DynamicPermissionApplicationContractsModule))]
* [DependsOn(typeof(DynamicPermissionDomainModule))]
* [DependsOn(typeof(DynamicPermissionDomainSharedModule))]
* [DependsOn(typeof(DynamicPermissionEntityFrameworkCoreModule))]
* [DependsOn(typeof(DynamicPermissionMongoDbModule))]
* [DependsOn(typeof(DynamicPermissionHttpApiModule))]
* [DependsOn(typeof(DynamicPermissionHttpApiClientModule))]

* [DependsOn(typeof(DynamicPermissionBlazorModule))]
* [DependsOn(typeof(DynamicPermissionBlazorServerModule))]
* [DependsOn(typeof(DynamicPermissionBlazorWebAssemblyModule))]
* [DependsOn(typeof(DynamicPermissionWebModule))]
### 3. Add ` builder.ConfigureDynamicPermission();` to the `OnModelCreating()` method in **YourProjectDbContext.cs**.
```csharp
protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureDynamicPermission();
    }
```

### 4. Add EF Core migrations and update your database.
Open a command-line terminal in the directory of the YourProject.EntityFrameworkCore project and type the following command:

````bash
> dotnet ef migrations add Added_DynamicPermission
````
````bash
> dotnet ef database update
````

### 5. Seed the initial data.
After updating your database, you may need to seed your database with initial data.
 
### 6. Add the following code to the `ConfigureServices` method in **YourProjectHttpApiHostModule.cs**.
```csharp
Configure<AbpPermissionOptions>(options =>
{
    options.ProviderPolicies[DynamicPermissionProvider.ProviderName] = new PermissionPolicy(DynamicPermissionProvider.ProviderName, L("DynamicPermission:Policy"), true);
});
```

## Samples

See the [sample projects](https://github.com/zhaofenglee/DynamicPermission/tree/master/host/JS.Abp.DynamicPermission.Blazor.Host)
