# Introduction
This library allows to query master data from various RDF data stores of the federal administration.

# Usage
## Configuration
Register the available master data services in your DI container as follows.

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMasterDataService(
    new SwissFChMasterDataSettings
	{
		
	});
```

The ```SwissFChMasterDataSettings``` allow you to configure the following options.

- ```RequestTimeoutMs```: Http request timeout in milliseconds
- ```ProxyAddress```: specify an http proxy (e.g. http://your-example-proxy.com:8080)
- ```UserName```: user name in case you are querying a protected RDF store
- ```Password```: password in case you are querying a protected RDF store

## Querying Data
Inject the required master data service in your class and use the offered methods to query data.

```csharp
var countries = await countryService.GetCountries(stoppingToken);
```

The library offers the following services that are ready to use:
- ```IAgentService```: Can query agencies and people that are possible actors in the processes of the federal assembly. You have to provide URIs that will be resolved by the service.
- ```ICantonService```: Returns a list of all the cantons of Switzerland.
- ```IDepartmentService```: Returns a list of all the departments of the federal administration.
- ```ICommitteeService```: Returns a list of all committees of the federal administration.
- ```ICountryService```: Returns a list of all the countries of the world.
- ```IOfficeService```: Returns a list of all the offices of the federal administration.
- ```ISessionService```: Returns a list of all the sessions (with begin and end dates) of the federal assembly.

# Contribution
See: https://github.com/swiss/fch-sparql/blob/main/CONTRIBUTING.md

# Security
See: https://github.com/swiss/fch-sparql/blob/main/SECURITY.md

# Development Workflow

To publish a new version of the NuGet package, proceed as follows.

* apply and push your changes
* define and describe the new version in ```CHANGELOG.md```
* push the corresponding label with ```git tag vx.x.x``` and ```git push origin vx.x.x```
* go to GitHub -> Actions -> 'Build and Publish to NuGet.org' and trigger a run while specifying the correct GIT label
