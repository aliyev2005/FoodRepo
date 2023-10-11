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
            string writePath = Path.Combine(Directory.GetCurrentDirectory(), _ROOT, _PATH);
            Directory.CreateDirectory(writePath);

            string extension = file.FileName.Split('.').Last();
            string newName;

            newName = string.IsNullOrEmpty(fileName)
                ? $"{Guid.NewGuid()}.{extension}"
                : $"{fileName}.{extension}";

            using FileStream stream = new(Path.Combine(writePath, newName), FileMode.Create);
            file.CopyTo(stream);
            return $"{_ROOT}/{_PATH}/{newName}";
        }
    }
}
