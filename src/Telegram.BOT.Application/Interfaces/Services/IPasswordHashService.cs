using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.BOT.Application.Interfaces.Services {
    public interface IPasswordHashService {
        string Hash(string password);
        bool VerifyPassword(string password, string hashedPassword);
    }
}
