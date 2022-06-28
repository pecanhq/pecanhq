## pecan

> Making authorization easy

[Pecan](https://www.pecanhq.com/) gives you **resource-level access control** (RLAC) as a service. This repository contains the official client libraries.

[Pecan](https://www.pecanhq.com/) exists so you don't need to redo authorization for every API you build, without replicating all your data into a third-party service.

It is a simple yet powerful approach that leaves you, the developer, in control. [Pecan](https://www.pecanhq.com/) is in open beta, so [reach out](mailto://beta@pecanhq.com) with any questions or comments so we can improve our product.

### What is Resource-Level Access Control?

Resources are the parts of the architecture you create as you develop your software. They might be REST endpoints, RPC methods, or database tables. With RLAC, authorization means controlling who can access what resource, and when they do, controlling the data they can see. RLAC is a mash-up of other authorization modes, with a twist:

<dl>
  <dt>Role-based access control</dt>
  <dd>You group resources into roles, then grant a user access to the role. With RLAC, your software only knows about the resources a user can access, so you can configure roles on the fly.</dd>
  <dt>Attribute-based access control</dt>
  <dd>A user profile has extra claims, and those claims are used to filter the data available in a resource. With RLAC, special claims (like tenant or user identifiers) can be required to unlock a group of resources, and the remaining claims can be used by your code to create custom filters.</dd>
</dl>

The aim of RLAC is to simplify and future-proof your code by focusing on what it provides (resources), rather than how it is used (roles).

### Where does pecan fit in?

Implementing RLAC is a hassle that [Pecan](https://www.pecanhq.com/) takes off your hands. Firstly, it provides tools for you to design your resource schema, and update it when you release new versions. Secondly, it allows you to customize roles and set which resources they can access. Finally, it acts as a specialized data store for user, organization, and app profiles.

[Pecan](https://www.pecanhq.com/) was created to simplify a complex DotNET application, and so the first client library has been implemented in [DotNET 6.0](dotnet/README.md). More languages will be added based on feedback.

We are at the start of our incredible journey, and by the end we aim to be the defacto standard for authorization in software that needs more than *user* and *admin* accounts.

### Getting Started

First, sign up at [https://www.pecanhq.com/](https://www.pecanhq.com/), and then obtain credentials by creating a new application account. Next, open the [.NET documentation](dotnet/README.md) for detailed installation instructions.

### Support or Contact

Having trouble with Pecan? [Contact support](mailto://support@pecanhq.com/) and we’ll help you sort it out.
