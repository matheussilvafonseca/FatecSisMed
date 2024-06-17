using FatecSisMed.IdentityServer.Configuration;//
using FatecSisMed.IdentityServer.Data.Entities;//
using FatecSisMed.IdentityServer.SeedDataBase.Interfaces; //
using IdentityModel;//
using Microsoft.AspNetCore.Identity;//
using System.Security.Claims;//

namespace FatecSisMed.IdentityServer.SeedDataBase.Entities;

public class DataBaseIdentityServerInitializer : IDataBaseInitializer
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public DataBaseIdentityServerInitializer(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }
    public void InitializeSeedRoles()
    {
        if (!_roleManager.RoleExistsAsync(IdentityConfiguration.Admin).Result)
        {
            // cria o perfil Admin
            IdentityRole roleAdmin = new IdentityRole();
            roleAdmin.Name = IdentityConfiguration.Admin;
            roleAdmin.NormalizedName = IdentityConfiguration.Admin.ToUpper();
            _roleManager.CreateAsync(roleAdmin).Wait();
        }
        //se o perfil client não existir, entõa cria o perfil
        if (!_roleManager.RoleExistsAsync(IdentityConfiguration.Client).Result)
        {
            //cria o perfil Client
            IdentityRole roleClient = new IdentityRole();
            roleClient.Name = IdentityConfiguration.Client;
            roleClient.NormalizedName = IdentityConfiguration.Client.ToUpper();
            _roleManager.CreateAsync(roleClient).Wait();
        }

    }
    public void InitializeSeedUsers()
    {
        //se o usuário admin não existir, cria o usuario, definindo a senha e atribuindo o perfil
        if (_userManager.FindByEmailAsync("matheus@gmail.com").Result is null)
        {
            //define os dados dos usuários admin
            ApplicationUser admin = new ApplicationUser()
            {
                UserName = "Matheus",
                NormalizedUserName = "MATHEUS",
                Email = "matheus@gmail.com",
                NormalizedEmail = "MATHEUS@GMAIL.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                PhoneNumber = "+55 (17) 99634-1796",
                FirstName = "Usuario",
                LastName = "Matheus",
                SecurityStamp = Guid.NewGuid().ToString(),
            };
            //cria o usuario Admin e atribui a senha a ele
            // obrigatoriamente deve ter 1 maiusculo, minuscula, numero e caracter especial
            IdentityResult resultAdmin = _userManager.CreateAsync(admin, "Admin@1234").Result;
            if (resultAdmin.Succeeded)
            {
                //inclui o usuário admin ao perfil admin
                _userManager.AddToRoleAsync(admin, IdentityConfiguration.Admin).Wait();

                //inclui as claims do usuário admin
                var adminClaims = _userManager.AddClaimsAsync(admin, new Claim[]
                {
                    new Claim(JwtClaimTypes.Name, $"{admin.FirstName} {admin.LastName}"),
                    new Claim(JwtClaimTypes.GivenName, admin.FirstName),
                    new Claim(JwtClaimTypes.FamilyName, admin.LastName),
                    new Claim(JwtClaimTypes.Role, IdentityConfiguration.Admin)
                }).Result;
            }
        }

        //se o usuário cient não existir, cria o usuário definindo a senha e atribuindo o perfil

        if (_userManager.FindByEmailAsync("client@gmail.com").Result is null)
        {
            //define os dados dos usuários client
            ApplicationUser client = new ApplicationUser()
            {
                UserName = "matheusclient",
                NormalizedUserName = "MATHEUSCLIENT",
                Email = "client@gmail.com",
                NormalizedEmail = "CLIENT@GMAIL.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                PhoneNumber = "+55 (17) 99772-8422",
                FirstName = "Usuario",
                LastName = "Client",
                SecurityStamp = Guid.NewGuid().ToString(),
            };


            //cria o usuário client e atribui a senha a ele
            IdentityResult resultClient = _userManager.CreateAsync(client, "Client@1234").Result;
            if (resultClient.Succeeded)
            {
                //inclui o usuário client ao perfil client
                _userManager.AddToRoleAsync(client, IdentityConfiguration.Client).Wait();

                //inclui as claims do usuário Client
                var clientClaims = _userManager.AddClaimsAsync(client, new Claim[] 
                {
                    new Claim(JwtClaimTypes.Name, $"{client.FirstName} {client.LastName}"),
                    new Claim(JwtClaimTypes.GivenName, client.FirstName),
                    new Claim(JwtClaimTypes.FamilyName, client.LastName),
                    new Claim(JwtClaimTypes.Role, IdentityConfiguration.Client)
                }).Result;
            }
        }

    }
}
