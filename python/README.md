# PecanHQ

> To get started, make sure you have signed up at [https://www.pecanhq.com/](https://www.pecanhq.com/), released an artifact, and created an application user with access to the *app* role.

Follow the steps below to embed the client library into your application.

## Step 1: Install the pip package.

```shell
python3 -m pip install pecanhq
```

The [PecanHQ pip](https://pypi.org/project/pecanhq/) package can be used to embed [Pecan](https://www.pecanhq.com/) into your python application.

## Step 2: Set up the client

```python
# Create the authorization service
import pecanhq, requests
pecan = pecanhq.create(
    requests.session(),
    "<app-key>",
    "<app-secret>",
    "<artifact>",
    <version>)
```

Create a single instance of the service on application startup. The service requires application credentials and a released schema, and will try load that schema from the [Pecan](https://www.pecanhq.com/) API.

## Step 2: Load a user profile

```python
# Look up a security principal by an identity claim
assertion = pecan.find("<claim>", "<value>", tenant=None)
if assertion is None:
    # In this case, user profiles are being lazily created
    profile = pecan.resource.as_assign_identity_uri().post(
        "<claim>",
        "<value>",
        [ "<scope>" ])

    # Construct a claims principal from the newly-created profile
    principal = {
        f"{x['issuer']}{x.['key']}": x['value'] for x in profile['assertions']
    }
else:
    # Load all claims associated with the account.
    result = pecan.load(assertion['account_id'])

    if result:
        # Construct a claims principal from the result
        principal = result['claims'];
    else:
        principal = {}
```

After authentication, the client library locates user profiles using unique asserted claims. Once located, the full profile can be be loaded using the identifier. The data in that profile can then be added to the security principal.

## Step 3: Use an authorization session

```python
# Create an authorization session
session = pecanhq.Session(pecan, principal)

# Assert access to a specific resource
if session.has_permissions("<service>", "<resource>", "<permissions>"):
    # Do guarded action
    pass

# Assume system-level access for the user
session = session.escalate_privileges();

# Fetch a typed assertion for attribute-based access control
value = session.as_decimal("<claim>")
```

Before each request, a new authentication session is created for the current principal. This session can be used to ensure access to a specific resource, escalate permissions, or extract data to use in attribute-based access control.

## Advanced Usage

A key design goal of [Pecan](https://www.pecanhq.com/) was to minimise communication with the API during normal operation. To achieve that, services and profiles can be locally cached, and enough data exists in each profile to make all authorization decisions locally.

### Caching the service

```python
# All schema details can be exported to JSON and cached.
cached = pecan.dump()

# Cached state can be used to reconstruct the service locally.
pecan = pecanhq.create(
    schema,
    "<app-key>",
    "<app-secret>",
    "<artifact>",
    <version>,
    cached=cached)
```

The client library loads the schema from the API, and uses it to make authorization decisions. To improve startup times, the service can be dumped to JSON and reloaded from this local copy on startup.

### Caching profiles

```python
# The claim profiles can be exported to JSON and cached.
data = pecan.as_json(result)

# Cached claims can be used to reconstruct profiles locally
result = pecan.from_json(data)
```

You don't control third party services. If they go down, or are too slow, they can affect your SLAs. So limit your exposure to that risk, and speed up your code, by caching user profiles locally.