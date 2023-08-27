using Telegram.BOT.Application.Interfaces.Services;

namespace Telegram.BOT.ImagesManagement.Services
{
    public class ImagesManagementServices : IImagesManagementServices
    {
        public string SaveImage(byte[] image)
        {
            string nomeArquivo = Guid.NewGuid().ToString() + ".png";
            string pathImages = Environment.GetEnvironmentVariable("ImagesPathByServiceInfra")!;
            string caminhoArquivo = Path.Combine(pathImages, nomeArquivo);
            using (var fs = new FileStream(caminhoArquivo, FileMode.Create))
            {
                fs.Write(image, 0, image.Length);
            }

            return nomeArquivo;
        }
        public bool DeleteImage(string name)
        {
            string pathImages = Environment.GetEnvironmentVariable("ImagesPathByServiceInfra")!;
            string caminhoArquivo = Path.Combine(pathImages, name);
            if (File.Exists(caminhoArquivo))
            {
                try
                {
                    File.Delete(caminhoArquivo);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
