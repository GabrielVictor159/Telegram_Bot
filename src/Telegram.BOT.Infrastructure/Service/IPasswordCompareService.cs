using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Services;
using Telegram.BOT.Application.UseCases.Ambient.EnvVariables.CreateEnv;

namespace Telegram.BOT.Infrastructure.Service {
    public class PasswordCompareService : IPasswordCompareService {
        public bool VerifyPassword(string password, string hashedPassword) {
            var passHasher = new PasswordHasher();

            if(passHasher.VerifyHashedPassword(hashedPassword, password) == PasswordVerificationResult.Failed) {
                return false;
            }
            return true;
        }
    }
}
