﻿using Autofac.Core;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Runtime.CompilerServices;
using Telegram.BOT.Application.UseCases.Ambient.EnvVariables.CreateEnv;

namespace Telegram.BOT.WebMVC.DependencyInjection {
    public static class AuthenticationExtensions {
        public static void AddAppAuthorization(this IServiceCollection services) {
            var urlPrefix = Environment.GetEnvironmentVariable("URL_PREFIX") ?? "";
            services.AddAuthentication(options => {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options => {
                options.LoginPath = $"{urlPrefix}/User/Login";
                options.LogoutPath = $"{urlPrefix}/User/Logout";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                options.AccessDeniedPath = $"{urlPrefix}/User/Forbidden";
            });

            services.AddAuthorization(options => {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
            });
        }

        public static void SetDefaultUserPassword(this IServiceProvider serviceProvider) {
            ICreateEnvRequest createEnvRequest = serviceProvider.GetService<ICreateEnvRequest>()!;

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
