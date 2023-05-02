// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
#nullable enable
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System;
using meta = PecanHQ;


namespace PecanHQ.Grant.Resources
{

    /// <summary>
    /// Utility methods to make resource navigation seamless.
    /// </summary>
    /// <remarks>
    /// All services in this portal will implement this interface.
    /// </remarks>
    public interface IResource : INavigationState
    {

        /// <summary>
        /// Open-ended navigation within the service group.
        /// </summary>
        /// <remarks>
        /// See the source URI and the object itself for documentation on
        /// where each entity can link.
        /// </remarks>
        GrantResource From(INavigationAware? src);

        /// <summary>
        /// A utility method for downloading attachments.
        /// </summary>
        Task<meta::IAttachment> GetAttachmentAsync(Uri uri, CancellationToken token = default);

        /// <summary>
        /// A utility method for downloading attachments.
        /// </summary>
        /// <remarks>
        /// A utility method for string URIs.
        /// </remarks>
        Task<meta::IAttachment> GetAttachmentAsync(string uri, CancellationToken token = default);

        /// <summary>
        /// A utility method for downloading resources directly.
        /// </summary>
        Task<T?> GetFromJsonAsync<T>(Uri uri, CancellationToken token = default) where T : class;

        /// <summary>
        /// A utility method for downloading resources directly.
        /// </summary>
        /// <remarks>
        /// A utility method for string URIs.
        /// </remarks>
        Task<T?> GetFromJsonAsync<T>(string uri, CancellationToken token = default) where T : class;

        /// <summary>
        /// A utility method for scrolling through a resource collection.
        /// </summary>
        Task<meta::IResultSet<T>> ScrollFromJsonAsync<T>(
            Uri uri,
            CancellationToken token = default,
            bool lazy = default);

        /// <summary>
        /// A utility method for scrolling through a resource collection.
        /// </summary>
        /// <remarks>
        /// A utility method for string URIs.
        /// </remarks>
        Task<meta::IResultSet<T>> ScrollFromJsonAsync<T>(
            string uri,
            CancellationToken token = default,
            bool lazy = default);

    }


    /// <summary>
    /// A local view of all resources that share a common root.
    /// </summary>
    public interface IGrantResource : ILinkable<ServiceEntrypoint>, IResource
    {

        /// <summary>
        /// A flag indicating the accounts resource is available.
        /// </summary>
        bool HasAccountsUri { get; }

        /// <summary>
        /// Start building an API request from the accounts resource url.
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item>
        /// <term>Navigations</term>
        /// <description>The accounts resource has 1 navigation</description>
        /// <list type="bullet">
        /// <item>From <c>/rows</c> to the account context</item>
        /// </list>
        /// </item>
        /// </list>
        /// </remarks>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.AccountsUri AsAccountsUri(CancellationToken token = default);

        /// <summary>
        /// A flag indicating the affected resource is available.
        /// </summary>
        bool HasAffectedUri { get; }

        /// <summary>
        /// Start building an API request from the affected resource url.
        /// </summary>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.AffectedUri AsAffectedUri(CancellationToken token = default);

        /// <summary>
        /// A flag indicating the artifacts resource is available.
        /// </summary>
        bool HasArtifactsUri { get; }

        /// <summary>
        /// Start building an API request from the artifacts resource url.
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item>
        /// <term>Navigations</term>
        /// <description>The artifacts resource has 1 navigation</description>
        /// <list type="bullet">
        /// <item>From <c>/rows</c> to the artifact context</item>
        /// </list>
        /// </item>
        /// </list>
        /// </remarks>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.ArtifactsUri AsArtifactsUri(CancellationToken token = default);

        /// <summary>
        /// A flag indicating the audit resource is available.
        /// </summary>
        bool HasAuditUri { get; }

        /// <summary>
        /// Start building an API request from the audit resource url.
        /// </summary>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.AuditUri AsAuditUri(CancellationToken token = default);

        /// <summary>
        /// A flag indicating the available policies resource is available.
        /// </summary>
        bool HasAvailablePoliciesUri { get; }

        /// <summary>
        /// Start building an API request from the available policies resource url.
        /// </summary>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.AvailablePoliciesUri AsAvailablePoliciesUri(CancellationToken token = default);

        /// <summary>
        /// A flag indicating the claims resource is available.
        /// </summary>
        bool HasClaimsUri { get; }

        /// <summary>
        /// Start building an API request from the claims resource url.
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item>
        /// <term>Navigations</term>
        /// <description>The claims resource has 2 navigations</description>
        /// <list type="bullet">
        /// <item>From <c>/rows</c> to the claim context</item>
        /// <item>From <c>/rows</c> to the update claim action</item>
        /// </list>
        /// </item>
        /// </list>
        /// </remarks>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.ClaimsUri AsClaimsUri(CancellationToken token = default);

        /// <summary>
        /// A flag indicating the evictions resource is available.
        /// </summary>
        bool HasEvictionsUri { get; }

        /// <summary>
        /// Start building an API request from the evictions resource url.
        /// </summary>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.EvictionsUri AsEvictionsUri(CancellationToken token = default);

        /// <summary>
        /// A flag indicating the lookup account resource is available.
        /// </summary>
        bool HasLookupAccountUri { get; }

        /// <summary>
        /// Start building an API request from the lookup account resource url.
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item>
        /// <term>Navigations</term>
        /// <description>The lookup account resource has 2 navigations</description>
        /// <list type="bullet">
        /// <item>From <c>/</c> to the account context</item>
        /// <item>From <c>/</c> to the update identity action</item>
        /// </list>
        /// </item>
        /// </list>
        /// </remarks>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.LookupAccountUri AsLookupAccountUri(CancellationToken token = default);

        /// <summary>
        /// A flag indicating the manifest resource is available.
        /// </summary>
        bool HasManifestUri { get; }

        /// <summary>
        /// Start building an API request from the manifest resource url.
        /// </summary>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.ManifestUri AsManifestUri(CancellationToken token = default);

        /// <summary>
        /// A flag indicating the organizations resource is available.
        /// </summary>
        bool HasOrganizationsUri { get; }

        /// <summary>
        /// Start building an API request from the organizations resource url.
        /// </summary>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.OrganizationsUri AsOrganizationsUri(CancellationToken token = default);

        /// <summary>
        /// A flag indicating the parse resource is available.
        /// </summary>
        bool HasParseUri { get; }

        /// <summary>
        /// Start building an API request from the parse resource url.
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item>
        /// <term>Navigations</term>
        /// <description>The parse resource has 1 navigation</description>
        /// <list type="bullet">
        /// <item>From <c>/</c> to the set template action</item>
        /// </list>
        /// </item>
        /// </list>
        /// </remarks>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.ParseUri AsParseUri(CancellationToken token = default);

        /// <summary>
        /// A flag indicating the permissions resource is available.
        /// </summary>
        bool HasPermissionsUri { get; }

        /// <summary>
        /// Start building an API request from the permissions resource url.
        /// </summary>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.PermissionsUri AsPermissionsUri(CancellationToken token = default);

        /// <summary>
        /// A flag indicating the providers resource is available.
        /// </summary>
        bool HasProvidersUri { get; }

        /// <summary>
        /// Start building an API request from the providers resource url.
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item>
        /// <term>Navigations</term>
        /// <description>The providers resource has 1 navigation</description>
        /// <list type="bullet">
        /// <item>From <c>/rows</c> to the provider context</item>
        /// </list>
        /// </item>
        /// </list>
        /// </remarks>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.ProvidersUri AsProvidersUri(CancellationToken token = default);

        /// <summary>
        /// A flag indicating the releases resource is available.
        /// </summary>
        bool HasReleasesUri { get; }

        /// <summary>
        /// Start building an API request from the releases resource url.
        /// </summary>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.ReleasesUri AsReleasesUri(CancellationToken token = default);

        /// <summary>
        /// A flag indicating the status resource is available.
        /// </summary>
        bool HasStatusUri { get; }

        /// <summary>
        /// Start building an API request from the status resource url.
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item>
        /// <term>Navigations</term>
        /// <description>The status resource has 1 navigation</description>
        /// <list type="bullet">
        /// <item>From <c>/artifact</c> to the artifact context</item>
        /// </list>
        /// </item>
        /// </list>
        /// </remarks>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.StatusUri AsStatusUri(CancellationToken token = default);

        /// <summary>
        /// A flag indicating the user assertion resource is available.
        /// </summary>
        bool HasUserAssertionUri { get; }

        /// <summary>
        /// Start building an API request from the user assertion resource url.
        /// </summary>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.UserAssertionUri AsUserAssertionUri(CancellationToken token = default);

        /// <summary>
        /// A flag indicating the assign identity resource is available.
        /// </summary>
        bool HasAssignIdentityUri { get; }

        /// <summary>
        /// Start building an API request from the assign identity resource url.
        /// </summary>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.AssignIdentityUri AsAssignIdentityUri(CancellationToken token = default);

        /// <summary>
        /// A flag indicating the configure resource is available.
        /// </summary>
        bool HasConfigureUri { get; }

        /// <summary>
        /// Start building an API request from the configure resource url.
        /// </summary>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.ConfigureUri AsConfigureUri(CancellationToken token = default);

        /// <summary>
        /// A flag indicating the create artifact resource is available.
        /// </summary>
        bool HasCreateArtifactUri { get; }

        /// <summary>
        /// Start building an API request from the create artifact resource url.
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item>
        /// <term>Navigations</term>
        /// <description>The create artifact resource has 6 navigations</description>
        /// <list type="bullet">
        /// <item>From <c>/</c> to the artifact context</item>
        /// <item>From <c>/</c> to the create service action</item>
        /// <item>From <c>/</c> to the release action</item>
        /// <item>From <c>/</c> to the set artifact status action</item>
        /// <item>From <c>/</c> to the update artifact action</item>
        /// <item>From <c>/</c> to the services view</item>
        /// </list>
        /// </item>
        /// </list>
        /// </remarks>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.CreateArtifactUri AsCreateArtifactUri(CancellationToken token = default);

        /// <summary>
        /// A flag indicating the create provider resource is available.
        /// </summary>
        bool HasCreateProviderUri { get; }

        /// <summary>
        /// Start building an API request from the create provider resource url.
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item>
        /// <term>Navigations</term>
        /// <description>The create provider resource has 3 navigations</description>
        /// <list type="bullet">
        /// <item>From <c>/</c> to the provider context</item>
        /// <item>From <c>/</c> to the update provider action</item>
        /// <item>From <c>/</c> to the roles view</item>
        /// </list>
        /// </item>
        /// </list>
        /// </remarks>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.CreateProviderUri AsCreateProviderUri(CancellationToken token = default);

        /// <summary>
        /// A flag indicating the create tenant resource is available.
        /// </summary>
        bool HasCreateTenantUri { get; }

        /// <summary>
        /// Start building an API request from the create tenant resource url.
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item>
        /// <term>Navigations</term>
        /// <description>The create tenant resource has 8 navigations</description>
        /// <list type="bullet">
        /// <item>From <c>/</c> to the account context</item>
        /// <item>From <c>/</c> to the send invitation action</item>
        /// <item>From <c>/</c> to the set account status action</item>
        /// <item>From <c>/</c> to the set identity action</item>
        /// <item>From <c>/</c> to the update account action</item>
        /// <item>From <c>/</c> to the update identity action</item>
        /// <item>From <c>/</c> to the assertions view</item>
        /// <item>From <c>/</c> to the policies view</item>
        /// </list>
        /// </item>
        /// </list>
        /// </remarks>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.CreateTenantUri AsCreateTenantUri(CancellationToken token = default);

        /// <summary>
        /// A flag indicating the create user resource is available.
        /// </summary>
        bool HasCreateUserUri { get; }

        /// <summary>
        /// Start building an API request from the create user resource url.
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item>
        /// <term>Navigations</term>
        /// <description>The create user resource has 8 navigations</description>
        /// <list type="bullet">
        /// <item>From <c>/</c> to the account context</item>
        /// <item>From <c>/</c> to the send invitation action</item>
        /// <item>From <c>/</c> to the set account status action</item>
        /// <item>From <c>/</c> to the set identity action</item>
        /// <item>From <c>/</c> to the update account action</item>
        /// <item>From <c>/</c> to the update identity action</item>
        /// <item>From <c>/</c> to the assertions view</item>
        /// <item>From <c>/</c> to the policies view</item>
        /// </list>
        /// </item>
        /// </list>
        /// </remarks>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.CreateUserUri AsCreateUserUri(CancellationToken token = default);

        /// <summary>
        /// A flag indicating the log resource is available.
        /// </summary>
        bool HasLogUri { get; }

        /// <summary>
        /// Start building an API request from the log resource url.
        /// </summary>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.LogUri AsLogUri(CancellationToken token = default);

        /// <summary>
        /// A flag indicating the override account resource is available.
        /// </summary>
        bool HasOverrideAccountUri { get; }

        /// <summary>
        /// Start building an API request from the override account resource url.
        /// </summary>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.OverrideAccountUri AsOverrideAccountUri(CancellationToken token = default);

        /// <summary>
        /// A flag indicating the override account access resource is available.
        /// </summary>
        bool HasOverrideAccountAccessUri { get; }

        /// <summary>
        /// Start building an API request from the override account access resource url.
        /// </summary>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.OverrideAccountAccessUri AsOverrideAccountAccessUri(CancellationToken token = default);

        /// <summary>
        /// A flag indicating the refresh resource is available.
        /// </summary>
        bool HasRefreshUri { get; }

        /// <summary>
        /// Start building an API request from the refresh resource url.
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item>
        /// <term>Navigations</term>
        /// <description>The refresh resource has 3 navigations</description>
        /// <list type="bullet">
        /// <item>From <c>/</c> to the account context</item>
        /// <item>From <c>/tenant</c> to the account context</item>
        /// <item>From <c>/assertions</c> to the user assertion view</item>
        /// </list>
        /// </item>
        /// </list>
        /// </remarks>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.RefreshUri AsRefreshUri(CancellationToken token = default);

        /// <summary>
        /// A flag indicating the refresh profile resource is available.
        /// </summary>
        bool HasRefreshProfileUri { get; }

        /// <summary>
        /// Start building an API request from the refresh profile resource url.
        /// </summary>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.RefreshProfileUri AsRefreshProfileUri(CancellationToken token = default);

        /// <summary>
        /// A flag indicating the register resource is available.
        /// </summary>
        bool HasRegisterUri { get; }

        /// <summary>
        /// Start building an API request from the register resource url.
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item>
        /// <term>Navigations</term>
        /// <description>The register resource has 6 navigations</description>
        /// <list type="bullet">
        /// <item>From <c>/</c> to the artifact context</item>
        /// <item>From <c>/</c> to the create service action</item>
        /// <item>From <c>/</c> to the release action</item>
        /// <item>From <c>/</c> to the set artifact status action</item>
        /// <item>From <c>/</c> to the update artifact action</item>
        /// <item>From <c>/</c> to the services view</item>
        /// </list>
        /// </item>
        /// </list>
        /// </remarks>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.RegisterUri AsRegisterUri(CancellationToken token = default);

        /// <summary>
        /// A flag indicating the register account resource is available.
        /// </summary>
        bool HasRegisterAccountUri { get; }

        /// <summary>
        /// Start building an API request from the register account resource url.
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item>
        /// <term>Navigations</term>
        /// <description>The register account resource has 8 navigations</description>
        /// <list type="bullet">
        /// <item>From <c>/</c> to the account context</item>
        /// <item>From <c>/</c> to the send invitation action</item>
        /// <item>From <c>/</c> to the set account status action</item>
        /// <item>From <c>/</c> to the set identity action</item>
        /// <item>From <c>/</c> to the update account action</item>
        /// <item>From <c>/</c> to the update identity action</item>
        /// <item>From <c>/</c> to the assertions view</item>
        /// <item>From <c>/</c> to the policies view</item>
        /// </list>
        /// </item>
        /// </list>
        /// </remarks>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.RegisterAccountUri AsRegisterAccountUri(CancellationToken token = default);

