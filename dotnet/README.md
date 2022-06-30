# .NET API

> To get started, make sure you have signed up at [https://www.pecanhq.com/](https://www.pecanhq.com/), released an artifact, and created an application user with access to the *app* role.

Follow the steps below to embed the client library into your application.

## Step 1: Install the nuget package.

```shell
dotnet add package PecanHQ
```

The [PecanHQ nuget](https://www.nuget.org/packages/PecanHQ) package can be used to embed [Pecan](https://www.pecanhq.com/) into your .NET application.

## Step 2: Set up the client

```csharp
// Create the authorization service
var service = await Pecan.CreateAsync(
    "<app-key>",
    "<app-secret>",
    "<artifact>",
    <version>);
```

Create a single instance of the service on application startup. The service requires application credentials and a released schema, and will try load that schema from the [Pecan](https://www.pecanhq.com/) API.

## Step 2: Load a user profile

```csharp
ClaimsPrincipal principal;

// Look up a security principal by an identity claim
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
    // Load all claims associated with the account.
    var result = await service.LoadAsync(assertion.AccountId);

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

After authentication, the client library locates user profiles using unique asserted claims. Once located, the full profile can be be loaded using the identifier. The data in that profile can then be added to the security principal.

## Step 3: Use an authorization session

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
decimal? value = session.AsDecimal("<claim>");
```

Before each request, a new authentication session is created for the current principal. This session can be used to ensure access to a specific resource, escalate permissions, or extract data to use in attribute-based access control.

## Advanced Usage

A key design goal of [Pecan](https://www.pecanhq.com/) was to minimise communication with the API during normal operation. To achieve that, services and profiles can be locally cached, and enough data exists in each profile to make all authorization decisions locally.

### Caching the service

```csharp
// All schema details can be exported to JSON and cached.
byte[] schema = service.Dump();

// Cached state can be used to reconstruct the service locally.
var success = Pecan.TryCreate(
    schema,
    "<app-key>",
    "<app-secret>",
    "<artifact>",
    <version>,
    out service);
```

The client library loads the schema from the API, and uses it to make authorization decisions. To improve startup times, the service can be dumped to JSON and reloaded from this local copy on startup.

### Caching profiles

```csharp
// The claim profiles can be exported to JSON and cached.
byte[] utf8Json = service.AsJson(result);

// Cached claims can be used to reconstruct profiles locally
result = await service.FromJsonAsync(utf8Json);
```

You don't control third party services. If they go down, or are too slow, they can affect your SLAs. So limit your exposure to that risk, and speed up your code, by caching user profiles locally.