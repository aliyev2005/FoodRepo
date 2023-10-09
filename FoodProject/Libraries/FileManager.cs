using FoodProject.Libraries.Repository;

namespace FoodProject.Libraries
{
    public class FileManager : IFileManager
    {
        private const string _ROOT = "Uploads";

        void IFileManager.Delete(string fileName, string _PATH)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), _ROOT, _PATH, fileName);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        bool IFileManager.FileExists(string fileName, string _PATH)
        {
            return File.Exists(Path.Combine(Directory.GetCurrentDirectory(), _ROOT, _PATH, fileName));
        }

        string IFileManager.Upload(IFormFile file, string _PATH, string fileName)
        {
            string[] list = file.FileName.Split('.');
            string newName;
            if (fileName == "")
            {
                newName = $"{Guid.NewGuid()}.{list[^1]}";
            }
            else
            {
                newName = $"{fileName}.{list[^1]}";
            }
            var writePath = Path.Combine(Directory.GetCurrentDirectory(), _ROOT, _PATH);
            if (!Directory.Exists(writePath))
                Directory.CreateDirectory(writePath);
            var path = Path.Combine(writePath, newName);
            using var stream = new FileStream(path, FileMode.Create);
            file.CopyTo(stream);
            return newName;
        }
    }
}
