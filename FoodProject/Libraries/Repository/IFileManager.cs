namespace FoodProject.Libraries.Repository
{
    public interface IFileManager
    {
        string Upload(IFormFile file, string fileName = "");
        void Delete(string fileName);
        bool FileExists(string fileName);
    }
}
