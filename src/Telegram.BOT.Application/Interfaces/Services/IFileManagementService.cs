using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.BOT.Application.Interfaces.Services {
    public interface IFileManagementService 
    {
        string SaveFile(string name, string content);
        string getFileContent(string fileName);
        void DeleteFile(string fileName);
    }
}
