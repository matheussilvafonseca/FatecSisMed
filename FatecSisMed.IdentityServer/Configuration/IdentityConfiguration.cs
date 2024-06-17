using Duende.IdentityServer.Models;
using Duende.IdentityServer;

namespace FatecSisMed.IdentityServer.Configuration;

public class IdentityConfiguration
{
    public const string Admin = "Admin";
    public const string Client = "Client";

    public static IEnumerable<IdentityResource> IdentyResources => new List<IdentityResource>
    {
        new IdentityResources.OpenId(), // usado para receber o token
        new IdentityResources.Email(),
        //email profile são usados para poder acessar os recursos do cliente
        new IdentityResources.Profile()

    };

    public static IEnumerable<ApiScope> ApiScopes => new List<ApiScope>
    {
        //fatecsismed que irá passar para o IdentityServer obter o token
        new ApiScope("fatecsismed", "FatecSisMed Server"),
        new ApiScope(name: "read", "Read data."),
        new ApiScope(name: "write", "Write data."),
        new ApiScope(name: "delete", "Delete data.")
    };

    public static IEnumerable<Client> Clients => new List<Client>
    {
        //cliente genérico
        new Client
        {
            ClientId = "client",
            //esse segredo tambéma será adicionado ao appsettings.json do projeot web
            ClientSecrets = {new Secret("vai_reprovar".Sha256())},
            AllowedGrantTypes = GrantTypes.ClientCredentials,
            //precisa das credenciais do usuário
            AllowedScopes = {"read", "write", "profiles"}
        },
      new Client
      {
          ClientId = "fatecsismed",
          ClientSecrets = {new Secret("vai_reprovar".Sha256())},
          AllowedGrantTypes= GrantTypes.Code,
          RedirectUris = {"https://localhost:7256/signin-oidc"},//login
          PostLogoutRedirectUris = {"https://localhost:7256/signout-callback-oidc"},
          AllowedScopes = new List<string>
          {
              IdentityServerConstants.StandardScopes.OpenId,
              IdentityServerConstants.StandardScopes.Profile,
              IdentityServerConstants.StandardScopes.Email,
              "fatecsismed"
          }
      }
    };
}

