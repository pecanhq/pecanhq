## DotNET API

### Install the nuget package.

```shell
dotnet add package PecanHQ
```

### Set up the client

```csharp
// Create the authorization service
var service = await Pecan.CreateAsync(
    "<app-key>",
    "<app-secret>",
    "<artifact>",
    <version>);

// All schema details can be exported to JSON and cached.
var schema = service.AsJson();

// Cached state can be used to reconstruct the service locally.
var success = Pecan.TryCreate(
    schema,
    "<app-key>",
    "<app-secret>",
    "<artifact>",
    <version>,
    out service);
```

### Load a user profile

```csharp
// Look up a security principal by an identity claim
ClaimsPrincipal principal;
var assertion = await service.FindAsync("<claim>", "<value>", tenant: null);
if (assertion == null)
{
    // In this case, user profiles are being lazily created
    var profile = await service.Resource.AsAssignIdentityUri().PostAsync(
        "<claim>",
        "<value>",
        new() { "<scope>" });

    // Construct a claims principal from the newly-created profile
    var claims = profile.Assertions.Select(x => new Claim(
        $"{x.Issuer}{x.Key}",
        x.Value,
        null,
        x.Issuer));
    var identity = new ClaimsIdentity(claims);
    principal = new ClaimsPrincipal(identity);
}
else
{
    // All claims associated with the account.
    var result = await service.LoadAsync(assertion.AccountId);

    // The claim profiles can be exported to JSON and cached.
    var utf8Json = service.AsJson(result);

    // Cached claims can be used to reconstruct profiles locally
    result = await service.FromJsonAsync(utf8Json);

    if (result.Success)
    {
        // Construct a claims principal from the result
        var claims = result.Claims.Select(x => new Claim(
            x.Key,
            x.Value,
            null,
            result.Issuer));
        var identity = new ClaimsIdentity(claims);
        principal = new ClaimsPrincipal(identity);
    }
    else principal = new ClaimsPrincipal();
}
```

### Use an authorization session

```csharp
// Create an authorization session
var session = new Session(service, principal);

// Assert access to a specific resource
if (session.HasPermissions("<service>", "<resource>", "<permissions>"))
{
    // Do guarded action
}

// Assume system-level access for the user
session.EscalatePrivileges();

// Fetch a typed assertion for attribute-based access control
decimal? value = session.GetDecimal("<claim>");
```