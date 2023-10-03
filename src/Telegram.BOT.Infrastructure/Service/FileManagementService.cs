using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Telegram.BOT.Application.Interfaces.Services;

namespace Telegram.BOT.Infrastructure.Service {
    internal class FileManagementService : IFileManagementService {
        public void DeleteFile(string fileName) {
            var filePath = Path.Combine(Environment.CurrentDirectory, "appData");
            var fullFilePath = Path.Combine(filePath, fileName);

            if (File.Exists(fullFilePath)) {
                File.Delete(fullFilePath);
            }
        }

        public string getFileContent(string fileName) {
            var filePath = Path.Combine(Environment.CurrentDirectory, "appData");
            var fullFilePath = Path.Combine(filePath, fileName);
            string content;

            if (!File.Exists(fullFilePath)) {
                throw new FileNotFoundException();
            }

            content = File.ReadAllText(fullFilePath);

            return content;
        }

        public string SaveFile(string name, string content) {
            var filePath = Path.Combine(Environment.CurrentDirectory, "appData");
            var fullFilePath = Path.Combine(filePath, name);

            if(!Directory.Exists(filePath)) {
                Directory.CreateDirectory(filePath);
            }

            File.WriteAllText(fullFilePath, content);

            return fullFilePath;
        }
    }
}
