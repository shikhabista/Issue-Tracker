using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Base.Entities;
using Microsoft.IdentityModel.Tokens;
using QB_Web.Extensions;

namespace QB_Web.Generator;

public interface IJwtTokenGenerator
{
    string GetJwtToken(User user);
}

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IConfiguration _configuration;

    public JwtTokenGenerator(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GetJwtToken(User user)
    {
        var key = Encoding.ASCII.GetBytes(_configuration.GetJwtKey());
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.UserName)
            }),
            Expires = DateTime.Now.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = tokenHandler.WriteToken(token);
        return jwtToken;
    }
}