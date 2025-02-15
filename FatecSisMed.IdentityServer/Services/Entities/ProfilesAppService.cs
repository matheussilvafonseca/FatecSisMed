﻿using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using FatecSisMed.IdentityServer.Data.Entities;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace FatecSisMed.IdentityServer.Services.Entities;

public class ProfilesAppService : IProfileService
{
    //como visto anteriormente, ele não tem todos atributos do usuário do token
    //por isso implementamos essa classe, para que ele carregue no token
    //todos os dados do usuário

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;

    public ProfilesAppService(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
    }

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        //id do usuário no IdentityServer
        string id = context.Subject.GetSubjectId();
        //localiza o usuário peo Id
        ApplicationUser user = await _userManager.FindByIdAsync(id);

        //cria a ClaimsPrincipal para o usuário

        ClaimsPrincipal userClaims = await _userClaimsPrincipalFactory.CreateAsync(user);

        //define uma coleção de claims para o usuário e inclui o sobrenome e o nome do usuário
        List<Claim> claims = userClaims.Claims.ToList();
        claims.Add(new Claim(JwtClaimTypes.FamilyName, user.LastName));
        claims.Add(new Claim(JwtClaimTypes.GivenName, user.FirstName));

        //se o userManager suportar a Role
        if(_userManager.SupportsUserRole)
        {
            //obtem a lista dos nomes das roles para o usuário
            IList<string> roles = await _userManager.GetRolesAsync(user);

            foreach (string role in roles)
            {
                //adiciona a role na claim
                claims.Add(new Claim(JwtClaimTypes.Role, role));

                //se roleManager suportar claims para roles
                if(_roleManager.SupportsRoleClaims) { 
                
                    //localizar o perfil
                    IdentityRole identityRole = await _roleManager.FindByNameAsync(role);

                    //inclui o perfil
                    if(identityRole != null)
                    {
                        //inlcui as claims associada com a role
                        claims.AddRange(await _roleManager.GetClaimsAsync(identityRole));
                    }
                }
            }
        }

        //retorna as claims no contexto
        context.IssuedClaims = claims;
    }
    public async Task IsActiveAsync(IsActiveContext context)
    {
        //obtem o id do usuário do identity
        string userId = context.Subject.GetSubjectId();

        //localiza o usuário
        ApplicationUser user = await _userManager.FindByIdAsync(userId);

        //verifica se está ativo
        context.IsActive = user is not null;
    }
}
