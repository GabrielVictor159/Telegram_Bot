using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Services;

namespace Telegram.BOT.Infrastructure.Service {
    public class PasswordHashService : IPasswordHashService {
        public string Hash(string password) {
            var passHasher = new PasswordHasher();
            var hashedPassword = passHasher.HashPassword(password);
            return hashedPassword;
        }

        public bool VerifyPassword(string password, string hashedPassword) {
            var passHasher = new PasswordHasher();

            if(passHasher.VerifyHashedPassword(hashedPassword, password) == PasswordVerificationResult.Failed) {
                return false;
            }
            return false;
        }
    }
}
