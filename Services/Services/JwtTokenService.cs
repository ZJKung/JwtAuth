using DAL.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Services.Services
{
	public class JwtTokenService
	{
		public JwtTokenService(IConfiguration configruration)
		{
			Configuration = configruration;
		}
		public IConfiguration Configuration { get; }

		public string GenerateJwtToken(User user)
		{

			var issuer = Configuration.GetSection("JwtSettings").GetSection("Issuer").Value;
			var signKey = Configuration.GetSection("JwtSettings").GetSection("SignKey").Value;			
			var tokenHandler = new JwtSecurityTokenHandler();
			//todo 
			var key = Encoding.ASCII.GetBytes(signKey);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Issuer=issuer,
				Subject = new ClaimsIdentity(new Claim[]
				{					
					new Claim(JwtRegisteredClaimNames.Sub, user.Username),
					new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
				}),
				Expires = DateTime.UtcNow.AddMonths(1),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), 
				SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}
		public RefreshToken GenerateRefreshToken(string ipAddress)
		{
			using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
			{
				var randomBytes = new byte[256];
				rngCryptoServiceProvider.GetBytes(randomBytes);
				return new RefreshToken
				{
					Token = Convert.ToBase64String(randomBytes),
					Expires = DateTime.UtcNow.AddMonths(1),
					Created = DateTime.UtcNow,
					CreatedByIp = ipAddress
				};
			}
		}
	}
}
