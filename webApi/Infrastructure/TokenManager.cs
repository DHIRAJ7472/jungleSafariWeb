using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using webApi.Infrastructure;
using webApi.Models;

namespace webApi.Infrastructure
{
    public class TokenManager
    {
        private static string Secret = "ZPQRSXaxdABCazc0944AXCRFGADF";

        public static MyJwtToken GenerateToken(string username, string password)
        {
            JungleSafariEntities1 db = new JungleSafariEntities1();
            var user = db.Users.FirstOrDefault(u => u.UserName == username && u.Password == password);
            if (user == null)
                return null;

            byte[] key = Encoding.UTF8.GetBytes(Secret);

            Claim c1 = new Claim(ClaimTypes.Name, username);
            Claim c2 = new Claim(ClaimTypes.Role, user.Role.roleName);
          

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new Claim[] { c1, c2});

            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.Now.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            string myToken = handler.WriteToken(token);
            //return myToken;
            MyJwtToken customToken = new MyJwtToken
            {
                Token = myToken,
                RoleName = user.Role.roleName
            };
            return customToken;
        }

        public static ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token);
                if (jwtToken == null)
                {
                    return null;
                }
                byte[] key = Encoding.UTF8.GetBytes(Secret);
                TokenValidationParameters parameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
                SecurityToken securityToken;
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, parameters, out securityToken);
                return principal;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}