        /// <summary>
        /// A flag indicating the register app resource is available.
        /// </summary>
        bool HasRegisterAppUri { get; }

        /// <summary>
        /// Start building an API request from the register app resource url.
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item>
        /// <term>Navigations</term>
        /// <description>The register app resource has 1 navigation</description>
        /// <list type="bullet">
        /// <item>From <c>/</c> to the account context</item>
        /// </list>
        /// </item>
        /// </list>
        /// </remarks>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.RegisterAppUri AsRegisterAppUri(CancellationToken token = default);

        /// <summary>
        /// A flag indicating the set release status resource is available.
        /// </summary>
        bool HasSetReleaseStatusUri { get; }

        /// <summary>
        /// Start building an API request from the set release status resource url.
        /// </summary>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.SetReleaseStatusUri AsSetReleaseStatusUri(CancellationToken token = default);

        /// <summary>
        /// A flag indicating the setup resource is available.
        /// </summary>
        bool HasSetupUri { get; }

        /// <summary>
        /// Start building an API request from the setup resource url.
        /// </summary>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.SetupUri AsSetupUri(CancellationToken token = default);

    }


    /// <summary>
    /// A local view of all resources that share a common root.
    /// </summary>
    public interface IAccountResource : ILinkable<Types.AccountDetails>, IResource
    {

        /// <summary>
        /// A flag indicating the assertions resource is available.
        /// </summary>
        bool HasAssertionsUri { get; }

        /// <summary>
        /// Start building an API request from the assertions resource url.
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item>
        /// <term>Navigations</term>
        /// <description>The assertions resource has 1 navigation</description>
        /// <list type="bullet">
        /// <item>From <c>/rows</c> to the assertion context</item>
        /// </list>
        /// </item>
        /// </list>
        /// </remarks>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.AssertionsUri AsAssertionsUri(CancellationToken token = default);

        /// <summary>
        /// A flag indicating the policies resource is available.
        /// </summary>
        bool HasPoliciesUri { get; }

        /// <summary>
        /// Start building an API request from the policies resource url.
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item>
        /// <term>Navigations</term>
        /// <description>The policies resource has 2 navigations</description>
        /// <list type="bullet">
        /// <item>From <c>/rows</c> to the policy context</item>
        /// <item>From <c>/rows</c> to the set account access action</item>
        /// </list>
        /// </item>
        /// </list>
        /// </remarks>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.PoliciesUri AsPoliciesUri(CancellationToken token = default);

        /// <summary>
        /// A flag indicating the send invitation resource is available.
        /// </summary>
        bool HasSendInvitationUri { get; }

        /// <summary>
        /// Start building an API request from the send invitation resource url.
        /// </summary>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.SendInvitationUri AsSendInvitationUri(CancellationToken token = default);

        /// <summary>
        /// A flag indicating the set account status resource is available.
        /// </summary>
        bool HasSetAccountStatusUri { get; }

        /// <summary>
        /// Start building an API request from the set account status resource url.
        /// </summary>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.SetAccountStatusUri AsSetAccountStatusUri(CancellationToken token = default);

        /// <summary>
        /// A flag indicating the set identity resource is available.
        /// </summary>
        bool HasSetIdentityUri { get; }

        /// <summary>
        /// Start building an API request from the set identity resource url.
        /// </summary>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.SetIdentityUri AsSetIdentityUri(CancellationToken token = default);

        /// <summary>
        /// A flag indicating the update account resource is available.
        /// </summary>
        bool HasUpdateAccountUri { get; }

        /// <summary>
        /// Start building an API request from the update account resource url.
        /// </summary>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.UpdateAccountUri AsUpdateAccountUri(CancellationToken token = default);

        /// <summary>
        /// A flag indicating the update identity resource is available.
        /// </summary>
        bool HasUpdateIdentityUri { get; }

        /// <summary>
        /// Start building an API request from the update identity resource url.
        /// </summary>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.UpdateIdentityUri AsUpdateIdentityUri(CancellationToken token = default);

        /// <summary>
        /// Start building an API request from the account resource url.
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item>
        /// <term>Navigations</term>
        /// <description>The account resource has 3 navigations</description>
        /// <list type="bullet">
        /// <item>From <c>/identities/identifier</c> to the set identity action</item>
        /// <item>From <c>/organization</c> to the organization context</item>
        /// <item>From <c>/tenant</c> to the account context</item>
        /// </list>
        /// </item>
        /// </list>
        /// </remarks>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.AccountUri AsAccountUri(CancellationToken token = default);

        /// <summary>
        /// A navigation used to obtain or refresh the account service.
        /// </summary>
        Task<Link<Types.AccountDetails, IAccountResource>> ToAccountAsync(CancellationToken token = default);

    }


    /// <summary>
    /// A local view of all resources that share a common root.
    /// </summary>
    public interface IArtifactResource : ILinkable<Types.ArtifactDetails>, IResource
    {

        /// <summary>
        /// A flag indicating the services resource is available.
        /// </summary>
        bool HasServicesUri { get; }

        /// <summary>
        /// Start building an API request from the services resource url.
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item>
        /// <term>Navigations</term>
        /// <description>The services resource has 1 navigation</description>
        /// <list type="bullet">
        /// <item>From <c>/rows</c> to the service context</item>
        /// </list>
        /// </item>
        /// </list>
        /// </remarks>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.ServicesUri AsServicesUri(CancellationToken token = default);

        /// <summary>
        /// A flag indicating the create service resource is available.
        /// </summary>
        bool HasCreateServiceUri { get; }

        /// <summary>
        /// Start building an API request from the create service resource url.
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item>
        /// <term>Navigations</term>
        /// <description>The create service resource has 8 navigations</description>
        /// <list type="bullet">
        /// <item>From <c>/</c> to the service context</item>
        /// <item>From <c>/</c> to the create resource action</item>
        /// <item>From <c>/</c> to the reset action</item>
        /// <item>From <c>/</c> to the set service status action</item>
        /// <item>From <c>/</c> to the set template action</item>
        /// <item>From <c>/</c> to the update resource action</item>
        /// <item>From <c>/</c> to the update service action</item>
        /// <item>From <c>/</c> to the resources view</item>
        /// </list>
        /// </item>
        /// </list>
        /// </remarks>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.CreateServiceUri AsCreateServiceUri(CancellationToken token = default);

        /// <summary>
        /// A flag indicating the release resource is available.
        /// </summary>
        bool HasReleaseUri { get; }

        /// <summary>
        /// Start building an API request from the release resource url.
        /// </summary>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.ReleaseUri AsReleaseUri(CancellationToken token = default);

        /// <summary>
        /// A flag indicating the set artifact status resource is available.
        /// </summary>
        bool HasSetArtifactStatusUri { get; }

        /// <summary>
        /// Start building an API request from the set artifact status resource url.
        /// </summary>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.SetArtifactStatusUri AsSetArtifactStatusUri(CancellationToken token = default);

        /// <summary>
        /// A flag indicating the update artifact resource is available.
        /// </summary>
        bool HasUpdateArtifactUri { get; }

        /// <summary>
        /// Start building an API request from the update artifact resource url.
        /// </summary>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.UpdateArtifactUri AsUpdateArtifactUri(CancellationToken token = default);

        /// <summary>
        /// Start building an API request from the artifact resource url.
        /// </summary>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.ArtifactUri AsArtifactUri(CancellationToken token = default);

        /// <summary>
        /// A navigation used to obtain or refresh the artifact service.
        /// </summary>
        Task<Link<Types.ArtifactDetails, IArtifactResource>> ToArtifactAsync(CancellationToken token = default);

    }


    /// <summary>
    /// A local view of all resources that share a common root.
    /// </summary>
    public interface IAssertionResource : ILinkable<Types.AssertionDetails>, IResource
    {

        /// <summary>
        /// Start building an API request from the assertion resource url.
        /// </summary>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.AssertionUri AsAssertionUri(CancellationToken token = default);

        /// <summary>
        /// A navigation used to obtain or refresh the assertion service.
        /// </summary>
        Task<Link<Types.AssertionDetails, IAssertionResource>> ToAssertionAsync(CancellationToken token = default);

    }


    /// <summary>
    /// A local view of all resources that share a common root.
    /// </summary>
    public interface IClaimResource : ILinkable<Types.ClaimDetails>, IResource
    {

        /// <summary>
        /// A flag indicating the update claim resource is available.
        /// </summary>
        bool HasUpdateClaimUri { get; }

        /// <summary>
        /// Start building an API request from the update claim resource url.
        /// </summary>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.UpdateClaimUri AsUpdateClaimUri(CancellationToken token = default);

        /// <summary>
        /// Start building an API request from the claim resource url.
        /// </summary>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.ClaimUri AsClaimUri(CancellationToken token = default);

        /// <summary>
        /// A navigation used to obtain or refresh the claim service.
        /// </summary>
        Task<Link<Types.ClaimDetails, IClaimResource>> ToClaimAsync(CancellationToken token = default);

    }


    /// <summary>
    /// A local view of all resources that share a common root.
    /// </summary>
    public interface IOrganizationResource : ILinkable<Types.OrganizationDetails>, IResource
    {

        /// <summary>
        /// A flag indicating the update organization resource is available.
        /// </summary>
        bool HasUpdateOrganizationUri { get; }

        /// <summary>
        /// Start building an API request from the update organization resource url.
        /// </summary>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.UpdateOrganizationUri AsUpdateOrganizationUri(CancellationToken token = default);

        /// <summary>
        /// Start building an API request from the organization resource url.
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item>
        /// <term>Navigations</term>
        /// <description>The organization resource has 1 navigation</description>
        /// <list type="bullet">
        /// <item>From <c>/tenant</c> to the account context</item>
        /// </list>
        /// </item>
        /// </list>
        /// </remarks>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.OrganizationUri AsOrganizationUri(CancellationToken token = default);

        /// <summary>
        /// A navigation used to obtain or refresh the organization service.
        /// </summary>
        Task<Link<Types.OrganizationDetails, IOrganizationResource>> ToOrganizationAsync(CancellationToken token = default);

    }


    /// <summary>
    /// A local view of all resources that share a common root.
    /// </summary>
    public interface IPolicyResource : ILinkable<Types.PolicyDetails>, IResource
    {

        /// <summary>
        /// A flag indicating the set account access resource is available.
        /// </summary>
        bool HasSetAccountAccessUri { get; }

        /// <summary>
        /// Start building an API request from the set account access resource url.
        /// </summary>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.SetAccountAccessUri AsSetAccountAccessUri(CancellationToken token = default);

        /// <summary>
        /// Start building an API request from the policy resource url.
        /// </summary>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.PolicyUri AsPolicyUri(CancellationToken token = default);

        /// <summary>
        /// A navigation used to obtain or refresh the policy service.
        /// </summary>
        Task<Link<Types.PolicyDetails, IPolicyResource>> ToPolicyAsync(CancellationToken token = default);

    }


    /// <summary>
    /// A local view of all resources that share a common root.
    /// </summary>
    public interface IProviderResource : ILinkable<Types.ProviderDetails>, IResource
    {

        /// <summary>
        /// A flag indicating the roles resource is available.
        /// </summary>
        bool HasRolesUri { get; }

        /// <summary>
        /// Start building an API request from the roles resource url.
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item>
        /// <term>Navigations</term>
        /// <description>The roles resource has 1 navigation</description>
        /// <list type="bullet">
        /// <item>From <c>/rows</c> to the role context</item>
        /// </list>
        /// </item>
        /// </list>
        /// </remarks>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.RolesUri AsRolesUri(CancellationToken token = default);

        /// <summary>
        /// A flag indicating the update provider resource is available.
        /// </summary>
        bool HasUpdateProviderUri { get; }

        /// <summary>
        /// Start building an API request from the update provider resource url.
        /// </summary>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.UpdateProviderUri AsUpdateProviderUri(CancellationToken token = default);

        /// <summary>
        /// Start building an API request from the provider resource url.
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item>
        /// <term>Navigations</term>
        /// <description>The provider resource has 3 navigations</description>
        /// <list type="bullet">
        /// <item>From <c>/</c> to the create role action</item>
        /// <item>From <c>/</c> to the create claim action</item>
        /// <item>From <c>/</c> to the role context</item>
        /// </list>
        /// </item>
        /// </list>
        /// </remarks>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.ProviderUri AsProviderUri(CancellationToken token = default);

        /// <summary>
        /// A navigation used to obtain or refresh the provider service.
        /// </summary>
        Task<Link<Types.ProviderDetails, IProviderResource>> ToProviderAsync(CancellationToken token = default);

    }


    /// <summary>
    /// A local view of all resources that share a common root.
    /// </summary>
    public interface IRoleResource : ILinkable<Types.RoleDetails>, IResource
    {

        /// <summary>
        /// A flag indicating the available resources resource is available.
        /// </summary>
        bool HasAvailableResourcesUri { get; }

        /// <summary>
        /// Start building an API request from the available resources resource url.
        /// </summary>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.AvailableResourcesUri AsAvailableResourcesUri(CancellationToken token = default);

        /// <summary>
        /// A flag indicating the create claim resource is available.
        /// </summary>
        bool HasCreateClaimUri { get; }

        /// <summary>
        /// Start building an API request from the create claim resource url.
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item>
        /// <term>Navigations</term>
        /// <description>The create claim resource has 2 navigations</description>
        /// <list type="bullet">
        /// <item>From <c>/</c> to the claim context</item>
        /// <item>From <c>/</c> to the update claim action</item>
        /// </list>
        /// </item>
        /// </list>
        /// </remarks>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.CreateClaimUri AsCreateClaimUri(CancellationToken token = default);

        /// <summary>
        /// A flag indicating the create role resource is available.
        /// </summary>
        bool HasCreateRoleUri { get; }

        /// <summary>
        /// Start building an API request from the create role resource url.
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item>
        /// <term>Navigations</term>
        /// <description>The create role resource has 7 navigations</description>
        /// <list type="bullet">
        /// <item>From <c>/</c> to the role context</item>
        /// <item>From <c>/</c> to the create claim action</item>
        /// <item>From <c>/</c> to the create role action</item>
        /// <item>From <c>/</c> to the set general access action</item>
        /// <item>From <c>/</c> to the set resource access action</item>
        /// <item>From <c>/</c> to the update role action</item>
        /// <item>From <c>/</c> to the available resources view</item>
        /// </list>
        /// </item>
        /// </list>
        /// </remarks>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.CreateRoleUri AsCreateRoleUri(CancellationToken token = default);

        /// <summary>
        /// A flag indicating the set general access resource is available.
        /// </summary>
        bool HasSetGeneralAccessUri { get; }

        /// <summary>
        /// Start building an API request from the set general access resource url.
        /// </summary>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.SetGeneralAccessUri AsSetGeneralAccessUri(CancellationToken token = default);

        /// <summary>
        /// A flag indicating the set resource access resource is available.
        /// </summary>
        bool HasSetResourceAccessUri { get; }

        /// <summary>
        /// Start building an API request from the set resource access resource url.
        /// </summary>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.SetResourceAccessUri AsSetResourceAccessUri(CancellationToken token = default);

        /// <summary>
        /// A flag indicating the update role resource is available.
        /// </summary>
        bool HasUpdateRoleUri { get; }

        /// <summary>
        /// Start building an API request from the update role resource url.
        /// </summary>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.UpdateRoleUri AsUpdateRoleUri(CancellationToken token = default);

        /// <summary>
        /// Start building an API request from the role resource url.
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item>
        /// <term>Navigations</term>
        /// <description>The role resource has 2 navigations</description>
        /// <list type="bullet">
        /// <item>From <c>/</c> to the roles view</item>
        /// <item>From <c>/</c> to the claims view</item>
        /// </list>
        /// </item>
        /// </list>
        /// </remarks>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.RoleUri AsRoleUri(CancellationToken token = default);

