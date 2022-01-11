using IQT.Authorization.API.DTO;
using IQT.Authorization.API.Entities;
using IQT.Authorization.API.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IQT.Authorization.API.Services;

public class AuthService
{
    private readonly TokenConfigurations _tokenConfigurations;
    private readonly UserManager<UserModel> _userManager;

    public AuthService(TokenConfigurations tokenConfigurations, UserManager<UserModel> userManager)
    {
        _tokenConfigurations = tokenConfigurations;
        _userManager = userManager;
    }

    public async Task<AuthTokenDTO> GenerateTokenAsync(string email)
    {
        // Define as claims do usuário (não é obrigatório mas cria mais chaves no Payload)
        var user = await _userManager.FindByEmailAsync(email);
        var userClaims = await _userManager.GetClaimsAsync(user);

        var claims = await GetClaimsUser(userClaims, user);

        // Gera uma chave
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenConfigurations.SecretJWTKey));

        // Gera a assinatura digital do token
        var credenciais = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // Tempo de expiracão do token            
        var expiration = DateTime.UtcNow.AddSeconds(_tokenConfigurations.ExpireSeconds);

        // Monta as informações do token
        JwtSecurityToken token = new JwtSecurityToken(
            issuer: _tokenConfigurations.Issuer,
            audience: _tokenConfigurations.Audience,
            claims: claims,
            expires: expiration,
            signingCredentials: credenciais);

        // Retorna o token e demais informações
        var response = new AuthTokenDTO
        {
            Authenticated = true,
            Expiration = expiration,
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            RefreshToken = Guid.NewGuid().ToString().Replace("-", String.Empty),
            Message = "Token JWT OK",
            UserToken = new UserTokenDTO
            {
                Email = email
            }
        };

        return response;
    }

    private async Task<ICollection<Claim>> GetClaimsUser(ICollection<Claim> claims, UserModel user)
    {
        var userRoles = await _userManager.GetRolesAsync(user);

        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
        claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Birthdate, DateTime.Now.AddYears(-15).ToString()));

        foreach (var role in userRoles)
        {
            claims.Add(new Claim("role", role));
        }

        var identityClaims = new ClaimsIdentity();
        identityClaims.AddClaims(claims);

        return claims;
    }
}
