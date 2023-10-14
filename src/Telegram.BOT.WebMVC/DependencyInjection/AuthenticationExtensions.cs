using Autofac.Core;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Runtime.CompilerServices;
using Telegram.BOT.Application.UseCases.Ambient.EnvVariables.CreateEnv;

namespace Telegram.BOT.WebMVC.DependencyInjection {
    public static class AuthenticationExtensions {
        public static void AddAppAuthorization(this IServiceCollection services) {
            services.AddAuthentication(options => {
                options.RequireAuthenticatedSignIn = false;
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
                .AddJwtBearer()
                .AddCookie(options => {
                    options.LoginPath = "/User/Login";
                    options.LogoutPath = "/User/Logout";
                });

            services.AddAuthorization(options => {
                var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(CookieAuthenticationDefaults.AuthenticationScheme);

                defaultAuthorizationPolicyBuilder =
                    defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();

                options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
            });
        }

        public static void SetDefaultUserPassword(this IServiceProvider serviceProvider) {
            ICreateEnvRequest createEnvRequest = serviceProvider.GetService<ICreateEnvRequest>();

            var createUserPassRequest = new Application.UseCases.Ambient.EnvVariables.CreateEnv.CreateEnvRequest() {
                EnvVariable = new ManagementServices.variables.Domain.Models.EnvVariable() {
                    Key = "AdminPass",
                    Value = "ANdT02tr1EMS6BOMlU887oE/aNXrtSoYK6+oXw5usnRV64lEDRItFWZgs+uoyP6nVw=="
                }
            };
            createEnvRequest.Execute(createUserPassRequest);
            if (createUserPassRequest.IsError) {
                string message = $"Erro ao iniciar a senha de administrador: {createUserPassRequest.output}";
                throw new Exception(message);
            }
        }
    }
}