        /// <summary>
        /// A navigation used to obtain or refresh the role service.
        /// </summary>
        Task<Link<Types.RoleDetails, IRoleResource>> ToRoleAsync(CancellationToken token = default);

    }


    /// <summary>
    /// A local view of all resources that share a common root.
    /// </summary>
    public interface IServiceResource : ILinkable<Types.ServiceDetails>, IResource
    {

        /// <summary>
        /// A flag indicating the resources resource is available.
        /// </summary>
        bool HasResourcesUri { get; }

        /// <summary>
        /// Start building an API request from the resources resource url.
        /// </summary>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.ResourcesUri AsResourcesUri(CancellationToken token = default);

        /// <summary>
        /// A flag indicating the create resource resource is available.
        /// </summary>
        bool HasCreateResourceUri { get; }

        /// <summary>
        /// Start building an API request from the create resource resource url.
        /// </summary>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.CreateResourceUri AsCreateResourceUri(CancellationToken token = default);

        /// <summary>
        /// A flag indicating the reset resource is available.
        /// </summary>
        bool HasResetUri { get; }

        /// <summary>
        /// Start building an API request from the reset resource url.
        /// </summary>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.ResetUri AsResetUri(CancellationToken token = default);

        /// <summary>
        /// A flag indicating the set service status resource is available.
        /// </summary>
        bool HasSetServiceStatusUri { get; }

        /// <summary>
        /// Start building an API request from the set service status resource url.
        /// </summary>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.SetServiceStatusUri AsSetServiceStatusUri(CancellationToken token = default);

        /// <summary>
        /// A flag indicating the set template resource is available.
        /// </summary>
        bool HasSetTemplateUri { get; }

        /// <summary>
        /// Start building an API request from the set template resource url.
        /// </summary>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.SetTemplateUri AsSetTemplateUri(CancellationToken token = default);

        /// <summary>
        /// A flag indicating the update resource resource is available.
        /// </summary>
        bool HasUpdateResourceUri { get; }

        /// <summary>
        /// Start building an API request from the update resource resource url.
        /// </summary>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.UpdateResourceUri AsUpdateResourceUri(CancellationToken token = default);

        /// <summary>
        /// A flag indicating the update service resource is available.
        /// </summary>
        bool HasUpdateServiceUri { get; }

        /// <summary>
        /// Start building an API request from the update service resource url.
        /// </summary>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.UpdateServiceUri AsUpdateServiceUri(CancellationToken token = default);

        /// <summary>
        /// Start building an API request from the service resource url.
        /// </summary>
        /// <exception cref="System.Security.SecurityException">Thrown when the url is unavailable.</exception>
        Resources.ServiceUri AsServiceUri(CancellationToken token = default);

        /// <summary>
        /// A navigation used to obtain or refresh the service service.
        /// </summary>
        Task<Link<Types.ServiceDetails, IServiceResource>> ToServiceAsync(CancellationToken token = default);

    }

    /// <summary>
    /// A mutable intermediate form for a requested account context
    /// </summary>
    public sealed class AccountUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the account context
        /// </summary>
        /// <remarks>
        /// A utility method for a string uri.
        /// </remarks>
        public AccountUri(
            string uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Create a new UriBuilder for the account context
        /// </summary>
        public AccountUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Fetch details about an individual account.
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item>
        /// <term>Navigations</term>
        /// <description>The account resource has 3 navigations</description>
        /// <list type="bullet">
        /// <item>From <c>/identities/identifier</c> to the set identity action</item>
        /// <item>From <c>/organization</c> to the organization context</item>
        /// <item>From <c>/tenant</c> to the account context</item>
        /// </list>
        /// </item>
        /// </list>
        /// </remarks>
        public async Task<Types.AccountDetails> GetAsync() => await handler.GetFromJsonAsync<Types.AccountDetails>(
            this.Uri,
            token) ?? throw new ServiceException(HttpStatusCode.OK, new ServiceError(
                "evaluation.3000",
                "An unexpected empty response was received from the server",
                "The account service returned an empty response, a 404 [NotFound] or a valid payload are expected."));

    }

    /// <summary>
    /// A mutable intermediate form for a requested artifact context
    /// </summary>
    public sealed class ArtifactUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the artifact context
        /// </summary>
        /// <remarks>
        /// A utility method for a string uri.
        /// </remarks>
        public ArtifactUri(
            string uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Create a new UriBuilder for the artifact context
        /// </summary>
        public ArtifactUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Fetch information about an artifact.
        /// </summary>
        public async Task<Types.ArtifactDetails> GetAsync() => await handler.GetFromJsonAsync<Types.ArtifactDetails>(
            this.Uri,
            token) ?? throw new ServiceException(HttpStatusCode.OK, new ServiceError(
                "evaluation.3000",
                "An unexpected empty response was received from the server",
                "The artifact service returned an empty response, a 404 [NotFound] or a valid payload are expected."));

    }

    /// <summary>
    /// A mutable intermediate form for a requested assertion context
    /// </summary>
    public sealed class AssertionUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the assertion context
        /// </summary>
        /// <remarks>
        /// A utility method for a string uri.
        /// </remarks>
        public AssertionUri(
            string uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Create a new UriBuilder for the assertion context
        /// </summary>
        public AssertionUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// A specific asserted claim.
        /// </summary>
        public async Task<Types.AssertionDetails> GetAsync() => await handler.GetFromJsonAsync<Types.AssertionDetails>(
            this.Uri,
            token) ?? throw new ServiceException(HttpStatusCode.OK, new ServiceError(
                "evaluation.3000",
                "An unexpected empty response was received from the server",
                "The assertion service returned an empty response, a 404 [NotFound] or a valid payload are expected."));

    }

    /// <summary>
    /// A mutable intermediate form for a requested claim context
    /// </summary>
    public sealed class ClaimUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the claim context
        /// </summary>
        /// <remarks>
        /// A utility method for a string uri.
        /// </remarks>
        public ClaimUri(
            string uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Create a new UriBuilder for the claim context
        /// </summary>
        public ClaimUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Fetch details about a security claim.
        /// </summary>
        public async Task<Types.ClaimDetails> GetAsync() => await handler.GetFromJsonAsync<Types.ClaimDetails>(
            this.Uri,
            token) ?? throw new ServiceException(HttpStatusCode.OK, new ServiceError(
                "evaluation.3000",
                "An unexpected empty response was received from the server",
                "The claim service returned an empty response, a 404 [NotFound] or a valid payload are expected."));

    }

    /// <summary>
    /// A mutable intermediate form for a requested organization context
    /// </summary>
    public sealed class OrganizationUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the organization context
        /// </summary>
        /// <remarks>
        /// A utility method for a string uri.
        /// </remarks>
        public OrganizationUri(
            string uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Create a new UriBuilder for the organization context
        /// </summary>
        public OrganizationUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Information about an organization.
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item>
        /// <term>Navigations</term>
        /// <description>The organization resource has 1 navigation</description>
        /// <list type="bullet">
        /// <item>From <c>/tenant</c> to the account context</item>
        /// </list>
        /// </item>
        /// </list>
        /// </remarks>
        public async Task<Types.OrganizationDetails> GetAsync() => await handler.GetFromJsonAsync<Types.OrganizationDetails>(
            this.Uri,
            token) ?? throw new ServiceException(HttpStatusCode.OK, new ServiceError(
                "evaluation.3000",
                "An unexpected empty response was received from the server",
                "The organization service returned an empty response, a 404 [NotFound] or a valid payload are expected."));

    }

    /// <summary>
    /// A mutable intermediate form for a requested policy context
    /// </summary>
    public sealed class PolicyUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the policy context
        /// </summary>
        /// <remarks>
        /// A utility method for a string uri.
        /// </remarks>
        public PolicyUri(
            string uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Create a new UriBuilder for the policy context
        /// </summary>
        public PolicyUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Information about a user policy.
        /// </summary>
        public async Task<Types.PolicyDetails> GetAsync() => await handler.GetFromJsonAsync<Types.PolicyDetails>(
            this.Uri,
            token) ?? throw new ServiceException(HttpStatusCode.OK, new ServiceError(
                "evaluation.3000",
                "An unexpected empty response was received from the server",
                "The policy service returned an empty response, a 404 [NotFound] or a valid payload are expected."));

    }

    /// <summary>
    /// A mutable intermediate form for a requested provider context
    /// </summary>
    public sealed class ProviderUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the provider context
        /// </summary>
        /// <remarks>
        /// A utility method for a string uri.
        /// </remarks>
        public ProviderUri(
            string uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Create a new UriBuilder for the provider context
        /// </summary>
        public ProviderUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// An authentication provider.
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item>
        /// <term>Navigations</term>
        /// <description>The provider resource has 3 navigations</description>
        /// <list type="bullet">
        /// <item>From <c>/</c> to the create role action</item>
        /// <item>From <c>/</c> to the create claim action</item>
        /// <item>From <c>/</c> to the role context</item>
        /// </list>
        /// </item>
        /// </list>
        /// </remarks>
        public async Task<Types.ProviderDetails> GetAsync() => await handler.GetFromJsonAsync<Types.ProviderDetails>(
            this.Uri,
            token) ?? throw new ServiceException(HttpStatusCode.OK, new ServiceError(
                "evaluation.3000",
                "An unexpected empty response was received from the server",
                "The provider service returned an empty response, a 404 [NotFound] or a valid payload are expected."));

    }

    /// <summary>
    /// A mutable intermediate form for a requested role context
    /// </summary>
    public sealed class RoleUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the role context
        /// </summary>
        /// <remarks>
        /// A utility method for a string uri.
        /// </remarks>
        public RoleUri(
            string uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Create a new UriBuilder for the role context
        /// </summary>
        public RoleUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Fetch details about a specific policy group.
        /// </summary>
        /// <remarks>
        /// <list type="table">
        /// <item>
        /// <term>Navigations</term>
        /// <description>The role resource has 2 navigations</description>
        /// <list type="bullet">
        /// <item>From <c>/</c> to the roles view</item>
        /// <item>From <c>/</c> to the claims view</item>
        /// </list>
        /// </item>
        /// </list>
        /// </remarks>
        public async Task<Types.RoleDetails> GetAsync() => await handler.GetFromJsonAsync<Types.RoleDetails>(
            this.Uri,
            token) ?? throw new ServiceException(HttpStatusCode.OK, new ServiceError(
                "evaluation.3000",
                "An unexpected empty response was received from the server",
                "The role service returned an empty response, a 404 [NotFound] or a valid payload are expected."));

    }

    /// <summary>
    /// A mutable intermediate form for a requested service context
    /// </summary>
    public sealed class ServiceUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the service context
        /// </summary>
        /// <remarks>
        /// A utility method for a string uri.
        /// </remarks>
        public ServiceUri(
            string uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Create a new UriBuilder for the service context
        /// </summary>
        public ServiceUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Fetch information about a resource group.
        /// </summary>
        public async Task<Types.ServiceDetails> GetAsync() => await handler.GetFromJsonAsync<Types.ServiceDetails>(
            this.Uri,
            token) ?? throw new ServiceException(HttpStatusCode.OK, new ServiceError(
                "evaluation.3000",
                "An unexpected empty response was received from the server",
                "The service service returned an empty response, a 404 [NotFound] or a valid payload are expected."));

    }

    /// <summary>
    /// A mutable intermediate form for a requested accounts view
    /// </summary>
    public sealed class AccountsUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the accounts view
        /// </summary>
        public AccountsUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// List all user accounts.
        /// </summary>
        /// <remarks>
        /// This method assumes the URI was provided fully populated.
        /// <list type="table">
        /// <item>
        /// <term>Navigations</term>
        /// <description>The accounts resource has 1 navigation</description>
        /// <list type="bullet">
        /// <item>From <c>/rows</c> to the account context</item>
        /// </list>
        /// </item>
        /// </list>
        /// </remarks>
        public Task<meta::IResultSet<Types.AccountMetadata>> GetAsync() => handler.ScrollFromJsonAsync<Types.AccountMetadata>(
            this.Uri,
            token,
            lazy: false);

        /// <summary>
        /// A utility method to update the query parameters for this URI
        /// </summary>
        /// <param name="keys">All claim keys to include in the profile.</param>
        /// <param name="filter">Filter the account list with this value.</param>
        /// <param name="claim">Apply the filter to assertions of this claim.</param>
        /// <param name="desc">A flag indicating the list should be scrolled in descending order.</param>
        /// <param name="organizationId">The tenant organization identifier.</param>
        /// <remarks>
        /// This method mutates the URI and replaces all query parameters with the provided values.
        /// </remarks>
        public AccountsUri AsQuery(
            List<string> keys,
            string? filter = null,
            string? claim = null,
            bool? desc = null,
            Guid? organizationId = null)
        {
            this.Query = string.Join("&", AsParameterStream(
                keys,
                filter,
                claim,
                desc,
                organizationId));
            return this;
        }

        /// <summary>
        /// List all user accounts.
        /// </summary>
        /// <remarks>
        /// This method provides lazy pagination.
        /// <list type="table">
        /// <item>
        /// <term>Navigations</term>
        /// <description>The accounts resource has 1 navigation</description>
        /// <list type="bullet">
        /// <item>From <c>/rows</c> to the account context</item>
        /// </list>
        /// </item>
        /// </list>
        /// </remarks>
        public Task<meta::IResultSet<Types.AccountMetadata>> PageAsync(string? query = null)
        {
            this.Query = query;
            return handler.ScrollFromJsonAsync<Types.AccountMetadata>(
                this.Uri,
                token,
                lazy: true);
        }

