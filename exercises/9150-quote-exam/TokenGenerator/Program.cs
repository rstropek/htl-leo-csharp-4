using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

const string mySecret = "jlslfoikl890sdf^08~sdf870ß$!";

if (args.Length != 3)
{
    Console.WriteLine("USAGE: TokenGenerator <email> <name> <given-name>");
	return;
}

var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));

var myIssuer = "https://ui.quotexchange.com";
var myAudience = "https://api.quotexchange.com";

var tokenHandler = new JwtSecurityTokenHandler();
var tokenDescriptor = new SecurityTokenDescriptor
{
	Subject = new ClaimsIdentity(new Claim[]
	{
			new Claim(ClaimTypes.NameIdentifier, args[0]),
			new Claim(ClaimTypes.Name, args[1]),
			new Claim(ClaimTypes.GivenName, args[2]),
	}),
	Expires = DateTime.UtcNow.AddDays(7),
	Issuer = myIssuer,
	Audience = myAudience,
	SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
};

var token = tokenHandler.CreateToken(tokenDescriptor);
Console.WriteLine(tokenHandler.WriteToken(token));
