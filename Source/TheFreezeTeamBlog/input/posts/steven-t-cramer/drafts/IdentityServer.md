Title: IdentityServer 4 2.0 userInfo "No 'Access-Control-Allow-Origin' header is present on the requested resource.
Tags: 
  - C# 
  - Blazor 
  - dotnet 
  - Blazor-State
Author: Steven T. Cramer
Excerpt: ReduxDevTools off by default. 
Published: 03/12/2099
---
# The scenario.
The site correctly redirects to login and upon login success I am redirected back to the client callback page which updates state and then attempts to load the user via `userManager.getUser()`
which causes the following:

## The error message:
Failed to load https://dev-auth.mydomain.com/connect/userinfo: Response to preflight request doesn't pass access control check: No 'Access-Control-Allow-Origin' header is present on the requested resource. Origin 'https://dev-order.mydomain.com' is therefore not allowed access. The response had HTTP status code 404.

## The Logs

From the Seq Serilog we see the following other calls succeed.

Request starting HTTP/1.1 GET http://dev-mysite.com/.well-known/openid-configuration/jwks
Request starting HTTP/1.1 GET http://dev-mysite.com/.well-known/openid-configuration
Request starting HTTP/1.1 GET http://dev-mysite.com/connect/authorize/callback?client_id=...
Request starting HTTP/1.1 POST http://dev-mysite.com/Account/Login application/x-www-form-urlencoded 643
Request starting HTTP/1.1 GET http://dev-mysite.com/account/login?returnUrl=...
Request starting HTTP/1.1 GET http://dev-mysite.com/connect/authorize?client_id=...
Request starting HTTP/1.1 GET http://dev-mysite.com/.well-known/openid-configuration

# The Code

The projects are:

* api.mysite.com
* auth.mysite.com
* spa.mysite.com

All projects are .net core 2.0.  The spa project serves a react application that uses the oidc-client package.

## auth.mysite.com Startup.cs
The CofigureServices for Identity server portion of Startup is:
```csharp

    public void ConfigureServices(IServiceCollection aServiceCollection)
    {
      // CorsPolicy.Any.Apply(aServiceCollection);
      aServiceCollection.AddMvc();
      //aServiceCollection.AddMvcCore()
      //  .AddJsonFormatters()
      //  .AddRazorViewEngine();

      aServiceCollection.AddSession();

      ConfigureServicesIdentityServer(aServiceCollection);

      aServiceCollection.Configure<RazorViewEngineOptions>(options =>
      {
        options.ViewLocationExpanders.Add(new ViewLocationExpander());
      });

      aServiceCollection.AddMediatR();
    }

    private static void ConfigureServicesIdentityServer(IServiceCollection aServiceCollection)
    {
      aServiceCollection.AddIdentityServer()
        .AddDeveloperSigningCredential()
        .AddInMemoryApiResources(Resources.GetApiResources())
        .AddInMemoryClients(Clients.GetClients())
        .AddInMemoryIdentityResources(Resources.GetIdentityResources())
        // .AddJwtBearerClientAuthentication();
    }
```
The Configure 

```
    public void Configure(
      IApplicationBuilder aApplicationBuilder,
      IHostingEnvironment aHostingEnvironment,
      IOptions<RequestLocalizationOptions> aRequestLocalizationOptions)
    {
      Environment.UseExceptionPage(aHostingEnvironment, aApplicationBuilder);
      // aApplicationBuilder.UseCors(CorsPolicy.Any.DisplayName);
      aApplicationBuilder.UseIdentityServer();
      aApplicationBuilder.UseRequestLocalization(aRequestLocalizationOptions.Value);
      aApplicationBuilder.UseStaticFiles(); // Serve up static files from wwwroot images etc...
      aApplicationBuilder.UseSession();
      aApplicationBuilder.UseMvc();
    }
```
The Clients configuration:

```
 new Client
        {
          ClientId = "someGUID",
          ClientName = "MySite",
          RequireClientSecret=false, // if false this is a public client.
          AllowedGrantTypes = GrantTypes.Implicit,
          AllowAccessTokensViaBrowser = true,
          AlwaysIncludeUserClaimsInIdToken = true,

          RedirectUris = {
            "http://local-spa.mysite.com:3000/callback",
            "https://dev-spa.mysite.com/callback",
          },
          PostLogoutRedirectUris = {
            "http://local-spa.mysite.com:3000/",
            "https://dev-spa.mysite.com/",
          },
          AllowedCorsOrigins = {
            "http://local-spa.mysite.com:3000",
            "https://dev-spa.mysite.com",
          },

          AllowedScopes =
          {
            IdentityServerConstants.StandardScopes.OpenId,
            IdentityServerConstants.StandardScopes.Profile,
            IdentityServerConstants.StandardScopes.Email,
            IdentityServerConstants.StandardScopes.Phone,
            Resources.WebOrderingApi,
          },

          RequireConsent = false,
        },
```
The TypeScript client:

```
import { Log, OidcClientSettings, UserManager, UserManagerSettings, } from 'oidc-client';

...

const protocol: string = window.location.protocol;
const hostname: string = window.location.hostname;
const port: string = window.location.port;

const oidcClientSettings: OidcClientSettings = {
  client_id: '46a0ab4a-1321-4d77-abe5-98f09310df0b',
  post_logout_redirect_uri: `${protocol}//${hostname}${port ? `:${port}` : ''}/`,
  redirect_uri: `${protocol}//${hostname}${port ? `:${port}` : ''}/callback`,
  response_type: 'id_token token',
  scope: 'openid profile email phone WebOrderingApi',
};

const userManagerSettings: UserManagerSettings = {
  ...oidcClientSettings,
  automaticSilentRenew: false,
  filterProtocolClaims: true,
  loadUserInfo: true,
  monitorSession: false,
  silent_redirect_uri: `${protocol}//${hostname}${port ? `:${port}` : ''}/callback`,
};

```
