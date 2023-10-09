using FoodProject.Libraries.Repository;

namespace FoodProject.Libraries
{
    public class FileManager : IFileManager
    {
        //private const string _PATH = "Uploads";
        private const string _ROOT = "Uploads";

        void IFileManager.Delete(string fileName)
        {
            throw new NotImplementedException();
        }

        bool IFileManager.FileExists(string fileName)
        {
            throw new NotImplementedException();
        }

        string IFileManager.Upload(IFormFile file, string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
