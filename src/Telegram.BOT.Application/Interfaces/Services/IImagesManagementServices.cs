using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.BOT.Application.Interfaces.Services
{
    public interface IImagesManagementServices
    {
        string SaveImage(byte[] image);
        bool DeleteImage(string name);
    }
}
