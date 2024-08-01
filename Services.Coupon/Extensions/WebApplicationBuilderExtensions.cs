using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Services.Coupon.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static WebApplicationBuilder WebApplicationBuilder(this WebApplicationBuilder builder)
        {
            // validate token
            var sectionAppSetting = builder.Configuration.GetSection("ApiSettings");
            var secret = sectionAppSetting.GetValue<string>("Secret");
            var issuer = sectionAppSetting.GetValue<string>("Issuer");
            var audience = sectionAppSetting.GetValue<string>("Audience");

            //var key = Encoding.ASCII.GetBytes(secret);

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = audience,
                    ValidIssuer = issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret))
                };
            });
            return builder;
        }
    }
}
