//using Microsoft.Owin;
//using Microsoft.Owin.Security;
//using Microsoft.Owin.Security.Jwt;
//using Owin;

//[assembly: OwinStartup(typeof(YourNamespace.Startup))]

//namespace ACCWebApp
//{
//    public class Startup
//    {
//        public void Configuration(IAppBuilder app)
//        {
//            ConfigureAuth(app);
//            // Additional startup configurations
//        }

//        private void ConfigureAuth(IAppBuilder app)
//        {
//            var issuer = "http://your-token-issuer";
//            var audience = "http://your-web-api-endpoint";
//            var secretKey = "your-secret-key";

//            var authenticationOptions = new JwtBearerAuthenticationOptions
//            {
//                AuthenticationMode = AuthenticationMode.Active,
//                AllowedAudiences = new[] { audience },
//                IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
//                {
//                    new SymmetricKeyIssuerSecurityTokenProvider(issuer, secretKey)
//                }
//            };

//            // Enable JWT authentication
//            app.UseJwtBearerAuthentication(authenticationOptions);
//        }
//    }
//}