        /// <summary>
        /// A utility method to simplify query string generation.
        /// </summary>
        private static IEnumerable<string> AsParameterStream(
            List<string> keys,
            string? filter = null,
            string? claim = null,
            bool? desc = null,
            Guid? organizationId = null)
        {
            foreach (var i in keys) yield return string.Format(
                "keys={0}",
                Uri.EscapeDataString(i));
            if (filter != null) yield return string.Format(
                "filter={0}",
                Uri.EscapeDataString(filter));
            if (claim != null) yield return string.Format(
                "claim={0}",
                Uri.EscapeDataString(claim));
            if (desc != null) yield return string.Format(
                "desc={0}",
                desc);
            if (organizationId != null) yield return string.Format(
                "organization_id={0}",
                organizationId.Value.ToString("n"));
        }

    }

    /// <summary>
    /// A mutable intermediate form for a requested affected view
    /// </summary>
    public sealed class AffectedUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the affected view
        /// </summary>
        public AffectedUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Relevant audit entries for a resource.
        /// </summary>
        /// <remarks>
        /// This method assumes the URI was provided fully populated.
        /// </remarks>
        public Task<meta::IResultSet<Types.AuditEvent>> GetAsync() => handler.ScrollFromJsonAsync<Types.AuditEvent>(
            this.Uri,
            token,
            lazy: false);

        /// <summary>
        /// A utility method to update the query parameters for this URI
        /// </summary>
        /// <param name="resourceId">The resource identifier.</param>
        /// <param name="key">The entity identifier, if audit entries affecting a single entity are required.</param>
        /// <remarks>
        /// This method mutates the URI and replaces all query parameters with the provided values.
        /// </remarks>
        public AffectedUri AsQuery(
            Guid resourceId,
            string? key = null)
        {
            this.Query = string.Join("&", AsParameterStream(
                resourceId,
                key));
            return this;
        }

        /// <summary>
        /// Relevant audit entries for a resource.
        /// </summary>
        /// <remarks>
        /// This method provides lazy pagination.
        /// </remarks>
        public Task<meta::IResultSet<Types.AuditEvent>> PageAsync(string? query = null)
        {
            this.Query = query;
            return handler.ScrollFromJsonAsync<Types.AuditEvent>(
                this.Uri,
                token,
                lazy: true);
        }

        /// <summary>
        /// A utility method to simplify query string generation.
        /// </summary>
        private static IEnumerable<string> AsParameterStream(
            Guid resourceId,
            string? key = null)
        {
            yield return string.Format(
                "resource_id={0}",
                resourceId.ToString("n"));
            if (key != null) yield return string.Format(
                "key={0}",
                Uri.EscapeDataString(key));
        }

    }

    /// <summary>
    /// A mutable intermediate form for a requested artifacts view
    /// </summary>
    public sealed class ArtifactsUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the artifacts view
        /// </summary>
        public ArtifactsUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// List all available artifacts within the current issuer.
        /// </summary>
        /// <remarks>
        /// This method assumes the URI was provided fully populated.
        /// <list type="table">
        /// <item>
        /// <term>Navigations</term>
        /// <description>The artifacts resource has 1 navigation</description>
        /// <list type="bullet">
        /// <item>From <c>/rows</c> to the artifact context</item>
        /// </list>
        /// </item>
        /// </list>
        /// </remarks>
        public Task<meta::IResultSet<Types.ArtifactMetadata>> GetAsync() => handler.ScrollFromJsonAsync<Types.ArtifactMetadata>(
            this.Uri,
            token,
            lazy: false);

    }

    /// <summary>
    /// A mutable intermediate form for a requested assertions view
    /// </summary>
    public sealed class AssertionsUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the assertions view
        /// </summary>
        public AssertionsUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// List all security assertions for an account.
        /// </summary>
        /// <remarks>
        /// This method assumes the URI was provided fully populated.
        /// <list type="table">
        /// <item>
        /// <term>Navigations</term>
        /// <description>The assertions resource has 1 navigation</description>
        /// <list type="bullet">
        /// <item>From <c>/rows</c> to the assertion context</item>
        /// </list>
        /// </item>
        /// </list>
        /// </remarks>
        public Task<meta::IResultSet<Types.AssertionMetadata>> GetAsync() => handler.ScrollFromJsonAsync<Types.AssertionMetadata>(
            this.Uri,
            token,
            lazy: false);

    }

    /// <summary>
    /// A mutable intermediate form for a requested audit view
    /// </summary>
    public sealed class AuditUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the audit view
        /// </summary>
        public AuditUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// All audit log entries in descending chronological order.
        /// </summary>
        /// <remarks>
        /// This method assumes the URI was provided fully populated.
        /// </remarks>
        public Task<meta::IResultSet<Types.AuditEvent>> GetAsync() => handler.ScrollFromJsonAsync<Types.AuditEvent>(
            this.Uri,
            token,
            lazy: false);

    }

    /// <summary>
    /// A mutable intermediate form for a requested available policies view
    /// </summary>
    public sealed class AvailablePoliciesUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the available policies view
        /// </summary>
        public AvailablePoliciesUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Review available policies for an account.
        /// </summary>
        /// <remarks>
        /// This method assumes the URI was provided fully populated.
        /// </remarks>
        public Task<meta::IResultSet<Types.PolicyMetadata>> GetAsync() => handler.ScrollFromJsonAsync<Types.PolicyMetadata>(
            this.Uri,
            token,
            lazy: false);

        /// <summary>
        /// A utility method to update the query parameters for this URI
        /// </summary>
        /// <param name="accountId">The account identifier.</param>
        /// <param name="administratorId">The administrator account being analysed.</param>
        /// <remarks>
        /// This method mutates the URI and replaces all query parameters with the provided values.
        /// </remarks>
        public AvailablePoliciesUri AsQuery(
            Guid accountId,
            Guid administratorId)
        {
            this.Query = string.Join("&", AsParameterStream(
                accountId,
                administratorId));
            return this;
        }

        /// <summary>
        /// Review available policies for an account.
        /// </summary>
        /// <remarks>
        /// This method provides lazy pagination.
        /// </remarks>
        public Task<meta::IResultSet<Types.PolicyMetadata>> PageAsync(string? query = null)
        {
            this.Query = query;
            return handler.ScrollFromJsonAsync<Types.PolicyMetadata>(
                this.Uri,
                token,
                lazy: true);
        }

        /// <summary>
        /// A utility method to simplify query string generation.
        /// </summary>
        private static IEnumerable<string> AsParameterStream(
            Guid accountId,
            Guid administratorId)
        {
            yield return string.Format(
                "account_id={0}",
                accountId.ToString("n"));
            yield return string.Format(
                "administrator_id={0}",
                administratorId.ToString("n"));
        }

    }

    /// <summary>
    /// A mutable intermediate form for a requested available resources view
    /// </summary>
    public sealed class AvailableResourcesUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the available resources view
        /// </summary>
        public AvailableResourcesUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Resources that are available to a role.
        /// </summary>
        /// <remarks>
        /// This method assumes the URI was provided fully populated.
        /// </remarks>
        public Task<meta::IResultSet<Types.RoleResource>> GetAsync() => handler.ScrollFromJsonAsync<Types.RoleResource>(
            this.Uri,
            token,
            lazy: false);

        /// <summary>
        /// A utility method to update the query parameters for this URI
        /// </summary>
        /// <param name="editing">A flag indicating the user is editing the role and needs access to all available resources from the parent.</param>
        /// <remarks>
        /// This method mutates the URI and replaces all query parameters with the provided values.
        /// </remarks>
        public AvailableResourcesUri AsQuery(
            bool? editing = null)
        {
            this.Query = string.Join("&", AsParameterStream(
                editing));
            return this;
        }

        /// <summary>
        /// Resources that are available to a role.
        /// </summary>
        /// <remarks>
        /// This method provides lazy pagination.
        /// </remarks>
        public Task<meta::IResultSet<Types.RoleResource>> PageAsync(string? query = null)
        {
            this.Query = query;
            return handler.ScrollFromJsonAsync<Types.RoleResource>(
                this.Uri,
                token,
                lazy: true);
        }

        /// <summary>
        /// A utility method to simplify query string generation.
        /// </summary>
        private static IEnumerable<string> AsParameterStream(
            bool? editing = null)
        {
            if (editing != null) yield return string.Format(
                "editing={0}",
                editing);
        }

    }

    /// <summary>
    /// A mutable intermediate form for a requested claims view
    /// </summary>
    public sealed class ClaimsUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the claims view
        /// </summary>
        public ClaimsUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// All available claims.
        /// </summary>
        /// <remarks>
        /// This method assumes the URI was provided fully populated.
        /// <list type="table">
        /// <item>
        /// <term>Navigations</term>
        /// <description>The claims resource has 2 navigations</description>
        /// <list type="bullet">
        /// <item>From <c>/rows</c> to the claim context</item>
        /// <item>From <c>/rows</c> to the update claim action</item>
        /// </list>
        /// </item>
        /// </list>
        /// </remarks>
        public Task<meta::IResultSet<Types.ClaimMetadata>> GetAsync() => handler.ScrollFromJsonAsync<Types.ClaimMetadata>(
            this.Uri,
            token,
            lazy: false);

        /// <summary>
        /// A utility method to update the query parameters for this URI
        /// </summary>
        /// <param name="provider">The referenced provider key, or claims from all providers if omitted.</param>
        /// <param name="policyGroup">The referenced policy group, or claims from all policy groups if omitted.</param>
        /// <remarks>
        /// This method mutates the URI and replaces all query parameters with the provided values.
        /// </remarks>
        public ClaimsUri AsQuery(
            string? provider = null,
            string? policyGroup = null)
        {
            this.Query = string.Join("&", AsParameterStream(
                provider,
                policyGroup));
            return this;
        }

        /// <summary>
        /// All available claims.
        /// </summary>
        /// <remarks>
        /// This method provides lazy pagination.
        /// <list type="table">
        /// <item>
        /// <term>Navigations</term>
        /// <description>The claims resource has 2 navigations</description>
        /// <list type="bullet">
        /// <item>From <c>/rows</c> to the claim context</item>
        /// <item>From <c>/rows</c> to the update claim action</item>
        /// </list>
        /// </item>
        /// </list>
        /// </remarks>
        public Task<meta::IResultSet<Types.ClaimMetadata>> PageAsync(string? query = null)
        {
            this.Query = query;
            return handler.ScrollFromJsonAsync<Types.ClaimMetadata>(
                this.Uri,
                token,
                lazy: true);
        }

        /// <summary>
        /// A utility method to simplify query string generation.
        /// </summary>
        private static IEnumerable<string> AsParameterStream(
            string? provider = null,
            string? policyGroup = null)
        {
            if (provider != null) yield return string.Format(
                "provider={0}",
                Uri.EscapeDataString(provider));
            if (policyGroup != null) yield return string.Format(
                "policy_group={0}",
                Uri.EscapeDataString(policyGroup));
        }

    }

    /// <summary>
    /// A mutable intermediate form for a requested evictions view
    /// </summary>
    public sealed class EvictionsUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the evictions view
        /// </summary>
        public EvictionsUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Eviction information for use by downstream authorization
        /// caches.
        /// </summary>
        /// <remarks>
        /// This method assumes the URI was provided fully populated.
        /// </remarks>
        public Task<Types.CacheSummary?> GetAsync() => handler.GetFromJsonAsync<Types.CacheSummary>(
            this.Uri,
            token);

        /// <summary>
        /// A utility method to update the query parameters for this URI
        /// </summary>
        /// <param name="timestamp">The UNIX timestamp (seconds) for the beginning of the eviction analysis.</param>
        /// <param name="events">The total number of events to include in the eviction analysis.</param>
        /// <param name="key">The audit event key for the start of the eviction period, if a prior analysis is being resumed.</param>
        /// <remarks>
        /// This method mutates the URI and replaces all query parameters with the provided values.
        /// </remarks>
        public EvictionsUri AsQuery(
            long timestamp,
            int events,
            Guid? key = null)
        {
            this.Query = string.Join("&", AsParameterStream(
                timestamp,
                events,
                key));
            return this;
        }

        /// <summary>
        /// A utility method to simplify query string generation.
        /// </summary>
        private static IEnumerable<string> AsParameterStream(
            long timestamp,
            int events,
            Guid? key = null)
        {
            yield return string.Format(
                "timestamp={0}",
                timestamp);
            yield return string.Format(
                "events={0}",
                events);
            if (key != null) yield return string.Format(
                "key={0}",
                key.Value.ToString("n"));
        }

    }

    /// <summary>
    /// A mutable intermediate form for a requested lookup account view
    /// </summary>
    public sealed class LookupAccountUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the lookup account view
        /// </summary>
        public LookupAccountUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Look up an account by the value of a unique key.
        /// </summary>
        /// <remarks>
        /// This method assumes the URI was provided fully populated.
        /// <list type="table">
        /// <item>
        /// <term>Navigations</term>
        /// <description>The lookup account resource has 2 navigations</description>
        /// <list type="bullet">
        /// <item>From <c>/</c> to the account context</item>
        /// <item>From <c>/</c> to the update identity action</item>
        /// </list>
        /// </item>
        /// </list>
        /// </remarks>
        public Task<Types.AccountAssertion?> GetAsync() => handler.GetFromJsonAsync<Types.AccountAssertion>(
            this.Uri,
            token);

        /// <summary>
        /// A utility method to update the query parameters for this URI
        /// </summary>
        /// <param name="key">The claim key.</param>
        /// <param name="value">The asserted claim value.</param>
        /// <param name="tenant">The value of the associated tenant claim, if the key claim is associated with a multi-tenanted identity.</param>
        /// <remarks>
        /// This method mutates the URI and replaces all query parameters with the provided values.
        /// </remarks>
        public LookupAccountUri AsQuery(
            string key,
            string value,
            string? tenant = null)
        {
            this.Query = string.Join("&", AsParameterStream(
                key,
                value,
                tenant));
            return this;
        }

        /// <summary>
        /// A utility method to simplify query string generation.
        /// </summary>
        private static IEnumerable<string> AsParameterStream(
            string key,
            string value,
            string? tenant = null)
        {
            yield return string.Format(
                "key={0}",
                Uri.EscapeDataString(key));
            yield return string.Format(
                "value={0}",
                Uri.EscapeDataString(value));
            if (tenant != null) yield return string.Format(
                "tenant={0}",
                Uri.EscapeDataString(tenant));
        }

    }

    /// <summary>
    /// A mutable intermediate form for a requested manifest view
    /// </summary>
    public sealed class ManifestUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the manifest view
        /// </summary>
        public ManifestUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// A manifest containing the permissions schema for a released
        /// service.
        /// </summary>
        /// <remarks>
        /// This method assumes the URI was provided fully populated.
        /// </remarks>
        public Task<Types.AppManifest?> GetAsync() => handler.GetFromJsonAsync<Types.AppManifest>(
            this.Uri,
            token);

        /// <summary>
        /// A utility method to update the query parameters for this URI
        /// </summary>
        /// <param name="artifact">The artifact name.</param>
        /// <param name="version">The schema version. If omitted, the latest (perhaps unreleased) state will be used.</param>
        /// <remarks>
        /// This method mutates the URI and replaces all query parameters with the provided values.
        /// </remarks>
        public ManifestUri AsQuery(
            string artifact,
            int? version = null)
        {
            this.Query = string.Join("&", AsParameterStream(
                artifact,
                version));
            return this;
        }

        /// <summary>
        /// A utility method to simplify query string generation.
        /// </summary>
        private static IEnumerable<string> AsParameterStream(
            string artifact,
            int? version = null)
        {
            yield return string.Format(
                "artifact={0}",
                Uri.EscapeDataString(artifact));
            if (version != null) yield return string.Format(
                "version={0}",
                version);
        }

    }

    /// <summary>
    /// A mutable intermediate form for a requested organizations view
    /// </summary>
    public sealed class OrganizationsUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the organizations view
        /// </summary>
        public OrganizationsUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// List all organization managed by the current account.
        /// </summary>
        /// <remarks>
        /// This method assumes the URI was provided fully populated.
        /// </remarks>
        public Task<meta::IResultSet<Types.OrganizationMetadata>> GetAsync() => handler.ScrollFromJsonAsync<Types.OrganizationMetadata>(
            this.Uri,
            token,
            lazy: false);

    }

    /// <summary>
    /// A mutable intermediate form for a requested parse view
    /// </summary>
    public sealed class ParseUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the parse view
        /// </summary>
        public ParseUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Parse an audit event.
        /// </summary>
        /// <remarks>
        /// This method assumes the URI was provided fully populated.
        /// <list type="table">
        /// <item>
        /// <term>Navigations</term>
        /// <description>The parse resource has 1 navigation</description>
        /// <list type="bullet">
        /// <item>From <c>/</c> to the set template action</item>
        /// </list>
        /// </item>
        /// </list>
        /// </remarks>
        public Task<Types.ParseResult?> GetAsync() => handler.GetFromJsonAsync<Types.ParseResult>(
            this.Uri,
            token);

        /// <summary>
        /// A utility method to update the query parameters for this URI
        /// </summary>
        /// <param name="auditId">The audit message identifier.</param>
        /// <remarks>
        /// This method mutates the URI and replaces all query parameters with the provided values.
        /// </remarks>
        public ParseUri AsQuery(
            Guid auditId)
        {
            this.Query = string.Join("&", AsParameterStream(
                auditId));
            return this;
        }

        /// <summary>
        /// A utility method to simplify query string generation.
        /// </summary>
        private static IEnumerable<string> AsParameterStream(
            Guid auditId)
        {
            yield return string.Format(
                "audit_id={0}",
                auditId.ToString("n"));
        }

    }

    /// <summary>
    /// A mutable intermediate form for a requested permissions view
    /// </summary>
    public sealed class PermissionsUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the permissions view
        /// </summary>
        public PermissionsUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// List all permissions mappings for a claim.
        /// </summary>
        /// <remarks>
        /// This method assumes the URI was provided fully populated.
        /// </remarks>
        public Task<meta::IResultSet<Types.ServicePermission>> GetAsync() => handler.ScrollFromJsonAsync<Types.ServicePermission>(
            this.Uri,
            token,
            lazy: false);

        /// <summary>
        /// A utility method to update the query parameters for this URI
        /// </summary>
        /// <param name="claim">The claim key.</param>
        /// <param name="version">The claim version.</param>
        /// <remarks>
        /// This method mutates the URI and replaces all query parameters with the provided values.
        /// </remarks>
        public PermissionsUri AsQuery(
            string claim,
            int version)
        {
            this.Query = string.Join("&", AsParameterStream(
                claim,
                version));
            return this;
        }

        /// <summary>
        /// List all permissions mappings for a claim.
        /// </summary>
        /// <remarks>
        /// This method provides lazy pagination.
        /// </remarks>
        public Task<meta::IResultSet<Types.ServicePermission>> PageAsync(string? query = null)
        {
            this.Query = query;
            return handler.ScrollFromJsonAsync<Types.ServicePermission>(
                this.Uri,
                token,
                lazy: true);
        }

        /// <summary>
        /// A utility method to simplify query string generation.
        /// </summary>
        private static IEnumerable<string> AsParameterStream(
            string claim,
            int version)
        {
            yield return string.Format(
                "claim={0}",
                Uri.EscapeDataString(claim));
            yield return string.Format(
                "version={0}",
                version);
        }

    }

    /// <summary>
    /// A mutable intermediate form for a requested policies view
    /// </summary>
    public sealed class PoliciesUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the policies view
        /// </summary>
        public PoliciesUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Fetch all policies that apply to the account.
        /// </summary>
        /// <remarks>
        /// This method assumes the URI was provided fully populated.
        /// <list type="table">
        /// <item>
        /// <term>Navigations</term>
        /// <description>The policies resource has 2 navigations</description>
        /// <list type="bullet">
        /// <item>From <c>/rows</c> to the policy context</item>
        /// <item>From <c>/rows</c> to the set account access action</item>
        /// </list>
        /// </item>
        /// </list>
        /// </remarks>
        public Task<meta::IResultSet<Types.PolicyMetadata>> GetAsync() => handler.ScrollFromJsonAsync<Types.PolicyMetadata>(
            this.Uri,
            token,
            lazy: false);

        /// <summary>
        /// A utility method to update the query parameters for this URI
        /// </summary>
        /// <param name="editing">A flag indicating the policy list is being edited and should return all policies where the calling account has rights.</param>
        /// <remarks>
        /// This method mutates the URI and replaces all query parameters with the provided values.
        /// </remarks>
        public PoliciesUri AsQuery(
            bool? editing = null)
        {
            this.Query = string.Join("&", AsParameterStream(
                editing));
            return this;
        }

        /// <summary>
        /// Fetch all policies that apply to the account.
        /// </summary>
        /// <remarks>
        /// This method provides lazy pagination.
        /// <list type="table">
        /// <item>
        /// <term>Navigations</term>
        /// <description>The policies resource has 2 navigations</description>
        /// <list type="bullet">
        /// <item>From <c>/rows</c> to the policy context</item>
        /// <item>From <c>/rows</c> to the set account access action</item>
        /// </list>
        /// </item>
        /// </list>
        /// </remarks>
        public Task<meta::IResultSet<Types.PolicyMetadata>> PageAsync(string? query = null)
        {
            this.Query = query;
            return handler.ScrollFromJsonAsync<Types.PolicyMetadata>(
                this.Uri,
                token,
                lazy: true);
        }

        /// <summary>
        /// A utility method to simplify query string generation.
        /// </summary>
        private static IEnumerable<string> AsParameterStream(
            bool? editing = null)
        {
            if (editing != null) yield return string.Format(
                "editing={0}",
                editing);
        }

    }

    /// <summary>
    /// A mutable intermediate form for a requested providers view
    /// </summary>
    public sealed class ProvidersUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the providers view
        /// </summary>
        public ProvidersUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// A list of all available authentication providers.
        /// </summary>
        /// <remarks>
        /// This method assumes the URI was provided fully populated.
        /// <list type="table">
        /// <item>
        /// <term>Navigations</term>
        /// <description>The providers resource has 1 navigation</description>
        /// <list type="bullet">
        /// <item>From <c>/rows</c> to the provider context</item>
        /// </list>
        /// </item>
        /// </list>
        /// </remarks>
        public Task<meta::IResultSet<Types.ProviderMetadata>> GetAsync() => handler.ScrollFromJsonAsync<Types.ProviderMetadata>(
            this.Uri,
            token,
            lazy: false);

    }

    /// <summary>
    /// A mutable intermediate form for a requested releases view
    /// </summary>
    public sealed class ReleasesUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the releases view
        /// </summary>
        public ReleasesUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// All released schemas.
        /// </summary>
        /// <remarks>
        /// This method assumes the URI was provided fully populated.
        /// </remarks>
        public Task<meta::IResultSet<Types.ReleaseMetadata>> GetAsync() => handler.ScrollFromJsonAsync<Types.ReleaseMetadata>(
            this.Uri,
            token,
            lazy: false);

        /// <summary>
        /// A utility method to update the query parameters for this URI
        /// </summary>
        /// <param name="artifact">The artifact name.</param>
        /// <remarks>
        /// This method mutates the URI and replaces all query parameters with the provided values.
        /// </remarks>
        public ReleasesUri AsQuery(
            string? artifact = null)
        {
            this.Query = string.Join("&", AsParameterStream(
                artifact));
            return this;
        }

        /// <summary>
        /// All released schemas.
        /// </summary>
        /// <remarks>
        /// This method provides lazy pagination.
        /// </remarks>
        public Task<meta::IResultSet<Types.ReleaseMetadata>> PageAsync(string? query = null)
        {
            this.Query = query;
            return handler.ScrollFromJsonAsync<Types.ReleaseMetadata>(
                this.Uri,
                token,
                lazy: true);
        }

        /// <summary>
        /// A utility method to simplify query string generation.
        /// </summary>
        private static IEnumerable<string> AsParameterStream(
            string? artifact = null)
        {
            if (artifact != null) yield return string.Format(
                "artifact={0}",
                Uri.EscapeDataString(artifact));
        }

    }

    /// <summary>
    /// A mutable intermediate form for a requested resources view
    /// </summary>
    public sealed class ResourcesUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the resources view
        /// </summary>
        public ResourcesUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// List all available resources.
        /// </summary>
        /// <remarks>
        /// This method assumes the URI was provided fully populated.
        /// </remarks>
        public Task<meta::IResultSet<Types.ResourceMetadata>> GetAsync() => handler.ScrollFromJsonAsync<Types.ResourceMetadata>(
            this.Uri,
            token,
            lazy: false);

    }

    /// <summary>
    /// A mutable intermediate form for a requested roles view
    /// </summary>
    public sealed class RolesUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the roles view
        /// </summary>
        public RolesUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// All roles.
        /// </summary>
        /// <remarks>
        /// This method assumes the URI was provided fully populated.
        /// <list type="table">
        /// <item>
        /// <term>Navigations</term>
        /// <description>The roles resource has 1 navigation</description>
        /// <list type="bullet">
        /// <item>From <c>/rows</c> to the role context</item>
        /// </list>
        /// </item>
        /// </list>
        /// </remarks>
        public Task<meta::IResultSet<Types.RoleMetadata>> GetAsync() => handler.ScrollFromJsonAsync<Types.RoleMetadata>(
            this.Uri,
            token,
            lazy: false);

        /// <summary>
        /// A utility method to update the query parameters for this URI
        /// </summary>
        /// <param name="policyGroup">The identifier for the parent policy group.</param>
        /// <remarks>
        /// This method mutates the URI and replaces all query parameters with the provided values.
        /// </remarks>
        public RolesUri AsQuery(
            string? policyGroup = null)
        {
            this.Query = string.Join("&", AsParameterStream(
                policyGroup));
            return this;
        }

        /// <summary>
        /// All roles.
        /// </summary>
        /// <remarks>
        /// This method provides lazy pagination.
        /// <list type="table">
        /// <item>
        /// <term>Navigations</term>
        /// <description>The roles resource has 1 navigation</description>
        /// <list type="bullet">
        /// <item>From <c>/rows</c> to the role context</item>
        /// </list>
        /// </item>
        /// </list>
        /// </remarks>
        public Task<meta::IResultSet<Types.RoleMetadata>> PageAsync(string? query = null)
        {
            this.Query = query;
            return handler.ScrollFromJsonAsync<Types.RoleMetadata>(
                this.Uri,
                token,
                lazy: true);
        }

        /// <summary>
        /// A utility method to simplify query string generation.
        /// </summary>
        private static IEnumerable<string> AsParameterStream(
            string? policyGroup = null)
        {
            if (policyGroup != null) yield return string.Format(
                "policy_group={0}",
                Uri.EscapeDataString(policyGroup));
        }

    }

    /// <summary>
    /// A mutable intermediate form for a requested services view
    /// </summary>
    public sealed class ServicesUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the services view
        /// </summary>
        public ServicesUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// A list of all available resource groups.
        /// </summary>
        /// <remarks>
        /// This method assumes the URI was provided fully populated.
        /// <list type="table">
        /// <item>
        /// <term>Navigations</term>
        /// <description>The services resource has 1 navigation</description>
        /// <list type="bullet">
        /// <item>From <c>/rows</c> to the service context</item>
        /// </list>
        /// </item>
        /// </list>
        /// </remarks>
        public Task<meta::IResultSet<Types.ServiceMetadata>> GetAsync() => handler.ScrollFromJsonAsync<Types.ServiceMetadata>(
            this.Uri,
            token,
            lazy: false);

    }

    /// <summary>
    /// A mutable intermediate form for a requested status view
    /// </summary>
    public sealed class StatusUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the status view
        /// </summary>
        public StatusUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Status information for the current account.
        /// </summary>
        /// <remarks>
        /// This method assumes the URI was provided fully populated.
        /// <list type="table">
        /// <item>
        /// <term>Navigations</term>
        /// <description>The status resource has 1 navigation</description>
        /// <list type="bullet">
        /// <item>From <c>/artifact</c> to the artifact context</item>
        /// </list>
        /// </item>
        /// </list>
        /// </remarks>
        public Task<Types.StatusSummary?> GetAsync() => handler.GetFromJsonAsync<Types.StatusSummary>(
            this.Uri,
            token);

    }

    /// <summary>
    /// A mutable intermediate form for a requested user assertion view
    /// </summary>
    public sealed class UserAssertionUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the user assertion view
        /// </summary>
        public UserAssertionUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// An asserted claim for a user.
        /// </summary>
        /// <remarks>
        /// This method assumes the URI was provided fully populated.
        /// </remarks>
        public Task<Types.AssertionDetails?> GetAsync() => handler.GetFromJsonAsync<Types.AssertionDetails>(
            this.Uri,
            token);

        /// <summary>
        /// A utility method to update the query parameters for this URI
        /// </summary>
        /// <param name="claimId">The claim identifier.</param>
        /// <remarks>
        /// This method mutates the URI and replaces all query parameters with the provided values.
        /// </remarks>
        public UserAssertionUri AsQuery(
            Guid claimId)
        {
            this.Query = string.Join("&", AsParameterStream(
                claimId));
            return this;
        }

        /// <summary>
        /// A utility method to simplify query string generation.
        /// </summary>
        private static IEnumerable<string> AsParameterStream(
            Guid claimId)
        {
            yield return string.Format(
                "claim_id={0}",
                claimId.ToString("n"));
        }

    }

    /// <summary>
    /// A mutable intermediate form for a requested assign identity action
    /// </summary>
    public sealed class AssignIdentityUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the assign identity action
        /// </summary>
        public AssignIdentityUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Assign an externally managed identity, optionally creating
        /// the profile if it does not exist.
        /// </summary>
        /// <param name="key">The claim key for the identity claim.</param>
        /// <param name="value">The asserted claim value.</param>
        /// <param name="scopes">All external scopes assigned to this user. Any existing roles belonging to a scope not in this collection will be disabled.</param>
        /// <param name="secret">The secret value for the identity claim.</param>
        /// <param name="tenant">The value of the associated tenant claim, if the key claim is associated with a multi-tenanted identity.</param>
        /// <param name="display">The display name for the account, if it should be overwritten.</param>
        public async Task<Types.UserProfile> PostAsync(
            string key,
            string value,
            List<string> scopes,
            byte[]? secret = null,
            string? tenant = null,
            string? display = null)
        {
            string d = JsonSerializer.Serialize(new Types.AssignIdentity(
                key,
                value,
                scopes)
                {
                    Secret = secret,
                    Tenant = tenant,
                    Display = display,
                },
                handler.Json);
            StringContent c = new StringContent(d, Encoding.UTF8, "application/json");
            var r = new HttpRequestMessage(HttpMethod.Post, this.Uri);
            r.Headers.Add("accept", "application/json");
            r.Content = c;
            return await handler.SendAsync(r, this.token)
                .GetFromJsonAsync<Types.UserProfile>(handler.Json, this.token) ?? throw new ServiceException(HttpStatusCode.OK, new ServiceError(
                    "evaluation.3000",
                    "An unexpected empty response was received from the server",
                    "The assign identity service returned an empty response, a 404 [NotFound] or a valid payload are expected."));
        }

    }

    /// <summary>
    /// A mutable intermediate form for a requested configure action
    /// </summary>
    public sealed class ConfigureUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the configure action
        /// </summary>
        public ConfigureUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Perform manual configuration of the account.
        /// </summary>
        /// <param name="permissions">All permissions associated with the issuer.</param>
        public async Task PostAsync(
            List<Types.PermissionDefinition> permissions)
        {
            string d = JsonSerializer.Serialize(new Types.Configure(
                permissions),
                handler.Json);
            StringContent c = new StringContent(d, Encoding.UTF8, "application/json");
            var r = new HttpRequestMessage(HttpMethod.Post, this.Uri);
            r.Headers.Add("accept", "application/json");
            r.Content = c;
            var o = await handler.SendAsync(r, this.token);
            if (!o.IsSuccessStatusCode) throw await ServiceException.CreateAsync(o, options: handler.Json);
        }

    }

    /// <summary>
    /// A mutable intermediate form for a requested create artifact action
    /// </summary>
    public sealed class CreateArtifactUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the create artifact action
        /// </summary>
        public CreateArtifactUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Create a new  artifact.
        /// </summary>
        /// <param name="name">The artifact name.</param>
        /// <param name="description">Full-text information about the artifact.</param>
        /// <remarks>
        /// <list type="table">
        /// <item>
        /// <term>Navigations</term>
        /// <description>The create artifact resource has 6 navigations</description>
        /// <list type="bullet">
        /// <item>From <c>/</c> to the artifact context</item>
        /// <item>From <c>/</c> to the create service action</item>
        /// <item>From <c>/</c> to the release action</item>
        /// <item>From <c>/</c> to the set artifact status action</item>
        /// <item>From <c>/</c> to the update artifact action</item>
        /// <item>From <c>/</c> to the services view</item>
        /// </list>
        /// </item>
        /// </list>
        /// </remarks>
        public async Task<Types.Artifact> PostAsync(
            string name,
            string description)
        {
            string d = JsonSerializer.Serialize(new Types.CreateArtifact(
                name,
                description),
                handler.Json);
            StringContent c = new StringContent(d, Encoding.UTF8, "application/json");
            var r = new HttpRequestMessage(HttpMethod.Post, this.Uri);
            r.Headers.Add("accept", "application/json");
            r.Content = c;
            return await handler.SendAsync(r, this.token)
                .GetFromJsonAsync<Types.Artifact>(handler.Json, this.token) ?? throw new ServiceException(HttpStatusCode.OK, new ServiceError(
                    "evaluation.3000",
                    "An unexpected empty response was received from the server",
                    "The create artifact service returned an empty response, a 404 [NotFound] or a valid payload are expected."));
        }

    }

    /// <summary>
    /// A mutable intermediate form for a requested create claim action
    /// </summary>
    public sealed class CreateClaimUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the create claim action
        /// </summary>
        public CreateClaimUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Create a new security claim.
        /// </summary>
        /// <param name="claim">The claim key.</param>
        /// <param name="description">A description of the semantic meaning of this claim.</param>
        /// <param name="unique">A flag indicating the claim is a unique key for an account.</param>
        /// <remarks>
        /// <list type="table">
        /// <item>
        /// <term>Navigations</term>
        /// <description>The create claim resource has 2 navigations</description>
        /// <list type="bullet">
        /// <item>From <c>/</c> to the claim context</item>
        /// <item>From <c>/</c> to the update claim action</item>
        /// </list>
        /// </item>
        /// </list>
        /// </remarks>
        public async Task<Types.Claim> PostAsync(
            string claim,
            string description,
            bool unique)
        {
            string d = JsonSerializer.Serialize(new Types.CreateClaim(
                claim,
                description,
                unique),
                handler.Json);
            StringContent c = new StringContent(d, Encoding.UTF8, "application/json");
            var r = new HttpRequestMessage(HttpMethod.Post, this.Uri);
            r.Headers.Add("accept", "application/json");
            r.Content = c;
            return await handler.SendAsync(r, this.token)
                .GetFromJsonAsync<Types.Claim>(handler.Json, this.token) ?? throw new ServiceException(HttpStatusCode.OK, new ServiceError(
                    "evaluation.3000",
                    "An unexpected empty response was received from the server",
                    "The create claim service returned an empty response, a 404 [NotFound] or a valid payload are expected."));
        }

    }

    /// <summary>
    /// A mutable intermediate form for a requested create provider action
    /// </summary>
    public sealed class CreateProviderUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the create provider action
        /// </summary>
        public CreateProviderUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Create a new authentication provider.
        /// </summary>
        /// <param name="name">The provider name.</param>
        /// <param name="description">Full-text information about the provider.</param>
        /// <param name="subject">The subject identifier claim key.</param>
        /// <param name="tenant">The tenant identifier claim key, if multi-tenanted.</param>
        /// <remarks>
        /// <list type="table">
        /// <item>
        /// <term>Navigations</term>
        /// <description>The create provider resource has 3 navigations</description>
        /// <list type="bullet">
        /// <item>From <c>/</c> to the provider context</item>
        /// <item>From <c>/</c> to the update provider action</item>
        /// <item>From <c>/</c> to the roles view</item>
        /// </list>
        /// </item>
        /// </list>
        /// </remarks>
        public async Task<Types.Provider> PostAsync(
            string name,
            string description,
            string? subject = null,
            string? tenant = null)
        {
            string d = JsonSerializer.Serialize(new Types.CreateProvider(
                name,
                description)
                {
                    Subject = subject,
                    Tenant = tenant,
                },
                handler.Json);
            StringContent c = new StringContent(d, Encoding.UTF8, "application/json");
            var r = new HttpRequestMessage(HttpMethod.Post, this.Uri);
            r.Headers.Add("accept", "application/json");
            r.Content = c;
            return await handler.SendAsync(r, this.token)
                .GetFromJsonAsync<Types.Provider>(handler.Json, this.token) ?? throw new ServiceException(HttpStatusCode.OK, new ServiceError(
                    "evaluation.3000",
                    "An unexpected empty response was received from the server",
                    "The create provider service returned an empty response, a 404 [NotFound] or a valid payload are expected."));
        }

    }

    /// <summary>
    /// A mutable intermediate form for a requested create resource action
    /// </summary>
    public sealed class CreateResourceUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the create resource action
        /// </summary>
        public CreateResourceUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Create a new resource.
        /// </summary>
        /// <param name="name">The name of this resource.</param>
        /// <param name="mask">The unix permissions associated with this resource (7:rwx, 6:rw-, 5:r-x, 4:r--, 3:-wx, 2:-w-, 1:--x).</param>
        /// <param name="restricted">The restriction applied to the mask for normal user accounts. System accountabilities have full access to the unmodified mask.</param>
        /// <param name="description">A human-readable description of the resource.</param>
        public async Task<Types.ResourceMetadata> PostAsync(
            string name,
            int mask,
            int restricted,
            string description)
        {
            string d = JsonSerializer.Serialize(new Types.CreateResource(
                name,
                mask,
                restricted,
                description),
                handler.Json);
            StringContent c = new StringContent(d, Encoding.UTF8, "application/json");
            var r = new HttpRequestMessage(HttpMethod.Post, this.Uri);
            r.Headers.Add("accept", "application/json");
            r.Content = c;
            return await handler.SendAsync(r, this.token)
                .GetFromJsonAsync<Types.ResourceMetadata>(handler.Json, this.token) ?? throw new ServiceException(HttpStatusCode.OK, new ServiceError(
                    "evaluation.3000",
                    "An unexpected empty response was received from the server",
                    "The create resource service returned an empty response, a 404 [NotFound] or a valid payload are expected."));
        }

    }

    /// <summary>
    /// A mutable intermediate form for a requested create role action
    /// </summary>
    public sealed class CreateRoleUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the create role action
        /// </summary>
        public CreateRoleUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Create a child policy group.
        /// </summary>
        /// <param name="name">The name of this policy group.</param>
        /// <param name="description">A description of the semantic meaning of this policy group.</param>
        /// <param name="mask">The maximum permissions that can be granted within this group.</param>
        /// <param name="scope">The name of the externally managed scope for the policy group, used by the authentication provider to expand access tokens.</param>
        /// <remarks>
        /// <list type="table">
        /// <item>
        /// <term>Navigations</term>
        /// <description>The create role resource has 7 navigations</description>
        /// <list type="bullet">
        /// <item>From <c>/</c> to the role context</item>
        /// <item>From <c>/</c> to the create claim action</item>
        /// <item>From <c>/</c> to the create role action</item>
        /// <item>From <c>/</c> to the set general access action</item>
        /// <item>From <c>/</c> to the set resource access action</item>
        /// <item>From <c>/</c> to the update role action</item>
        /// <item>From <c>/</c> to the available resources view</item>
        /// </list>
        /// </item>
        /// </list>
        /// </remarks>
        public async Task<Types.Role> PostAsync(
            string name,
            string description,
            int? mask = null,
            string? scope = null)
        {
            string d = JsonSerializer.Serialize(new Types.CreateRole(
                name,
                description)
                {
                    Mask = mask,
                    Scope = scope,
                },
                handler.Json);
            StringContent c = new StringContent(d, Encoding.UTF8, "application/json");
            var r = new HttpRequestMessage(HttpMethod.Post, this.Uri);
            r.Headers.Add("accept", "application/json");
            r.Content = c;
            return await handler.SendAsync(r, this.token)
                .GetFromJsonAsync<Types.Role>(handler.Json, this.token) ?? throw new ServiceException(HttpStatusCode.OK, new ServiceError(
                    "evaluation.3000",
                    "An unexpected empty response was received from the server",
                    "The create role service returned an empty response, a 404 [NotFound] or a valid payload are expected."));
        }

    }

    /// <summary>
    /// A mutable intermediate form for a requested create service action
    /// </summary>
    public sealed class CreateServiceUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the create service action
        /// </summary>
        public CreateServiceUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Create a new service.
        /// </summary>
        /// <param name="name">The resource group name.</param>
        /// <param name="claim">The permissions claim key.</param>
        /// <param name="description">Full-text information about the resource group.</param>
        /// <param name="provider">The provider name.</param>
        /// <param name="openapi">An OpenAPI specification.</param>
        /// <remarks>
        /// <list type="table">
        /// <item>
        /// <term>Navigations</term>
        /// <description>The create service resource has 8 navigations</description>
        /// <list type="bullet">
        /// <item>From <c>/</c> to the service context</item>
        /// <item>From <c>/</c> to the create resource action</item>
        /// <item>From <c>/</c> to the reset action</item>
        /// <item>From <c>/</c> to the set service status action</item>
        /// <item>From <c>/</c> to the set template action</item>
        /// <item>From <c>/</c> to the update resource action</item>
        /// <item>From <c>/</c> to the update service action</item>
        /// <item>From <c>/</c> to the resources view</item>
        /// </list>
        /// </item>
        /// </list>
        /// </remarks>
        public async Task<Types.Service> PostAsync(
            string name,
            string claim,
            string description,
            string provider,
            meta::IAttachment? openapi = null)
        {
            meta::AttachmentStream s;
            StreamContent f;
            var c = new MultipartFormDataContent();
            c.Add(new StringContent(name), "name");
            c.Add(new StringContent(claim), "claim");
            c.Add(new StringContent(description), "description");
            c.Add(new StringContent(provider), "provider");
            if (openapi != null)
            {
                s = await openapi.LoadAsync(this.token);
                f = new StreamContent(s);
                f.Headers.ContentType = MediaTypeHeaderValue.Parse(s.ContentType.ToString());
                c.Add(f, "openapi", s.FileName);
            }
            var r = new HttpRequestMessage(HttpMethod.Post, this.Uri);
            r.Headers.Add("accept", "application/json");
            r.Content = c;
            return await handler.SendAsync(r, this.token)
                .GetFromJsonAsync<Types.Service>(handler.Json, this.token) ?? throw new ServiceException(HttpStatusCode.OK, new ServiceError(
                    "evaluation.3000",
                    "An unexpected empty response was received from the server",
                    "The create service service returned an empty response, a 404 [NotFound] or a valid payload are expected."));
        }

    }

    /// <summary>
    /// A mutable intermediate form for a requested create tenant action
    /// </summary>
    public sealed class CreateTenantUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the create tenant action
        /// </summary>
        public CreateTenantUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Register a system tenant.
        /// </summary>
        /// <param name="name">The organization's display name.</param>
        /// <param name="claims">All claims associated with the account.</param>
        /// <param name="description">A description of the organization.</param>
        /// <param name="organizationId">The tenant organization account, if a multi-tenanted account.</param>
        /// <remarks>
        /// <list type="table">
        /// <item>
        /// <term>Navigations</term>
        /// <description>The create tenant resource has 8 navigations</description>
        /// <list type="bullet">
        /// <item>From <c>/</c> to the account context</item>
        /// <item>From <c>/</c> to the send invitation action</item>
        /// <item>From <c>/</c> to the set account status action</item>
        /// <item>From <c>/</c> to the set identity action</item>
        /// <item>From <c>/</c> to the update account action</item>
        /// <item>From <c>/</c> to the update identity action</item>
        /// <item>From <c>/</c> to the assertions view</item>
        /// <item>From <c>/</c> to the policies view</item>
        /// </list>
        /// </item>
        /// </list>
        /// </remarks>
        public async Task<Types.Account> PostAsync(
            string name,
            List<Types.ClaimValue> claims,
            string? description = null,
            Guid? organizationId = null)
        {
            string d = JsonSerializer.Serialize(new Types.CreateTenant(
                name,
                claims)
                {
                    Description = description,
                    OrganizationId = organizationId,
                },
                handler.Json);
            StringContent c = new StringContent(d, Encoding.UTF8, "application/json");
            var r = new HttpRequestMessage(HttpMethod.Post, this.Uri);
            r.Headers.Add("accept", "application/json");
            r.Content = c;
            return await handler.SendAsync(r, this.token)
                .GetFromJsonAsync<Types.Account>(handler.Json, this.token) ?? throw new ServiceException(HttpStatusCode.OK, new ServiceError(
                    "evaluation.3000",
                    "An unexpected empty response was received from the server",
                    "The create tenant service returned an empty response, a 404 [NotFound] or a valid payload are expected."));
        }

    }

    /// <summary>
    /// A mutable intermediate form for a requested create user action
    /// </summary>
    public sealed class CreateUserUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the create user action
        /// </summary>
        public CreateUserUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Create a new user account.
        /// </summary>
        /// <param name="display">The display name for the account.</param>
        /// <param name="claims">All claims associated with the account.</param>
        /// <param name="description">A description of the account.</param>
        /// <param name="organizationId">The tenant organization account, if a multi-tenanted account.</param>
        /// <remarks>
        /// <list type="table">
        /// <item>
        /// <term>Navigations</term>
        /// <description>The create user resource has 8 navigations</description>
        /// <list type="bullet">
        /// <item>From <c>/</c> to the account context</item>
        /// <item>From <c>/</c> to the send invitation action</item>
        /// <item>From <c>/</c> to the set account status action</item>
        /// <item>From <c>/</c> to the set identity action</item>
        /// <item>From <c>/</c> to the update account action</item>
        /// <item>From <c>/</c> to the update identity action</item>
        /// <item>From <c>/</c> to the assertions view</item>
        /// <item>From <c>/</c> to the policies view</item>
        /// </list>
        /// </item>
        /// </list>
        /// </remarks>
        public async Task<Types.Account> PostAsync(
            string display,
            List<Types.ClaimValue> claims,
            string? description = null,
            Guid? organizationId = null)
        {
            string d = JsonSerializer.Serialize(new Types.CreateUser(
                display,
                claims)
                {
                    Description = description,
                    OrganizationId = organizationId,
                },
                handler.Json);
            StringContent c = new StringContent(d, Encoding.UTF8, "application/json");
            var r = new HttpRequestMessage(HttpMethod.Post, this.Uri);
            r.Headers.Add("accept", "application/json");
            r.Content = c;
            return await handler.SendAsync(r, this.token)
                .GetFromJsonAsync<Types.Account>(handler.Json, this.token) ?? throw new ServiceException(HttpStatusCode.OK, new ServiceError(
                    "evaluation.3000",
                    "An unexpected empty response was received from the server",
                    "The create user service returned an empty response, a 404 [NotFound] or a valid payload are expected."));
        }

    }

    /// <summary>
    /// A mutable intermediate form for a requested override account action
    /// </summary>
    public sealed class OverrideAccountUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the override account action
        /// </summary>
        public OverrideAccountUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Override account metadata.
        /// </summary>
        /// <param name="accountId">The account identifier.</param>
        /// <param name="administratorId">The administrator account controlling the action.</param>
        /// <param name="display">The display name for the account.</param>
        /// <param name="claims">All claim values to be modified.</param>
        /// <param name="description">A description of the account.</param>
        public async Task PostAsync(
            Guid accountId,
            Guid administratorId,
            string display,
            List<Types.ClaimValue> claims,
            string? description = null)
        {
            string d = JsonSerializer.Serialize(new Types.OverrideAccount(
                accountId,
                administratorId,
                display,
                claims)
                {
                    Description = description,
                },
                handler.Json);
            StringContent c = new StringContent(d, Encoding.UTF8, "application/json");
            var r = new HttpRequestMessage(HttpMethod.Post, this.Uri);
            r.Headers.Add("accept", "application/json");
            r.Content = c;
            var o = await handler.SendAsync(r, this.token);
            if (!o.IsSuccessStatusCode) throw await ServiceException.CreateAsync(o, options: handler.Json);
        }

    }

    /// <summary>
    /// A mutable intermediate form for a requested override account access action
    /// </summary>
    public sealed class OverrideAccountAccessUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the override account access action
        /// </summary>
        public OverrideAccountAccessUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Use delegated authority to override the account access level
        /// for a policy.
        /// </summary>
        /// <param name="policyGroupId">The policy group name for this role.</param>
        /// <param name="accountId">The account identifier.</param>
        /// <param name="administratorId">The administrator account controlling the action.</param>
        /// <param name="claims">All claim values to be modified.</param>
        /// <param name="mask">The permissions granted to the user for this group.</param>
        public async Task PostAsync(
            Guid policyGroupId,
            Guid accountId,
            Guid administratorId,
            List<Types.ClaimValue> claims,
            int? mask = null)
        {
            string d = JsonSerializer.Serialize(new Types.OverrideAccountAccess(
                policyGroupId,
                accountId,
                administratorId,
                claims)
                {
                    Mask = mask,
                },
                handler.Json);
            StringContent c = new StringContent(d, Encoding.UTF8, "application/json");
            var r = new HttpRequestMessage(HttpMethod.Post, this.Uri);
            r.Headers.Add("accept", "application/json");
            r.Content = c;
            var o = await handler.SendAsync(r, this.token);
            if (!o.IsSuccessStatusCode) throw await ServiceException.CreateAsync(o, options: handler.Json);
        }

    }

    /// <summary>
    /// A mutable intermediate form for a requested refresh action
    /// </summary>
    public sealed class RefreshUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the refresh action
        /// </summary>
        public RefreshUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Refresh the user's session.
        /// </summary>
        /// <param name="session">The specific session.</param>
        /// <remarks>
        /// <list type="table">
        /// <item>
        /// <term>Navigations</term>
        /// <description>The refresh resource has 3 navigations</description>
        /// <list type="bullet">
        /// <item>From <c>/</c> to the account context</item>
        /// <item>From <c>/tenant</c> to the account context</item>
        /// <item>From <c>/assertions</c> to the user assertion view</item>
        /// </list>
        /// </item>
        /// </list>
        /// </remarks>
        public async Task<Types.UserSession> PostAsync(
            string? session = null)
        {
            string d = JsonSerializer.Serialize(new Types.Refresh()
                {
                    Session = session,
                },
                handler.Json);
            StringContent c = new StringContent(d, Encoding.UTF8, "application/json");
            var r = new HttpRequestMessage(HttpMethod.Post, this.Uri);
            r.Headers.Add("accept", "application/json");
            r.Content = c;
            return await handler.SendAsync(r, this.token)
                .GetFromJsonAsync<Types.UserSession>(handler.Json, this.token) ?? throw new ServiceException(HttpStatusCode.OK, new ServiceError(
                    "evaluation.3000",
                    "An unexpected empty response was received from the server",
                    "The refresh service returned an empty response, a 404 [NotFound] or a valid payload are expected."));
        }

    }

    /// <summary>
    /// A mutable intermediate form for a requested refresh profile action
    /// </summary>
    public sealed class RefreshProfileUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the refresh profile action
        /// </summary>
        public RefreshProfileUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Refresh all profile assertions.
        /// </summary>
        /// <param name="accountId">The account identifier.</param>
        public async Task<Types.UserProfile> PostAsync(
            Guid accountId)
        {
            string d = JsonSerializer.Serialize(new Types.RefreshProfile(
                accountId),
                handler.Json);
            StringContent c = new StringContent(d, Encoding.UTF8, "application/json");
            var r = new HttpRequestMessage(HttpMethod.Post, this.Uri);
            r.Headers.Add("accept", "application/json");
            r.Content = c;
            return await handler.SendAsync(r, this.token)
                .GetFromJsonAsync<Types.UserProfile>(handler.Json, this.token) ?? throw new ServiceException(HttpStatusCode.OK, new ServiceError(
                    "evaluation.3000",
                    "An unexpected empty response was received from the server",
                    "The refresh profile service returned an empty response, a 404 [NotFound] or a valid payload are expected."));
        }

    }

    /// <summary>
    /// A mutable intermediate form for a requested register action
    /// </summary>
    public sealed class RegisterUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the register action
        /// </summary>
        public RegisterUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Register an artifact from an application manifest.
        /// </summary>
        /// <param name="manifest">The application manifest.</param>
        /// <remarks>
        /// <list type="table">
        /// <item>
        /// <term>Navigations</term>
        /// <description>The register resource has 6 navigations</description>
        /// <list type="bullet">
        /// <item>From <c>/</c> to the artifact context</item>
        /// <item>From <c>/</c> to the create service action</item>
        /// <item>From <c>/</c> to the release action</item>
        /// <item>From <c>/</c> to the set artifact status action</item>
        /// <item>From <c>/</c> to the update artifact action</item>
        /// <item>From <c>/</c> to the services view</item>
        /// </list>
        /// </item>
        /// </list>
        /// </remarks>
        public async Task<Types.Artifact> PostAsync(
            meta::IAttachment manifest)
        {
            meta::AttachmentStream s;
            StreamContent f;
            var c = new MultipartFormDataContent();
            s = await manifest.LoadAsync(this.token);
            f = new StreamContent(s);
            f.Headers.ContentType = MediaTypeHeaderValue.Parse(s.ContentType.ToString());
            c.Add(f, "manifest", s.FileName);
            var r = new HttpRequestMessage(HttpMethod.Post, this.Uri);
            r.Headers.Add("accept", "application/json");
            r.Content = c;
            return await handler.SendAsync(r, this.token)
                .GetFromJsonAsync<Types.Artifact>(handler.Json, this.token) ?? throw new ServiceException(HttpStatusCode.OK, new ServiceError(
                    "evaluation.3000",
                    "An unexpected empty response was received from the server",
                    "The register service returned an empty response, a 404 [NotFound] or a valid payload are expected."));
        }

    }

    /// <summary>
    /// A mutable intermediate form for a requested register account action
    /// </summary>
    public sealed class RegisterAccountUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the register account action
        /// </summary>
        public RegisterAccountUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Register a new account on behalf of an adminstrator.
        /// </summary>
        /// <param name="administratorId">The administrator account controlling the action.</param>
        /// <param name="display">The display name for the account.</param>
        /// <param name="multitenant">A flag indicating the account should be created as a new organization with a shadow profile belonging to the administrator that triggered this action.</param>
        /// <param name="claims">All claim values to be modified.</param>
        /// <param name="description">A description of the account.</param>
        /// <remarks>
        /// <list type="table">
        /// <item>
        /// <term>Navigations</term>
        /// <description>The register account resource has 8 navigations</description>
        /// <list type="bullet">
        /// <item>From <c>/</c> to the account context</item>
        /// <item>From <c>/</c> to the send invitation action</item>
        /// <item>From <c>/</c> to the set account status action</item>
        /// <item>From <c>/</c> to the set identity action</item>
        /// <item>From <c>/</c> to the update account action</item>
        /// <item>From <c>/</c> to the update identity action</item>
        /// <item>From <c>/</c> to the assertions view</item>
        /// <item>From <c>/</c> to the policies view</item>
        /// </list>
        /// </item>
        /// </list>
        /// </remarks>
        public async Task<Types.Account> PostAsync(
            Guid administratorId,
            string display,
            bool multitenant,
            List<Types.ClaimValue> claims,
            string? description = null)
        {
            string d = JsonSerializer.Serialize(new Types.RegisterAccount(
                administratorId,
                display,
                multitenant,
                claims)
                {
                    Description = description,
                },
                handler.Json);
            StringContent c = new StringContent(d, Encoding.UTF8, "application/json");
            var r = new HttpRequestMessage(HttpMethod.Post, this.Uri);
            r.Headers.Add("accept", "application/json");
            r.Content = c;
            return await handler.SendAsync(r, this.token)
                .GetFromJsonAsync<Types.Account>(handler.Json, this.token) ?? throw new ServiceException(HttpStatusCode.OK, new ServiceError(
                    "evaluation.3000",
                    "An unexpected empty response was received from the server",
                    "The register account service returned an empty response, a 404 [NotFound] or a valid payload are expected."));
        }

    }

    /// <summary>
    /// A mutable intermediate form for a requested register app action
    /// </summary>
    public sealed class RegisterAppUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the register app action
        /// </summary>
        public RegisterAppUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Register a new application identity.
        /// </summary>
        /// <param name="display">The display name for the account.</param>
        /// <param name="description">A description of the account.</param>
        /// <remarks>
        /// <list type="table">
        /// <item>
        /// <term>Navigations</term>
        /// <description>The register app resource has 1 navigation</description>
        /// <list type="bullet">
        /// <item>From <c>/</c> to the account context</item>
        /// </list>
        /// </item>
        /// </list>
        /// </remarks>
        public async Task<Types.AppRegistration> PostAsync(
            string display,
            string? description = null)
        {
            string d = JsonSerializer.Serialize(new Types.RegisterApp(
                display)
                {
                    Description = description,
                },
                handler.Json);
            StringContent c = new StringContent(d, Encoding.UTF8, "application/json");
            var r = new HttpRequestMessage(HttpMethod.Post, this.Uri);
            r.Headers.Add("accept", "application/json");
            r.Content = c;
            return await handler.SendAsync(r, this.token)
                .GetFromJsonAsync<Types.AppRegistration>(handler.Json, this.token) ?? throw new ServiceException(HttpStatusCode.OK, new ServiceError(
                    "evaluation.3000",
                    "An unexpected empty response was received from the server",
                    "The register app service returned an empty response, a 404 [NotFound] or a valid payload are expected."));
        }

    }

    /// <summary>
    /// A mutable intermediate form for a requested release action
    /// </summary>
    public sealed class ReleaseUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the release action
        /// </summary>
        public ReleaseUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Release the current state of the artifact as a new version.
        /// </summary>
        public async Task PostAsync()
        {
            StringContent c = new StringContent(string.Empty, Encoding.UTF8, "application/json");
            var r = new HttpRequestMessage(HttpMethod.Post, this.Uri);
            r.Headers.Add("accept", "application/json");
            r.Content = c;
            var o = await handler.SendAsync(r, this.token);
            if (!o.IsSuccessStatusCode) throw await ServiceException.CreateAsync(o, options: handler.Json);
        }

    }

    /// <summary>
    /// A mutable intermediate form for a requested reset action
    /// </summary>
    public sealed class ResetUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the reset action
        /// </summary>
        public ResetUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Reset all pending changes for a service.
        /// </summary>
        public async Task PostAsync()
        {
            StringContent c = new StringContent(string.Empty, Encoding.UTF8, "application/json");
            var r = new HttpRequestMessage(HttpMethod.Post, this.Uri);
            r.Headers.Add("accept", "application/json");
            r.Content = c;
            var o = await handler.SendAsync(r, this.token);
            if (!o.IsSuccessStatusCode) throw await ServiceException.CreateAsync(o, options: handler.Json);
        }

    }

    /// <summary>
    /// A mutable intermediate form for a requested send invitation action
    /// </summary>
    public sealed class SendInvitationUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the send invitation action
        /// </summary>
        public SendInvitationUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Send an invitation for a user to take ownership of an
        /// account.
        /// </summary>
        /// <param name="email">The user's email address.</param>
        public async Task PostAsync(
            string email)
        {
            string d = JsonSerializer.Serialize(new Types.SendInvitation(
                email),
                handler.Json);
            StringContent c = new StringContent(d, Encoding.UTF8, "application/json");
            var r = new HttpRequestMessage(HttpMethod.Post, this.Uri);
            r.Headers.Add("accept", "application/json");
            r.Content = c;
            var o = await handler.SendAsync(r, this.token);
            if (!o.IsSuccessStatusCode) throw await ServiceException.CreateAsync(o, options: handler.Json);
        }

    }

    /// <summary>
    /// A mutable intermediate form for a requested set account access action
    /// </summary>
    public sealed class SetAccountAccessUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the set account access action
        /// </summary>
        public SetAccountAccessUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Configure an access policy for an account.
        /// </summary>
        /// <param name="claims">All claim values to be modified.</param>
        /// <param name="mask">The permissions granted to the user for this group.</param>
        public async Task PostAsync(
            List<Types.ClaimValue> claims,
            int? mask = null)
        {
            string d = JsonSerializer.Serialize(new Types.SetAccountAccess(
                claims)
                {
                    Mask = mask,
                },
                handler.Json);
            StringContent c = new StringContent(d, Encoding.UTF8, "application/json");
            var r = new HttpRequestMessage(HttpMethod.Post, this.Uri);
            r.Headers.Add("accept", "application/json");
            r.Content = c;
            var o = await handler.SendAsync(r, this.token);
            if (!o.IsSuccessStatusCode) throw await ServiceException.CreateAsync(o, options: handler.Json);
        }

    }

    /// <summary>
    /// A mutable intermediate form for a requested set account status action
    /// </summary>
    public sealed class SetAccountStatusUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the set account status action
        /// </summary>
        public SetAccountStatusUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Update the account status.
        /// </summary>
        /// <param name="archived">A flag indicating the account should be archived.</param>
        public async Task PostAsync(
            bool archived)
        {
            string d = JsonSerializer.Serialize(new Types.SetAccountStatus(
                archived),
                handler.Json);
            StringContent c = new StringContent(d, Encoding.UTF8, "application/json");
            var r = new HttpRequestMessage(HttpMethod.Post, this.Uri);
            r.Headers.Add("accept", "application/json");
            r.Content = c;
            var o = await handler.SendAsync(r, this.token);
            if (!o.IsSuccessStatusCode) throw await ServiceException.CreateAsync(o, options: handler.Json);
        }

    }

    /// <summary>
    /// A mutable intermediate form for a requested set artifact status action
    /// </summary>
    public sealed class SetArtifactStatusUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the set artifact status action
        /// </summary>
        public SetArtifactStatusUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// A flag indicating the artifact should be archived.
        /// </summary>
        /// <param name="archived">A flag indicating the resource group should currently be archived.</param>
        public async Task PostAsync(
            bool archived)
        {
            string d = JsonSerializer.Serialize(new Types.SetArtifactStatus(
                archived),
                handler.Json);
            StringContent c = new StringContent(d, Encoding.UTF8, "application/json");
            var r = new HttpRequestMessage(HttpMethod.Post, this.Uri);
            r.Headers.Add("accept", "application/json");
            r.Content = c;
            var o = await handler.SendAsync(r, this.token);
            if (!o.IsSuccessStatusCode) throw await ServiceException.CreateAsync(o, options: handler.Json);
        }

    }

    /// <summary>
    /// A mutable intermediate form for a requested set general access action
    /// </summary>
    public sealed class SetGeneralAccessUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the set general access action
        /// </summary>
        public SetGeneralAccessUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Mark a policy group with general access status.
        /// </summary>
        /// <param name="enabled">A flag indicating the policy group should be made general access.</param>
        public async Task PostAsync(
            bool enabled)
        {
            string d = JsonSerializer.Serialize(new Types.SetGeneralAccess(
                enabled),
                handler.Json);
            StringContent c = new StringContent(d, Encoding.UTF8, "application/json");
            var r = new HttpRequestMessage(HttpMethod.Post, this.Uri);
            r.Headers.Add("accept", "application/json");
            r.Content = c;
            var o = await handler.SendAsync(r, this.token);
            if (!o.IsSuccessStatusCode) throw await ServiceException.CreateAsync(o, options: handler.Json);
        }

    }

    /// <summary>
    /// A mutable intermediate form for a requested set identity action
    /// </summary>
    public sealed class SetIdentityUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the set identity action
        /// </summary>
        public SetIdentityUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Set the identity for a account within a provider.
        /// </summary>
        /// <param name="key">The claim key for the identity claim.</param>
        /// <param name="value">The identifier value, if the account should be granted access.</param>
        public async Task PostAsync(
            string key,
            string? value = null)
        {
            string d = JsonSerializer.Serialize(new Types.SetIdentity(
                key)
                {
                    Value = value,
                },
                handler.Json);
            StringContent c = new StringContent(d, Encoding.UTF8, "application/json");
            var r = new HttpRequestMessage(HttpMethod.Post, this.Uri);
            r.Headers.Add("accept", "application/json");
            r.Content = c;
            var o = await handler.SendAsync(r, this.token);
            if (!o.IsSuccessStatusCode) throw await ServiceException.CreateAsync(o, options: handler.Json);
        }

    }

    /// <summary>
    /// A mutable intermediate form for a requested set release status action
    /// </summary>
    public sealed class SetReleaseStatusUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the set release status action
        /// </summary>
        public SetReleaseStatusUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Activate a specific artifact version.
        /// </summary>
        /// <param name="artifact">The name of this artifact.</param>
        /// <param name="version">The artifact schema version.</param>
        /// <param name="published">A flag indicating whether the artifact should be the currently published version of the schema.</param>
        /// <param name="description">Optionally override the description.</param>
        public async Task PostAsync(
            string artifact,
            int version,
            bool published,
            string? description = null)
        {
            string d = JsonSerializer.Serialize(new Types.SetReleaseStatus(
                artifact,
                version,
                published)
                {
                    Description = description,
                },
                handler.Json);
            StringContent c = new StringContent(d, Encoding.UTF8, "application/json");
            var r = new HttpRequestMessage(HttpMethod.Post, this.Uri);
            r.Headers.Add("accept", "application/json");
            r.Content = c;
            var o = await handler.SendAsync(r, this.token);
            if (!o.IsSuccessStatusCode) throw await ServiceException.CreateAsync(o, options: handler.Json);
        }

    }

    /// <summary>
    /// A mutable intermediate form for a requested set resource access action
    /// </summary>
    public sealed class SetResourceAccessUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the set resource access action
        /// </summary>
        public SetResourceAccessUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Update the active status for a resource.
        /// </summary>
        /// <param name="enabled">The resource identifiers to enable.</param>
        /// <param name="disabled">The resource identifiers to disable.</param>
        public async Task PostAsync(
            List<Guid> enabled,
            List<Guid> disabled)
        {
            string d = JsonSerializer.Serialize(new Types.SetResourceAccess(
                enabled,
                disabled),
                handler.Json);
            StringContent c = new StringContent(d, Encoding.UTF8, "application/json");
            var r = new HttpRequestMessage(HttpMethod.Post, this.Uri);
            r.Headers.Add("accept", "application/json");
            r.Content = c;
            var o = await handler.SendAsync(r, this.token);
            if (!o.IsSuccessStatusCode) throw await ServiceException.CreateAsync(o, options: handler.Json);
        }

    }

    /// <summary>
    /// A mutable intermediate form for a requested set service status action
    /// </summary>
    public sealed class SetServiceStatusUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the set service status action
        /// </summary>
        public SetServiceStatusUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Update the service status.
        /// </summary>
        /// <param name="archived">A flag indicating the service should be archived.</param>
        public async Task PostAsync(
            bool archived)
        {
            string d = JsonSerializer.Serialize(new Types.SetServiceStatus(
                archived),
                handler.Json);
            StringContent c = new StringContent(d, Encoding.UTF8, "application/json");
            var r = new HttpRequestMessage(HttpMethod.Post, this.Uri);
            r.Headers.Add("accept", "application/json");
            r.Content = c;
            var o = await handler.SendAsync(r, this.token);
            if (!o.IsSuccessStatusCode) throw await ServiceException.CreateAsync(o, options: handler.Json);
        }

    }

    /// <summary>
    /// A mutable intermediate form for a requested set template action
    /// </summary>
    public sealed class SetTemplateUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the set template action
        /// </summary>
        public SetTemplateUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Upset the template for a specific event.
        /// </summary>
        /// <param name="name">The name of this event.</param>
        /// <param name="mimetype">The mime type targeted by this template.</param>
        /// <param name="description">A human-readable description of the event.</param>
        /// <param name="literal">The template literal for creating audit event descriptions.</param>
        /// <param name="entities">All entity tokens associated with this audit event.</param>
        public async Task PostAsync(
            string name,
            string mimetype,
            string description,
            string literal,
            List<Types.EntityToken> entities)
        {
            string d = JsonSerializer.Serialize(new Types.SetTemplate(
                name,
                mimetype,
                description,
                literal,
                entities),
                handler.Json);
            StringContent c = new StringContent(d, Encoding.UTF8, "application/json");
            var r = new HttpRequestMessage(HttpMethod.Post, this.Uri);
            r.Headers.Add("accept", "application/json");
            r.Content = c;
            var o = await handler.SendAsync(r, this.token);
            if (!o.IsSuccessStatusCode) throw await ServiceException.CreateAsync(o, options: handler.Json);
        }

    }

    /// <summary>
    /// A mutable intermediate form for a requested setup action
    /// </summary>
    public sealed class SetupUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the setup action
        /// </summary>
        public SetupUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Perform initial setup actions for an account.
        /// </summary>
        /// <param name="name">The application name.</param>
        /// <param name="version">The schema version.</param>
        /// <param name="claim">The permissions claims.</param>
        /// <param name="description">A description of the application.</param>
        /// <param name="idp">The identity provider name.</param>
        /// <param name="subject">The user id claim, if relevant for the identity provider.</param>
        /// <param name="tenant">The tenant id claim, if relevant for the identity provider.</param>
        /// <param name="openapi">The OpenAPI document for the application.</param>
        public async Task PostAsync(
            string name,
            int version,
            string claim,
            string description,
            string idp,
            string? subject = null,
            string? tenant = null,
            meta::IAttachment? openapi = null)
        {
            meta::AttachmentStream s;
            StreamContent f;
            var c = new MultipartFormDataContent();
            c.Add(new StringContent(name), "name");
            c.Add(new StringContent(version.ToString()), "version");
            c.Add(new StringContent(claim), "claim");
            c.Add(new StringContent(description), "description");
            c.Add(new StringContent(idp), "idp");
            if (subject != null) c.Add(
                    new StringContent(subject),
                    "subject");
            if (tenant != null) c.Add(
                    new StringContent(tenant),
                    "tenant");
            if (openapi != null)
            {
                s = await openapi.LoadAsync(this.token);
                f = new StreamContent(s);
                f.Headers.ContentType = MediaTypeHeaderValue.Parse(s.ContentType.ToString());
                c.Add(f, "openapi", s.FileName);
            }
            var r = new HttpRequestMessage(HttpMethod.Post, this.Uri);
            r.Headers.Add("accept", "application/json");
            r.Content = c;
            var o = await handler.SendAsync(r, this.token);
            if (!o.IsSuccessStatusCode) throw await ServiceException.CreateAsync(o, options: handler.Json);
        }

    }

    /// <summary>
    /// A mutable intermediate form for a requested update account action
    /// </summary>
    public sealed class UpdateAccountUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the update account action
        /// </summary>
        public UpdateAccountUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Update an account's metadata.
        /// </summary>
        /// <param name="display">The display name for the account.</param>
        /// <param name="description">A description of the account.</param>
        public async Task PostAsync(
            string display,
            string? description = null)
        {
            string d = JsonSerializer.Serialize(new Types.UpdateAccount(
                display)
                {
                    Description = description,
                },
                handler.Json);
            StringContent c = new StringContent(d, Encoding.UTF8, "application/json");
            var r = new HttpRequestMessage(HttpMethod.Post, this.Uri);
            r.Headers.Add("accept", "application/json");
            r.Content = c;
            var o = await handler.SendAsync(r, this.token);
            if (!o.IsSuccessStatusCode) throw await ServiceException.CreateAsync(o, options: handler.Json);
        }

    }

    /// <summary>
    /// A mutable intermediate form for a requested update artifact action
    /// </summary>
    public sealed class UpdateArtifactUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the update artifact action
        /// </summary>
        public UpdateArtifactUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Update artifact metadata.
        /// </summary>
        /// <param name="description">Full-text information about the artifact.</param>
        public async Task PostAsync(
            string description)
        {
            string d = JsonSerializer.Serialize(new Types.UpdateArtifact(
                description),
                handler.Json);
            StringContent c = new StringContent(d, Encoding.UTF8, "application/json");
            var r = new HttpRequestMessage(HttpMethod.Post, this.Uri);
            r.Headers.Add("accept", "application/json");
            r.Content = c;
            var o = await handler.SendAsync(r, this.token);
            if (!o.IsSuccessStatusCode) throw await ServiceException.CreateAsync(o, options: handler.Json);
        }

    }

    /// <summary>
    /// A mutable intermediate form for a requested update claim action
    /// </summary>
    public sealed class UpdateClaimUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the update claim action
        /// </summary>
        public UpdateClaimUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Update a security claim.
        /// </summary>
        /// <param name="description">A description of the semantic meaning of this claim.</param>
        public async Task PostAsync(
            string description)
        {
            string d = JsonSerializer.Serialize(new Types.UpdateClaim(
                description),
                handler.Json);
            StringContent c = new StringContent(d, Encoding.UTF8, "application/json");
            var r = new HttpRequestMessage(HttpMethod.Post, this.Uri);
            r.Headers.Add("accept", "application/json");
            r.Content = c;
            var o = await handler.SendAsync(r, this.token);
            if (!o.IsSuccessStatusCode) throw await ServiceException.CreateAsync(o, options: handler.Json);
        }

    }

    /// <summary>
    /// A mutable intermediate form for a requested update identity action
    /// </summary>
    public sealed class UpdateIdentityUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the update identity action
        /// </summary>
        public UpdateIdentityUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Update an identity claim with a secret value.
        /// </summary>
        /// <param name="key">The claim key for the identity claim.</param>
        /// <param name="secret">The secret value for the identity claim.</param>
        public async Task PostAsync(
            string key,
            byte[]? secret = null)
        {
            string d = JsonSerializer.Serialize(new Types.UpdateIdentity(
                key)
                {
                    Secret = secret,
                },
                handler.Json);
            StringContent c = new StringContent(d, Encoding.UTF8, "application/json");
            var r = new HttpRequestMessage(HttpMethod.Post, this.Uri);
            r.Headers.Add("accept", "application/json");
            r.Content = c;
            var o = await handler.SendAsync(r, this.token);
            if (!o.IsSuccessStatusCode) throw await ServiceException.CreateAsync(o, options: handler.Json);
        }

    }

    /// <summary>
    /// A mutable intermediate form for a requested update organization action
    /// </summary>
    public sealed class UpdateOrganizationUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the update organization action
        /// </summary>
        public UpdateOrganizationUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Update organization details.
        /// </summary>
        /// <param name="name">The organization name.</param>
        /// <param name="description">A description of the organization.</param>
        public async Task PostAsync(
            string name,
            string? description = null)
        {
            string d = JsonSerializer.Serialize(new Types.UpdateOrganization(
                name)
                {
                    Description = description,
                },
                handler.Json);
            StringContent c = new StringContent(d, Encoding.UTF8, "application/json");
            var r = new HttpRequestMessage(HttpMethod.Post, this.Uri);
            r.Headers.Add("accept", "application/json");
            r.Content = c;
            var o = await handler.SendAsync(r, this.token);
            if (!o.IsSuccessStatusCode) throw await ServiceException.CreateAsync(o, options: handler.Json);
        }

    }

    /// <summary>
    /// A mutable intermediate form for a requested update provider action
    /// </summary>
    public sealed class UpdateProviderUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the update provider action
        /// </summary>
        public UpdateProviderUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Update provider metadata.
        /// </summary>
        /// <param name="description">Full-text information about the provider.</param>
        public async Task PostAsync(
            string description)
        {
            string d = JsonSerializer.Serialize(new Types.UpdateProvider(
                description),
                handler.Json);
            StringContent c = new StringContent(d, Encoding.UTF8, "application/json");
            var r = new HttpRequestMessage(HttpMethod.Post, this.Uri);
            r.Headers.Add("accept", "application/json");
            r.Content = c;
            var o = await handler.SendAsync(r, this.token);
            if (!o.IsSuccessStatusCode) throw await ServiceException.CreateAsync(o, options: handler.Json);
        }

    }

    /// <summary>
    /// A mutable intermediate form for a requested update resource action
    /// </summary>
    public sealed class UpdateResourceUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the update resource action
        /// </summary>
        public UpdateResourceUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Update a resource.
        /// </summary>
        /// <param name="resourceId">The resource identifier.</param>
        /// <param name="name">The name of this resource.</param>
        /// <param name="mask">The unix permissions associated with this resource (7:rwx, 6:rw-, 5:r-x, 4:r--, 3:-wx, 2:-w-, 1:--x).</param>
        /// <param name="restricted">The restriction applied to the mask for normal user accounts. System accountabilities have full access to the unmodified mask.</param>
        /// <param name="description">A human-readable description of the resource.</param>
        /// <param name="archived">A flag indicating the resource should currently be archived.</param>
        public async Task PostAsync(
            Guid resourceId,
            string name,
            int mask,
            int restricted,
            string description,
            bool archived)
        {
            string d = JsonSerializer.Serialize(new Types.UpdateResource(
                resourceId,
                name,
                mask,
                restricted,
                description,
                archived),
                handler.Json);
            StringContent c = new StringContent(d, Encoding.UTF8, "application/json");
            var r = new HttpRequestMessage(HttpMethod.Post, this.Uri);
            r.Headers.Add("accept", "application/json");
            r.Content = c;
            var o = await handler.SendAsync(r, this.token);
            if (!o.IsSuccessStatusCode) throw await ServiceException.CreateAsync(o, options: handler.Json);
        }

    }

    /// <summary>
    /// A mutable intermediate form for a requested update role action
    /// </summary>
    public sealed class UpdateRoleUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the update role action
        /// </summary>
        public UpdateRoleUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Update an existing policy group.
        /// </summary>
        /// <param name="description">Full-text information about the policy group.</param>
        public async Task PostAsync(
            string description)
        {
            string d = JsonSerializer.Serialize(new Types.UpdateRole(
                description),
                handler.Json);
            StringContent c = new StringContent(d, Encoding.UTF8, "application/json");
            var r = new HttpRequestMessage(HttpMethod.Post, this.Uri);
            r.Headers.Add("accept", "application/json");
            r.Content = c;
            var o = await handler.SendAsync(r, this.token);
            if (!o.IsSuccessStatusCode) throw await ServiceException.CreateAsync(o, options: handler.Json);
        }

    }

    /// <summary>
    /// A mutable intermediate form for a requested update service action
    /// </summary>
    public sealed class UpdateServiceUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the update service action
        /// </summary>
        public UpdateServiceUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Update an existing resource group.
        /// </summary>
        /// <param name="description">Full-text information about the resource group.</param>
        public async Task PostAsync(
            string description)
        {
            string d = JsonSerializer.Serialize(new Types.UpdateService(
                description),
                handler.Json);
            StringContent c = new StringContent(d, Encoding.UTF8, "application/json");
            var r = new HttpRequestMessage(HttpMethod.Post, this.Uri);
            r.Headers.Add("accept", "application/json");
            r.Content = c;
            var o = await handler.SendAsync(r, this.token);
            if (!o.IsSuccessStatusCode) throw await ServiceException.CreateAsync(o, options: handler.Json);
        }

    }

    /// <summary>
    /// A mutable intermediate form for a requested log action
    /// </summary>
    public sealed class LogUri : UriBuilder
    {

        private readonly IHttpHandler handler;

        private readonly CancellationToken token;

        /// <summary>
        /// Create a new UriBuilder for the log action
        /// </summary>
        public LogUri(
            Uri uri,
            IHttpHandler handler,
            CancellationToken token) : base(uri)
        {
            this.handler = handler;
            this.token = token;
        }

        /// <summary>
        /// Log a new audit event.
        /// </summary>
        /// <param name="resourceId">The resource that effected the action.</param>
        /// <param name="version">The schema version at the time the action was effected.</param>
        /// <param name="name">The audit event name.</param>
        /// <param name="payload">The raw contents of the audit event.</param>
        /// <param name="accountabilityId">The accountability that nominally undertook the action, under the auspices of the actor that submitted this event.</param>
        public async Task PostAsync(
            Guid resourceId,
            int version,
            string name,
            meta::IAttachment payload,
            Guid? accountabilityId = null)
        {
            meta::AttachmentStream s;
            StreamContent f;
            var c = new MultipartFormDataContent();
            c.Add(new StringContent(resourceId.ToString("n")), "resource_id");
            c.Add(new StringContent(version.ToString()), "version");
            c.Add(new StringContent(name), "name");
            s = await payload.LoadAsync(this.token);
            f = new StreamContent(s);
            f.Headers.ContentType = MediaTypeHeaderValue.Parse(s.ContentType.ToString());
            c.Add(f, "payload", s.FileName);
            if (accountabilityId != null) c.Add(
                    new StringContent(accountabilityId.Value.ToString("n")),
                    "accountability_id");
            var r = new HttpRequestMessage(HttpMethod.Post, this.Uri);
            r.Headers.Add("accept", "application/json");
            r.Content = c;
            var o = await handler.SendAsync(r, this.token);
            if (!o.IsSuccessStatusCode) throw await ServiceException.CreateAsync(o, options: handler.Json);
        }

    }

}